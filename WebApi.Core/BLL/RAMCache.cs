using System;
using System.Data;
using System.IO;
using System.Reflection;

using WebApi.Core;
using WebApi.DAL;
using WebApi.Public;

namespace WebApi.BLL
{
    internal class RAMCache
    {
        private static readonly Lazy<RAMCache> lazy = new Lazy<RAMCache>(() => new RAMCache());
        internal static RAMCache Instance { get { return lazy.Value; } }
        private DataSet _CachedTables = new DataSet();

        internal DataSet CachedTables
        {
            get { return _CachedTables; }
        }
        internal void ClearCache()
        {
            _CachedTables.Tables.Clear();
            GC.Collect();
        }
        internal void RemoveCache(string tableName)
        {
            DataTable dt = _CachedTables.Tables[tableName];
            if (dt == null) return;
            if (_CachedTables.Tables.IndexOf(dt.TableName) >= 0)
                _CachedTables.Tables.Remove(dt.TableName);
        }
        internal void RefreshCacheByUspName(string uspName)
        {
            RemoveCache(uspName);
            FindByUspName(uspName);
        }
        internal void RefreshCacheByTableName(string tableName)
        {
            RemoveCache(tableName);
            FindByTableName(tableName);
        }
        internal void AddToCache(DataTable dt)
        {
            if (_CachedTables.Tables.IndexOf(dt.TableName) >= 0)
                _CachedTables.Tables.Remove(dt.TableName);
            _CachedTables.Tables.Add(dt.Copy());
        }

        private DataTable FindFromCache(string tableName)
        {
            return _CachedTables.Tables[tableName];
        }

        internal DataTable FindByUspName(string uspName)
        {
            MyLog log = new MyLog(WebApiGlobal._MyLogPath);
            log.log("开始查找");
            DataTable dt = FindFromCache(uspName);
            if (dt == null)
            {
                dt = GetCache.Instance.GetByUspName(uspName).Tables[0];
                dt.TableName = uspName;
                AddToCache(dt.Copy());
            }
            return dt;
        }
        internal DataTable FindByTableName(string tableName)
        {
            DataTable dt = FindFromCache(tableName);
            if (dt == null)
            {
                dt = GetCache.Instance.GetByTableName(tableName).Tables[0];
                dt.TableName = tableName;
                AddToCache(dt.Copy());
            }
            return dt;
        }
        internal DataTable FindByEnumName(string EnumName)
        {
            DataTable dt = FindFromCache(EnumName);
            if (dt == null)
            {
                ////WebApiServer.exe
                //Assembly myAssembly = Assembly.GetEntryAssembly();
                //string exepath1 = myAssembly.Location;
                //string exepath2 = Process.GetCurrentProcess().MainModule.FileName;
                //string exepath3 = CommonMethod.GetWindowsServiceInstallPath("WebApiServer");

                ////SelfHost是当前路径，但是基于系统服务全是C:\Windows\system32
                //string exepath4 = System.Environment.CurrentDirectory;
                //string exepath5 = Directory.GetCurrentDirectory();

                ////获取的是DLL文件名
                //var dllpath1 = this.GetType().Assembly.Location;

                //目录名
                var dirpath1 = AppDomain.CurrentDomain.BaseDirectory;
                string CoreDLL = dirpath1 + @"\WebApi.Core.dll";
                byte[] fileData = File.ReadAllBytes(CoreDLL);
                Assembly EnumTypeAss = Assembly.Load(fileData);
                Type EnumType = EnumTypeAss.GetType("WebApi.Core." + EnumName + "");
                dt = new DataTable(EnumName);
                dt.Columns.Add(EnumName);
                string[] EnumFields = Enum.GetNames(EnumType);
                foreach (var item in EnumFields)
                {
                    DataRow dr = dt.Rows.Add();
                    dr[EnumName] = item;
                }
                AddToCache(dt.Copy());
            }
            return dt;
        }
        internal void RefreshUserKey()
        {
            RemoveCache(TbName.dt_Api_KeyAndSalt);
            DataTable dt = UserKeyAndSalt;
        }
        internal DataTable UserKeyAndSalt
        {
            get
            {
                DataTable dt = FindFromCache(TbName.dt_Api_KeyAndSalt);
                if (dt == null)
                {
                    dt = GetCache.Instance.GetUserKeyAndSalt().Tables[0];
                    dt.TableName = TbName.dt_Api_KeyAndSalt;

                    foreach (DataRow dr in dt.Rows)
                    {
                        string UserKey = dr[WebApiGlobal._USERKEY].ToString();
                        string UserSalt = dr[WebApiGlobal._USERSALT].ToString();
                        string EncryptCode = dr[WebApiGlobal._ENCTYPTCODE].ToString();
                        try
                        {
                            RijndaelEncryption.Instance.SetKeyAndIv(EncryptCode);

                            UserSalt = RijndaelEncryption.Instance.Decrypto(UserSalt);
                            string OriSalt = UserSalt.Substring(0, UserSalt.Length / 4);
                            dr[WebApiGlobal._USERSALT] = OriSalt;

                            UserKey = RijndaelEncryption.Instance.Decrypto(UserKey);
                            string OriKey = UserKey.Substring(0, UserKey.Length / 4);
                            dr[WebApiGlobal._DECODE_USERKEY] = OriKey;
                            dr[WebApiGlobal._USERKEY] = CommonMethod.StringToSHA256Hash(OriKey);
                        }
                        catch (Exception ex)
                        {
                            AutoNLog.Log4Exception(CustomErrorMessage.解密发生异常.ToString() + EncryptCode, ex);
                        }
                    }
                    AddToCache(dt.Copy());
                }
                return dt;
            }
        }
        internal DataTable CarSize
        {
            get
            {
                DataTable dt = FindFromCache("usp_WebApi_Common_GetCarSize");
                if (dt == null)
                {
                    dt = GetCache.Instance.GetByUspName("usp_WebApi_Common_GetCarSize").Tables[0];
                    dt.TableName = "usp_WebApi_Common_GetCarSize";
                    AddToCache(dt.Copy());
                }
                return dt;
            }
        }
        internal DataTable ChinaCity
        {
            get
            {
                DataTable dt = FindFromCache("usp_WebApi_Common_GetChinaCity");
                if (dt == null)
                {
                    dt = GetCache.Instance.GetByUspName("usp_WebApi_Common_GetChinaCity").Tables[0];
                    dt.TableName = "usp_WebApi_Common_GetChinaCity";
                    AddToCache(dt.Copy());
                }
                return dt;
            }
        }
        internal DataTable TransportType
        {
            get
            {
                DataTable dt = FindFromCache("usp_WebApi_Common_GetTransportType");
                if (dt == null)
                {
                    dt = GetCache.Instance.GetByUspName("usp_WebApi_Common_GetTransportType").Tables[0];
                    dt.TableName = "usp_WebApi_Common_GetTransportType";
                    AddToCache(dt.Copy());
                }
                return dt;
            }
        }
    }


}
