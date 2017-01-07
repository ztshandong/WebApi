using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using WebApi.Core;
using WebApi.Public;

namespace WebApi.DAL
{
    internal class DataProvider
    {
        //  private DataBaseConnStringFactory dataBaseConnFactory = new DataBaseConnStringFactory();

        private static readonly Lazy<DataProvider> lazy = new Lazy<DataProvider>(() => new DataProvider());

        internal static DataProvider Instance { get { return lazy.Value; } }

        Dictionary<Type, DbType> typeMap = new Dictionary<Type, DbType>();
        private DataProvider()
        { IniDbType(); }

        private void IniDbType()
        {
            typeMap[typeof(object)] = DbType.Object;
            typeMap[typeof(byte)] = DbType.Byte;
            typeMap[typeof(sbyte)] = DbType.SByte;
            typeMap[typeof(short)] = DbType.Int16;
            typeMap[typeof(ushort)] = DbType.UInt16;
            typeMap[typeof(int)] = DbType.Int32;
            typeMap[typeof(uint)] = DbType.UInt32;
            typeMap[typeof(long)] = DbType.Int64;
            typeMap[typeof(ulong)] = DbType.UInt64;
            typeMap[typeof(float)] = DbType.Single;
            typeMap[typeof(double)] = DbType.Double;
            typeMap[typeof(decimal)] = DbType.Decimal;
            typeMap[typeof(bool)] = DbType.Boolean;
            typeMap[typeof(string)] = DbType.String;
            typeMap[typeof(char)] = DbType.StringFixedLength;
            typeMap[typeof(Guid)] = DbType.Guid;
            typeMap[typeof(DateTime)] = DbType.DateTime;
            typeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            typeMap[typeof(byte[])] = DbType.Binary;
            typeMap[typeof(byte?)] = DbType.Byte;
            typeMap[typeof(sbyte?)] = DbType.SByte;
            typeMap[typeof(short?)] = DbType.Int16;
            typeMap[typeof(ushort?)] = DbType.UInt16;
            typeMap[typeof(int?)] = DbType.Int32;
            typeMap[typeof(uint?)] = DbType.UInt32;
            typeMap[typeof(long?)] = DbType.Int64;
            typeMap[typeof(ulong?)] = DbType.UInt64;
            typeMap[typeof(float?)] = DbType.Single;
            typeMap[typeof(double?)] = DbType.Double;
            typeMap[typeof(decimal?)] = DbType.Decimal;
            typeMap[typeof(bool?)] = DbType.Boolean;
            typeMap[typeof(char?)] = DbType.StringFixedLength;
            typeMap[typeof(Guid?)] = DbType.Guid;
            typeMap[typeof(DateTime?)] = DbType.DateTime;
            typeMap[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;
        }

        private DbConnection CreateConnection(DbProvider4DAL dbp)
        {
            MyLog log1 = new MyLog(WebApiGlobal._MyLogPath);
            log1.log("CreateConnection");
            string conn = DataBaseConnFactory.Instance.GetConnString(dbp.CurrentChooseDataBase);
            MyLog log2 = new MyLog(WebApiGlobal._MyLogPath);
            log2.log("conn");
            DbConnection DBConn = ProviderFactory.Instance.GetDbProviderFactory(dbp.CurrentDbProviderType).CreateConnection();
            DBConn.ConnectionString = conn;
           
            
            return DBConn;
        }

        internal DataSet GetDataSet(DbProvider4DAL dbp)
        {
            try
            {
                MyLog log = new MyLog(WebApiGlobal._MyLogPath);
                log.log("GetDataSet");
                DataSet ds = new DataSet();
                if (dbp.CurrentCommand.Connection == null)
                {
                    MyLog log1 = new MyLog(WebApiGlobal._MyLogPath);
                    log1.log("Connection == null");
                    dbp.CurrentCommand.Connection = CreateConnection(dbp);
                }
                using (dbp.CurrentCommand.Connection)
                {
                    dbp.CurrentDbDataAdapter.Fill(ds);
                }

                return ds;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                dbp.CurrentDbDataAdapter.Dispose();
                dbp.CurrentCommand.Connection.Close();
                dbp.CurrentCommand.Connection.Dispose();
            }
        }
        internal void AddDbParamsByCustom(DbCommand dbcomm, string ParameterName, object ParameterValue, DbType dbType, int dbSize)
        {
            DbParameter dbp = dbcomm.CreateParameter();
            dbp.ParameterName = ParameterName;
            dbp.Value = ParameterValue;
            dbp.DbType = dbType;
            dbp.Size = dbSize;
            dbcomm.Parameters.Add(dbp);
        }
        internal void AddDbParamsByPropertyInfo(DbCommand dbcomm, object parameters = null)
        {
            if (parameters != null)
            {
                Type T = parameters.GetType();
                PropertyInfo[] properties = T.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(parameters);

                    if ((!value.IsNullOrEmpty()) && (!dbcomm.Parameters.Contains("@" + property.Name)))
                    {
                        DbParameter dbp = dbcomm.CreateParameter();
                        dbp.ParameterName = "@" + property.Name;
                        dbp.Value = property.GetValue(parameters);
                        dbp.DbType = typeMap[property.PropertyType];
                        if (dbp.DbType == DbType.DateTime)
                            if (dbp.Value.ToSqlDateTime() <= WebApiGlobal.MinSqlDate) continue;
                        if (dbp.DbType == DbType.Int16 || dbp.DbType == DbType.Int32 || dbp.DbType == DbType.Int64
                            || dbp.DbType == DbType.Single || dbp.DbType == DbType.Double || dbp.DbType == DbType.Decimal
                            )
                            if (dbp.Value.ToDecimalEx() == 0) continue;
                        ORM_ValidateAttribute[] RequiredFields = (ORM_ValidateAttribute[])property.GetCustomAttributes(typeof(ORM_ValidateAttribute), false);
                        if (RequiredFields != null && RequiredFields.Length == 1)
                        {
                            int i = RequiredFields[0].DbSize.ToIntEx();
                            if (i > 0)
                                dbp.Size = i;
                        }
                        dbcomm.Parameters.Add(dbp);
                    }
                }
            }
        }

