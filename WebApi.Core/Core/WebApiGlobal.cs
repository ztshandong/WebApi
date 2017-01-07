using System;

namespace WebApi.Core
{
    internal class WebApiGlobal
    {
        internal const string _SQL_TEST_CONN = "SQL_Test";
        internal const string _SQL_OFFICIAL_CONN = "SQL_OFFICIAL";
        internal const string _SQL_SYSTEM_CONN = "SQL_SYSTEM";

        internal static string _MyLogPath = AppDomain.CurrentDomain.BaseDirectory + @"\MyLog.txt";

        internal const string _ROUTE_PREFIX_TMS_DD = "api/TMS_DD";
        internal const string _ROUTE_PREFIX_HELP = "api/HELP";
        internal const string _ROUTE_PREFIX_RAM_MGR = "api/RAM/Mgr";
        internal const string _ROUTE_REFRESH_USERKEY = "RefreshUserKey";
        internal const string _ROUTE_REFRESH_ALL_CACHE = "RefreshAllCache";

        internal const string _ROUTE_VERSION1 = "V1/";
        internal const string _ROUTE_SEARCH = "Search";
        internal const string _ROUTE_GET = "Get";
        internal const string _ROUTE_POST = "Post";
        internal const string _ROUTE_PUT = "Put";
        internal const string _ROUTE_DELETE = "Delete";

        internal const string _ROUTE_TEST_SEARCH = "Test/Search";
        internal const string _ROUTE_TEST_GET = "Test/Get";
        internal const string _ROUTE_TEST_POST = "Test/Post";
        internal const string _ROUTE_TEST_PUT = "Test/Put";
        internal const string _ROUTE_TEST_DELETE = "Test/Delete";

        internal const string _API_HELP_CRUD_PARAM_USP_NAME = "uspName";
        internal const string _API_HELP_CRUD_PARAM_ENUM_NAME = "enumName";

        internal const string _ROUTE_HELP_GET_COMMON_BY_USP = "Search4CommonByUspName";
        internal const string _ROUTE_HELP_GET_COMMON_BY_ENUM_NAME = "Search4CommonByEnumName";


        internal const string _ROUTE_HELP_GET_HASH = "GetHash";
        internal const string _ROUTE_HELP_GET_URLDECODE = "GetUrlDecode";
        internal const string _ROUTE_HELP_GET_TS = "GetTS";
        internal const string _ROUTE_HELP_GET_CHINA_CITY = "GetChinaCity";

        internal const string _USERKEY = "UserKey";
        internal const string _USERSALT = "UserSalt";
        internal const string _DATABASENAME = "DataBaseName";
        internal const string _DECODE_USERKEY = "OriKey";
        internal const string _SHA256KEY = "SHA256Key";
        internal const string _USERCODE = "UserCode";
        internal const string _ENCTYPTCODE = "EncryptCode";
        internal const string _TIMESPAN = "TS";
        internal const string _SHA256 = "HashSign";
        internal const string _CLIENT_IP = "ClientIP";
        internal const string _ORI_REQUEST_URL = "OriRequestUri";
        internal static DateTime MinSqlDate { get { return DateTime.Parse("1901-01-01 00:00"); } }
    }

    internal class TbName
    {
        internal const string tb_TMS_DD = "tb_TMS_DD";
        internal const string dt_Api_KeyAndSalt = "dt_Api_KeyAndSalt";
    }
}
