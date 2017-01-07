using System;
using System.Data;
using System.Transactions;
using WebApi.Core;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.DAL
{
    internal class dal_TMS_DD<TDALBase> : DALBase, Interface4tb_TMS_DD4DAL where TDALBase : Interface4tb_TMS_DD4DAL //,new()
    {
        //protected  IGenerateDataBaseCommand CreateDataBaseGenerator(string tableName, DataTable dt, DbProviderType dbProviderType)
        //{
        //    Type ORM = null;
        //    if (tableName == dt_test.__TableName) ORM = typeof(dt_test);
        //    if (ORM == null) throw new Exception(tableName + "表没有ORM模型，请生成此表的Model类！");
        //    return new GenerateDataBaseCommandByTableFields(ORM, dt, dbProviderType);
        //}
        public override DataSet Get(Params4ApiCRUD P)
        {
            DbProvider4DAL dbp = new DAL.DbProvider4DAL("usp_WebApi_Get_tb_TMS_DD", P.chooseDataBase, P.dbProviderType);
            DataProvider.Instance.AddDbParamsByPropertyInfo(dbp.CurrentCommand, P.fromUri);
            DataProvider.Instance.AddDbParamsByCustom(dbp.CurrentCommand, "@CustomerCode", P.UserCode, DbType.String, 32);
            DataSet ds = new DataSet();
            ds = DataProvider.Instance.GetDataSet(dbp);
            ds.Tables[0].TableName = TbName.tb_TMS_DD;
            return ds;
        }
        public override DataSet Post(Params4ApiCRUD P)
        {
            tb_TMS_DD dd = P.fromUri as tb_TMS_DD;
            DbProvider4DAL dbp = new DAL.DbProvider4DAL("usp_WebApi_Post_tb_TMS_DD", P.chooseDataBase, P.dbProviderType);
            DataProvider.Instance.AddDbParamsByPropertyInfo(dbp.CurrentCommand, dd);
            DataProvider.Instance.AddDbParamsByCustom(dbp.CurrentCommand, "@CustomerCode", P.UserCode, DbType.String, 32);
            DataSet ds = new DataSet();
            using (_Scope = new TransactionScope())
            {
                ds = DataProvider.Instance.GetDataSet(dbp);
                _Scope.Complete();
            }
            return ds;
        }
        public override DataSet Put(Params4ApiCRUD P)
        {
            tb_TMS_DD dd = P.fromUri as tb_TMS_DD;
            DbProvider4DAL dbp = new DAL.DbProvider4DAL("usp_WebApi_Put_tb_TMS_DD", P.chooseDataBase, P.dbProviderType);
            DataProvider.Instance.AddDbParamsByPropertyInfo(dbp.CurrentCommand, dd);
            DataProvider.Instance.AddDbParamsByCustom(dbp.CurrentCommand, "@CustomerCode", P.UserCode, DbType.String, 32);
            DataSet ds = new DataSet();
            using (_Scope = new TransactionScope())
            {
                ds = DataProvider.Instance.GetDataSet(dbp);
                _Scope.Complete();
            }
            return ds;
        }
        public override DataSet Delete(Params4ApiCRUD P)
        {
            DbProvider4DAL dbp = new DAL.DbProvider4DAL("usp_WebApi_Delete_tb_TMS_DD", P.chooseDataBase, P.dbProviderType);
            DataProvider.Instance.AddDbParamsByPropertyInfo(dbp.CurrentCommand, P.fromUri);
            DataProvider.Instance.AddDbParamsByCustom(dbp.CurrentCommand, "@CustomerCode", P.UserCode, DbType.String, 32);
            DataSet ds = new DataSet();
            using (_Scope = new TransactionScope())
            {
                ds = DataProvider.Instance.GetDataSet(dbp);
                _Scope.Complete();
            }
            return ds;
        }

        public override DataSet Search(Params4ApiCRUD params4ApiCRUD)
        {
            throw new NotImplementedException();
        }

    }





}
