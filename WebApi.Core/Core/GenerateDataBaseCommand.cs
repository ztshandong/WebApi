
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using WebApi.DAL;

namespace WebApi.Core
{

    internal class GenerateDataBaseCommandByTableFields : GenerateDataBaseCommand
    {
        internal GenerateDataBaseCommandByTableFields(Type ORM_Model, DataTable dt, DbProviderType dbProviderType = DbProviderType.SqlServer)
        {
            _ORMModel = ORM_Model;
            _DbProviderType = dbProviderType;
            this.GenerateSQL(ORM_Model, dt, dbProviderType);
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
                Array.Resize<FieldInfo>(ref infosBase, infosBase.Length + infosSelf.Length);//相加两数组
                infosSelf.CopyTo(infosBase, iLength);
                infos = infosBase;
            }
            return infos;
        }
        internal void GenerateSQL(Type ORM_Model, DataTable dt, DbProviderType dbProviderType)
        {
            object[] attrClass;

            //查找ORM的属性定义
            ORM_TableAttribute classAttribute = null;
            attrClass = ORM_Model.GetCustomAttributes(typeof(ORM_TableAttribute), false);
            if (attrClass != null && attrClass.Length == 0) throw new Exception("ORM_ObjectClassAttribute未定义!");
            classAttribute = attrClass[0] as ORM_TableAttribute;

            //查找ORM控制并发的属性定义
            ORM_ConcurrentAttribute concurrentAttribute = null;
            attrClass = ORM_Model.GetCustomAttributes(typeof(ORM_ConcurrentAttribute), false);
            if (attrClass != null && attrClass.Length > 0 && classAttribute.IsSummaryTable)//仅主表控制并发
            {
                concurrentAttribute = attrClass[0] as ORM_ConcurrentAttribute;
                _ConcurrentAttribute = concurrentAttribute;
            }

            //是否主表属性            
            _IsSummary = classAttribute.IsSummaryTable;
            _PrimaryFieldName = classAttribute.PrimaryKey;//主键，复合主键盘用";"分开            

            //生成一个新的对象
            object obj = ORM_Model.Assembly.CreateInstance(ORM_Model.FullName);

            FieldInfo[] infos = GetAllInfos(ORM_Model);

            ArrayList fieldArr = new ArrayList();

            _cmdInsert = ProviderFactory.Instance.GetDbProviderFactory(dbProviderType).CreateCommand();
            _cmdUpdate = ProviderFactory.Instance.GetDbProviderFactory(dbProviderType).CreateCommand();
            _cmdDelete = ProviderFactory.Instance.GetDbProviderFactory(dbProviderType).CreateCommand();
            _cmdInsert.CommandType = CommandType.Text;
            _cmdUpdate.CommandType = CommandType.Text;
            _cmdDelete.CommandType = CommandType.Text;
            foreach (FieldInfo info in infos)
            {
                //取字段属性定义
                object[] attrField = info.GetCustomAttributes(typeof(ORM_FieldAttribute), false);
                if (attrField == null || attrField.Length == 0) continue;

                string fieldName = info.GetValue(obj).ToString();

                ORM_FieldAttribute fieldAttr = attrField[0] as ORM_FieldAttribute;

                //用作构造sql语句中的参数
                if (fieldAttr.IsAddOrUpdate)
                {
                    //取得最多的参数，与生成的sql语句作匹配,构造SQL参数语句                                             
                    if (!_cmdInsert.Parameters.Contains("@" + fieldName)) this.AddParam(_cmdInsert, fieldAttr, fieldName); //_cmdInsert.Parameters.Add("@" + fieldName, fieldAttr.Type, fieldAttr.Size, fieldName);
                    if (!_cmdUpdate.Parameters.Contains("@" + fieldName)) this.AddParam(_cmdUpdate, fieldAttr, fieldName);//_cmdUpdate.Parameters.Add("@" + fieldName, fieldAttr.Type, fieldAttr.Size, fieldName);

                    ColumnProperty colProper = new ColumnProperty(fieldName, fieldAttr.IsPrimaryKey ? true : false);
                    fieldArr.Add(colProper);
                }

                //生成主键的参数
                if (fieldAttr.IsPrimaryKey)
                {
                    if (!_cmdUpdate.Parameters.Contains("@" + fieldName)) this.AddParam(_cmdUpdate, fieldAttr, fieldName); //_cmdUpdate.Parameters.Add("@" + fieldName, fieldAttr.Type, fieldAttr.Size, fieldName);
                    if (!_cmdDelete.Parameters.Contains("@" + fieldName)) this.AddParam(_cmdDelete, fieldAttr, fieldName); //_cmdDelete.Parameters.Add("@" + fieldName, fieldAttr.Type, fieldAttr.Size, fieldName);

                }

                //控制并发的字段名
                if (concurrentAttribute != null && concurrentAttribute.TimestampFieldName == fieldName)
                {
                    if (!_cmdUpdate.Parameters.Contains("@" + fieldName))
                        this.AddParam(_cmdUpdate, fieldAttr, fieldName);
                    //    _cmdUpdate.Parameters.Add("@" + fieldName, fieldAttr.Type, fieldAttr.Size, fieldName);
                }

                //流水号字段名
                if (fieldAttr.IsDocFieldName) _DocNoFieldName = fieldName;

                //外键字段名
                if (fieldAttr.IsForeignKey) _ForeignFieldName = _ForeignFieldName + ";" + fieldName;//组合外键
            }
            _addOrUpdateCol = fieldArr;
            _sqlInsert = GernerateInsertSql(classAttribute.TableName, classAttribute.PrimaryKey, fieldArr, dbProviderType);//新增的SQL
            _sqlUpdate = GernerateUpdateSql(classAttribute.TableName, classAttribute.PrimaryKey, fieldArr, dbProviderType);//修改的SQL
            _sqlDelete = GernerateDeleteSql(classAttribute.TableName, classAttribute.PrimaryKey, fieldArr, dbProviderType);//删除的SQL

        }
        private void AddParam(DbCommand dbCommand, ORM_FieldAttribute ORM_FieldAttribute, string sourceColumn)
        {
            DbParameter dbp = dbCommand.CreateParameter();
            dbp.ParameterName = "@" + sourceColumn;
            dbp.DbType = ORM_FieldAttribute.Type;
            dbp.Size = ORM_FieldAttribute.Size;
            dbp.SourceColumn = sourceColumn;
            dbCommand.Parameters.Add(dbp);
        }
    }
    internal class GenerateDataBaseCommand : IGenerateDataBaseCommand
    {
        protected Type _ORMModel = null;
        protected DbProviderType _DbProviderType = DbProviderType.SqlServer;


        //Insert SQL命令
        protected DbCommand _cmdInsert = null;

        //Update SQL命令
        protected DbCommand _cmdUpdate = null;

        //Delete SQL命令
        protected DbCommand _cmdDelete = null;

        protected ArrayList _addOrUpdateCol = null;

        /// <summary>
        /// 单号字段名 
        /// </summary>
        protected string _DocNoFieldName = string.Empty;

        /// <summary>
        /// 主键字段名,复合主键用分号";"分开.
        /// </summary>
        protected string _PrimaryFieldName = string.Empty;

        /// <summary>
        /// 外键字段名,复合外键用分号";"分开.
        /// </summary>
        protected string _ForeignFieldName = string.Empty;

        /// <summary>
        /// 是否主表
        /// </summary>
        protected bool _IsSummary = true;
        internal bool IsConcurrentTable() { return _ConcurrentAttribute != null; }

        protected ORM_ConcurrentAttribute _ConcurrentAttribute = null;
        public string _sqlInsert
        {
            get; set;
        }

        public string _sqlUpdate
        {
            get; set;
        }

        public string _sqlDelete
        {
            get; set;
        }

        public DbCommand GetInsertCommand(DbConnection dbconn)
        {
            _cmdInsert.Connection = dbconn;
            _cmdInsert.CommandText = _sqlInsert;
            _cmdInsert.CommandType = System.Data.CommandType.Text;
            return _cmdInsert;
        }

        /// <summary>
        /// 生成更新记录的SQL命令
        /// </summary>
        /// <param name="tran"></param>
        /// <returns></returns>
        public DbCommand GetUpdateCommand(DbConnection dbconn)
        {
            _cmdUpdate.Connection = dbconn;
            _cmdUpdate.CommandText = _sqlUpdate;
            _cmdUpdate.CommandType = System.Data.CommandType.Text;
            return _cmdUpdate;
        }


        /// <summary>
        /// 生成删除记录的SQL命令
        /// </summary>
        /// <param name="tran"></param>
        /// <returns></returns>
        public DbCommand GetDeleteCommand(DbConnection dbconn)
        {
            _cmdDelete.Connection = dbconn;
            _cmdDelete.CommandText = _sqlDelete;
            _cmdDelete.CommandType = System.Data.CommandType.Text;
            return _cmdDelete;
        }

        public string GernerateDeleteSql(string tableName, string keyName, IList field, DbProviderType dbProviderType)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (dbProviderType == DbProviderType.SqlServer || dbProviderType == DbProviderType.MySql)
                {
                    sb.Append("DELETE FROM" + tableName + " WHERE 1=1 " + SplitKeyName(keyName));//复合主键处理
                }
                return sb.ToString();
            }
            catch
            { return string.Empty; }
        }

        public string GernerateInsertSql(string tableName, string keyName, IList field, DbProviderType dbProviderType)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                if (dbProviderType == DbProviderType.SqlServer)
                {
                    sb.Append("INSERT INTO " + tableName + " ( ");
                    foreach (ColumnProperty colProper in field)
                    {
                        sb.Append("[" + colProper.ColumnName + "],");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(" ) VALUES ( ");
                    foreach (ColumnProperty colProper in field)
                    {
                        sb.Append("@" + colProper.ColumnName + ",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(" )");
                }
                else if (dbProviderType == DbProviderType.MySql)
                {
                    sb.Append("INSERT INTO " + tableName + " ( ");
                    foreach (ColumnProperty colProper in field)
                    {
                        sb.Append("`" + colProper.ColumnName + "`,");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(" ) VALUES ( ");
                    foreach (ColumnProperty colProper in field)
                    {
                        sb.Append("@" + colProper.ColumnName + ",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(" )");
                }
                return sb.ToString();
            }
            catch
            { return string.Empty; }
        }

        public string GernerateUpdateSql(string tableName, string keyName, IList field, DbProviderType dbProviderType)
        {
            //生成SQL格式(复合主键)：UPDATE TABLE_NAME SET F1=@F1 WHERE KEY1=@KEY1 AND KEY2=@KEY2
            try
            {
                bool isConcurrent = false;
                StringBuilder sb = new StringBuilder();
                if (dbProviderType == DbProviderType.SqlServer)
                {
                    sb.Append("UPDATE " + tableName + " SET ");
                    foreach (ColumnProperty colProper in field)
                    {
                        //是否并发字段
                        isConcurrent = IsConcurrentTable() && _ConcurrentAttribute.TimestampFieldName == colProper.ColumnName;

                        //主键及并发字段不拼接SQL脚本
                        if (colProper.IsPrimaryKey || isConcurrent) continue;

                        sb.Append("[" + colProper.ColumnName + "]=@" + colProper.ColumnName + ",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(" WHERE 1=1 " + SplitKeyName(keyName));//主键
                    if (IsConcurrentTable()) sb.Append(" AND  " + GetConncurrentKeyName());//并发
                }
                else if (dbProviderType == DbProviderType.MySql)
                {
                    sb.Append("UPDATE " + tableName + " SET ");
                    foreach (ColumnProperty colProper in field)
                    {
                        //是否并发字段
                        isConcurrent = IsConcurrentTable() && _ConcurrentAttribute.TimestampFieldName == colProper.ColumnName;

                        //主键及并发字段不拼接SQL脚本
                        if (colProper.IsPrimaryKey || isConcurrent) continue;

                        sb.Append("`" + colProper.ColumnName + "`=@" + colProper.ColumnName + ",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(" WHERE 1=1 " + SplitKeyName(keyName));//主键
                    if (IsConcurrentTable()) sb.Append(" AND  " + GetConncurrentKeyName());//并发
                }
                return sb.ToString();
            }
            catch
            { return string.Empty; }
        }

        public string GetDocNoFieldName() { return _DocNoFieldName; }

        /// <summary>
        /// 获取主键字段名
        /// </summary>
        /// <returns></returns>
        public string GetPrimaryFieldName() { return _PrimaryFieldName; }

        /// <summary>
        /// 获取外键字段名
        /// </summary>
        /// <returns></returns>
        public string GetForeignFieldName() { return _ForeignFieldName; }

        /// <summary>
        /// 是否主表
        /// </summary>
        /// <returns></returns>
        public bool IsSummary() { return _IsSummary; }

        public ArrayList AddOrUpdateCol() { return _addOrUpdateCol; }

        protected virtual string SplitKeyName(string keyName)
        {
            StringBuilder strBuilder = new StringBuilder();
            string[] arrayStr = keyName.Split(';');
            foreach (string tempKeyName in arrayStr)
            {
                if (tempKeyName == "") continue;
                strBuilder.Append(" AND " + tempKeyName + "=@" + tempKeyName);
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// 取并发操作的字段名
        /// </summary>
        /// <returns></returns>
        protected virtual string GetConncurrentKeyName()
        {
            if (IsConcurrentTable())
                return _ConcurrentAttribute.TimestampFieldName + "=@" + _ConcurrentAttribute.TimestampFieldName;
            else
                return "";
        }

    }
    internal interface IGenerateDataBaseCommand
    {
        string _sqlInsert
        {
            get; set;
        }
        string _sqlUpdate
        {
            get; set;
        }
        string _sqlDelete
        {
            get; set;
        }
        DbCommand GetInsertCommand(DbConnection dbconn);

        /// <summary>
        /// 生成更新记录的SQL命令
        /// </summary>
        /// <param name="tran"></param>
        /// <returns></returns>
        DbCommand GetUpdateCommand(DbConnection dbconn);

        /// <summary>
        /// 生成删除记录的SQL命令
        /// </summary>
        /// <param name="tran"></param>
        /// <returns></returns>
        DbCommand GetDeleteCommand(DbConnection dbconn);
        string GernerateInsertSql(string tableName, string keyName, IList field, DbProviderType dbProviderType);


        string GernerateUpdateSql(string tableName, string keyName, IList field, DbProviderType dbProviderType);


        string GernerateDeleteSql(string tableName, string keyName, IList field, DbProviderType dbProviderType);

        /// <summary>
        /// 单据号码
        /// </summary>
        /// <returns></returns>
        string GetDocNoFieldName();

        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        string GetPrimaryFieldName();

        /// <summary>
        /// 外键
        /// </summary>
        /// <returns></returns>
        string GetForeignFieldName();

        /// <summary>
        /// 是否主表
        /// </summary>
        /// <returns></returns>
        bool IsSummary();

        ArrayList AddOrUpdateCol();
    }
}
