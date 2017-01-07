using System.Data;
using System.Transactions;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.DAL
{
    public abstract partial class DALBase : IApiCRUDBaseInterface4DAL
    {
        //protected virtual IGenerateDataBaseCommand CreateDataBaseGenerator(string tableName, DataTable dt, DbProviderType dbProviderType) { return null; }
        protected bool _UserManualControlTrans = false;
        protected TransactionScope _Scope = null;
        //  internal DbProvider4DAL _DbProvider4DAL { get; set; } = new DbProvider4DAL();
        internal DALBase() { }

        public abstract DataSet Get(Params4ApiCRUD params4ApiCRUD);
        //{
        //    throw new NotImplementedException();
        //}

        public abstract DataSet Search(Params4ApiCRUD params4ApiCRUD);
        //{
        //    throw new NotImplementedException();
        //}

        public abstract DataSet Post(Params4ApiCRUD params4ApiCRUD);
        //{
        //    throw new NotImplementedException();
        //}

        public abstract DataSet Put(Params4ApiCRUD params4ApiCRUD);
        //{
        //    throw new NotImplementedException();
        //}

        public abstract DataSet Delete(Params4ApiCRUD params4ApiCRUD);
        //{
        //    throw new NotImplementedException();
        //}








        //private void SqlBulkCopyInsert(DataTable table, string tableName, string[] columns, SqlConnection sqlConnection)
        //{
        //    SqlBulkCopy sbc = new SqlBulkCopy(sqlConnection);
        //    if (sbc == null) throw new Exception("创建SqlBulkCopy失败");
        //    sbc.DestinationTableName = tableName;
        //    foreach (string col in columns)
        //    {
        //        sbc.ColumnMappings.Add(col, col);
        //    }
        //    sbc.WriteToServer(table);
        //}



        //internal virtual void UpdateDataSet(DataSet ds)
        //{
        //    try
        //    {
        //        if (_UserManualControlTrans == false)
        //        {
        //            using (_scope = new TransactionScope())
        //            {
        //                using (_DbProvider4DAL._CurrentConnection = ProviderFactory.GetDbProviderFactory(_DbProvider4DAL._CurrentDbProviderType).CreateConnection())
        //                {
        //                    _DbProvider4DAL.Connect();
        //                    if (ds.Tables[0].Rows[0].RowState == DataRowState.Added && _DbProvider4DAL._CurrentDbProviderType == DbProviderType.SqlServer)
        //                    {
        //                        List<string> list = new List<string>();
        //                        foreach (DataTable dt in ds.Tables)
        //                        {
        //                            IGenerateDataBaseCommand gend1 = this.CreateDataBaseGenerator(dt.TableName, dt, _DbProvider4DAL._CurrentDbProviderType);
        //                            foreach (ColumnProperty colProper in gend1.AddOrUpdateCol())
        //                            {
        //                                list.Add(colProper.ColumnName);
        //                            }
        //                            SqlBulkCopyInsert(dt, dt.TableName, list.ToArray(), _DbProvider4DAL._CurrentConnection as SqlConnection);
        //                            list.Clear();
        //                            dt.AcceptChanges();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        foreach (DataTable dt in ds.Tables)
        //                        {
        //                            IGenerateDataBaseCommand gend1 = this.CreateDataBaseGenerator(dt.TableName, dt, _DbProvider4DAL._CurrentDbProviderType);

        //                            //_DbProvider4DAL._CurrentDbDataAdapter.InsertCommand.CommandText = gend1._sqlInsert;
        //                            _DbProvider4DAL._CurrentDbDataAdapter.InsertCommand = gend1.GetInsertCommand(_DbProvider4DAL._CurrentConnection);
        //                            //_DbProvider4DAL._CurrentDbDataAdapter.UpdateCommand.CommandText = gend1._sqlUpdate;
        //                            _DbProvider4DAL._CurrentDbDataAdapter.UpdateCommand = gend1.GetUpdateCommand(_DbProvider4DAL._CurrentConnection);
        //                            //_DbProvider4DAL._CurrentDbDataAdapter.DeleteCommand.CommandText = gend1._sqlDelete;
        //                            _DbProvider4DAL._CurrentDbDataAdapter.DeleteCommand = gend1.GetDeleteCommand(_DbProvider4DAL._CurrentConnection);
        //                            _DbProvider4DAL._CurrentDbDataAdapter.Update(dt);
        //                            dt.AcceptChanges();
        //                        }
        //                    }
        //                }
        //                if (_UserManualControlTrans == false) _scope.Complete();
        //            }
        //        }
        //    }
        //    catch (DBConcurrencyException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _DbProvider4DAL.DisConnect();
        //    }
        //}










    }


}

