        internal void AddDbParamsByFieldInfo(DbCommand dbcomm, object parameters = null)
        {
            if (parameters != null)
            {
                Type T = parameters.GetType();

                FieldInfo[] fields = GetAllInfos(T);
                foreach (FieldInfo field in fields)
                {
                    object value = field.GetValue(parameters);
                    if ((!value.IsNullOrEmpty()) && (!dbcomm.Parameters.Contains("@" + field.Name)))
                    {
                        DbParameter dbp = dbcomm.CreateParameter();
                        dbp.ParameterName = "@" + field.Name;
                        dbp.Value = field.GetValue(parameters);
                        dbp.DbType = typeMap[field.FieldType];
                        dbcomm.Parameters.Add(dbp);
                    }
                }
            }
        }
        private FieldInfo[] GetAllInfos(Type T)
        {
            FieldInfo[] infos = null;
            FieldInfo[] infosSelf = T.GetFields();
            FieldInfo[] infosBase = null;

            if (T.BaseType != null)
                infosBase = GetAllInfos(T.BaseType);

            if (infosBase == null || infosBase.Length == 0)
                infos = infosSelf;
            else
            {
                int iLength = infosBase.Length;
                Array.Resize<FieldInfo>(ref infosBase, infosBase.Length + infosSelf.Length);
                infosSelf.CopyTo(infosBase, iLength);
                infos = infosBase;
            }
            return infos;
        }
        //internal DataSet GetDataSet(DbProvider4DAL dbp ,string sql, object parameters = null)
        //{
        //    try
        //    {
        //        using (dbp._CurrentConnection= ProviderFactory.GetDbProviderFactory(dbp._CurrentDbProviderType).CreateConnection())
        //        {
        //            dbp.Connect();
        //            AddDbParams(dbp,sql, parameters);
        //            //using (DbDataAdapter adapter = providerFactory.CreateDataAdapter())
        //            //{
        //            //dbp._CurrentDbDataAdapter.SelectCommand = dbp._CurrentCommand;
        //            DataSet data = new DataSet();
        //                dbp._CurrentDbDataAdapter.Fill(data);
        //                return data;
        //            //}
        //        }
        //    }
        //    catch (Exception ex) { throw ex; }
        //}

        //internal DataSet GetDataSet(DbProvider4DAL dbp)
        //{
        //    try
        //    {
        //        using (dbp._CurrentConnection = ProviderFactory.GetDbProviderFactory(dbp._CurrentDbProviderType).CreateConnection())
        //        {
        //            DataSet data = new DataSet();
        //            dbp._CurrentDbDataAdapter.Fill(data);
        //            return data;
        //        }
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
        //internal void AddDbParams(DbProvider4DAL dbpro,string sql, object parameters = null)
        //{
        //    dbpro._CurrentDbDataAdapter.SelectCommand.CommandText = sql;
        //    if (parameters != null)
        //    {
        //        Type T = parameters.GetType();

        //        FieldInfo[] fields = GetAllInfos(T);
        //        foreach (FieldInfo field in fields)
        //        {
        //            object value = field.GetValue(parameters);
        //            if (value != null && (!dbpro._CurrentCommand.Parameters.Contains("@" + field.Name)))
        //            {
        //                DbParameter dbp = dbpro._CurrentCommand.CreateParameter();
        //                dbp.ParameterName = "@" + field.Name;
        //                dbp.Value = field.GetValue(parameters);
        //                dbp.DbType = typeMap[field.FieldType];
        //                dbpro._CurrentCommand.Parameters.Add(dbp);
        //            }
        //        }
        //    }
        //}

        //internal DbCommand CreateDbCommand(string sql, CommandType commandType = CommandType.StoredProcedure, string ConnectionString = "SQLTestConnection", DbProviderType dbProviderType = DbProviderType.SqlServer)
        //{
        //    DbConnection connection = providerFactory.CreateConnection();
        //    DbCommand command = providerFactory.CreateCommand();
        //    connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;
        //    command.CommandText = sql;
        //    command.CommandType = commandType;
        //    command.Connection = connection;
        //    return command;
        //}




    }
}
