using System;
using System.Data;
using System.Data.Common;

using WebApi.Core;

namespace WebApi.DAL
{
    internal class GetCache
    {
        private static readonly Lazy<GetCache> lazy = new Lazy<GetCache>(() => new GetCache());
        internal static GetCache Instance { get { return lazy.Value; } }
        internal DataSet GetUserKeyAndSalt()
        {
            DbProvider4DAL dbp = new DbProvider4DAL("usp_sys_Api_GetUserKeyAndSalt", ChooseDataBase.System, DbProviderType.SqlServer);
            DataSet ds = new DataSet();
            ds = DataProvider.Instance.GetDataSet(dbp);
            return ds;
        }
        internal DataSet GetByUspName(string uspName)
        {

            DbProvider4DAL dbp = new DbProvider4DAL(uspName, ChooseDataBase.Firs, DbProviderType.SqlServer);

            DataSet ds = new DataSet();
            ds = DataProvider.Instance.GetDataSet(dbp);
            return ds;
        }
        internal DataSet GetByTableName(string tableName)
        {
            DbProvider4DAL dbpro = new DbProvider4DAL("usp_WebApi_TableNameAsParam", ChooseDataBase.Firs, DbProviderType.SqlServer);
            DbParameter dbp = dbpro.CurrentCommand.CreateParameter();
            dbp.ParameterName = "@TableName";
            dbp.Value = tableName;
            dbpro.CurrentCommand.Parameters.Add(dbp);
            DataSet ds = new DataSet();
            ds = DataProvider.Instance.GetDataSet(dbpro);
            return ds;
        }
    }
}
