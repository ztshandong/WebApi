using System;
using System.Collections.Generic;
using WebApi.Core;

namespace WebApi.DAL
{
    internal class DataBaseConnFactory
    {
        private string _SQL_TEST_CONN = System.Configuration.ConfigurationManager.ConnectionStrings[WebApiGlobal._SQL_TEST_CONN].ConnectionString;
        private string _SQL_FIRS_CONN = System.Configuration.ConfigurationManager.ConnectionStrings[WebApiGlobal._SQL_OFFICIAL_CONN].ConnectionString;
        private string _SQL_SysDataBase_CONN = System.Configuration.ConfigurationManager.ConnectionStrings[WebApiGlobal._SQL_SYSTEM_CONN].ConnectionString;

        private static Dictionary<ChooseDataBase, string> Conn4StringDataBase = new Dictionary<ChooseDataBase, string>();
        private static Dictionary<ChooseDataBase, string> CheckDataBase4UserKey = new Dictionary<ChooseDataBase, string>();

        private static readonly Lazy<DataBaseConnFactory> lazy = new Lazy<DataBaseConnFactory>(() => new DataBaseConnFactory());
        internal static DataBaseConnFactory Instance { get { return lazy.Value; } }

        private DataBaseConnFactory()
        {
            Conn4StringDataBase.Add(ChooseDataBase.Test, _SQL_TEST_CONN);
            Conn4StringDataBase.Add(ChooseDataBase.Firs, _SQL_FIRS_CONN);
            Conn4StringDataBase.Add(ChooseDataBase.System, _SQL_SysDataBase_CONN);
            CheckDataBase4UserKey.Add(ChooseDataBase.Test, ChooseDataBase.Test.ToString());
            CheckDataBase4UserKey.Add(ChooseDataBase.Firs, ChooseDataBase.Firs.ToString());
            CheckDataBase4UserKey.Add(ChooseDataBase.System, ChooseDataBase.System.ToString());
        }
        internal string GetConnString(ChooseDataBase chooseDataBase)
        {
            return Conn4StringDataBase[chooseDataBase];
        }
        /// <summary>
        /// 调用BLL之前检验UserKey属于哪个数据库，防止跨数据库访问
        /// </summary>
        /// <param name="chooseDataBase"></param>
        /// <returns></returns>
        internal string GetUserKeyAndDataBase(ChooseDataBase chooseDataBase)
        {
            return CheckDataBase4UserKey[chooseDataBase];
        }
    }
}
