using Microsoft.Win32;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace WebApi.Public
{
    public static class CommonMethod
    {
        public static string GetWindowsServiceInstallPath(string ServiceName)
        {
            string key = @"SYSTEM\CurrentControlSet\Services\" + ServiceName;
            string path = Registry.LocalMachine.OpenSubKey(key).GetValue("ImagePath").ToString();
            //替换掉双引号 
            path = path.Replace("\"", string.Empty);

            FileInfo fi = new FileInfo(path);
            return fi.FullName;
            //return fi.FullName.Directory.ToString();
        }
        public static string UTCTS
        {
            get
            {
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return Convert.ToInt64(ts.TotalSeconds).ToString();
            }
        }
        public static int Asc(string character)
        {
            try
            {
                if (character.Length == 1)
                {
                    System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                    int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                    return (intAsciiCode);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static string Chr(int asciiCode)
        {
            try
            {
                if (asciiCode >= 0 && asciiCode <= 255)
                {
                    System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                    byte[] byteArray = new byte[] { (byte)asciiCode };
                    string strCharacter = asciiEncoding.GetString(byteArray);
                    return (strCharacter);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex) { return ""; }
        }
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            //request.Headers.Add(HttpContext.Current.Request.Headers);//报错
            #region 报头属性设置
            /*
            Accept由 Accept 属性设置。
            Connection由 Connection 属性和 KeepAlive 属性设置。
            Content-Length由 ContentLength 属性设置。
            Content-Type由 ContentType 属性设置。
            Expect由 Expect 属性设置。
            Date由系统设置为当前日期。
            Host由系统设置为当前主机信息。
            If-Modified-Since由 IfModifiedSince 属性设置。
            Range由 AddRange 方法设置。
            Referer由 Referer 属性设置。
            Transfer-Encoding由 TransferEncoding 属性设置（SendChunked 属性必须为 true）。
            User-Agent由 UserAgent 属性设置。
            */
            #endregion
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
            public static string GetResponse(HttpWebRequest request)
        {
            //request.ContentType = "text/html;charset=UTF-8";
            request.ContentType = "application/json;charset=UTF-8";
            //request.ContentType = "application/x-www-form-urlencoded";            
            request.Headers["Accept-Encoding"] = "gzip,deflate";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
        /// <summary>
        /// POST与PUT要用这个
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetResponse4P(HttpWebRequest request)
        {
            //request.ContentType = "text/html;charset=UTF-8";
            request.ContentType = "application/json;charset=UTF-8";
            //request.ContentType = "application/x-www-form-urlencoded";            
            request.Headers["Accept-Encoding"] = "gzip,deflate";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            string paraUrlCoded = request.RequestUri.Query;
            byte[] payload;
            payload = System.Text.UTF8Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
        public static string String2Unicode(string source)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(source);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }
        public static string Unicode2String(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                         source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }
        public static string StringToMD5Hash(string inputString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }
        public static string StringToSHA1Hash(string inputString)
        {
            SHA1 shaM = new SHA1Managed();
            byte[] encryptedBytes = shaM.ComputeHash(UTF8Encoding.UTF8.GetBytes(inputString));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }
        public static string StringToSHA256Hash(string inputString)
        {
            SHA256 shaM = new SHA256Managed();

            byte[] encryptedBytes = shaM.ComputeHash(UTF8Encoding.UTF8.GetBytes(inputString));
            //string s = Convert.ToBase64String(encryptedBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }
        public static string StringToSHA384Hash(string inputString)
        {
            SHA384 shaM = new SHA384Managed();
            byte[] encryptedBytes = shaM.ComputeHash(UTF8Encoding.UTF8.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }
        public static string StringToSHA512Hash(string inputString)
        {
            SHA512 shaM = new SHA512Managed();

            byte[] encryptedBytes = shaM.ComputeHash(UTF8Encoding.UTF8.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }
        public static DataSet Excel2DataSet(string path)
        {
            try
            {
                DataSet ds = new DataSet();
                string fileType = System.IO.Path.GetExtension(path);
                if (fileType == null) return null;
                string strConn = "";
                if (fileType == ".xls")
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                else
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";
                OleDbConnection conn = null;
                OleDbDataAdapter myCommand = null;
                using (conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    System.Data.DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);

                    foreach (DataRow dr in schemaTable.Rows)
                    {
                        string tableName = dr[2].ToString().Trim();

                        string strExcel = "";
                        strExcel = "Select   *   From   [" + tableName + "]";
                        myCommand = new OleDbDataAdapter(strExcel, strConn);

                        if (tableName == "_xlnm#_FilterDatabase") continue;
                        DataSet dstmp = new DataSet();
                        myCommand.Fill(dstmp, tableName);
                        ds.Tables.Add(dstmp.Tables[0].Copy());
                    }
                    return ds;
                }
            }
            catch (Exception ex) { throw ex; }
            //finally
            //{
            //    //if (conn.State == ConnectionState.Open)
            //    //{
            //    conn.Close();
            //    myCommand.Dispose();
            //    conn.Dispose();
            //    // }
            //}
            //return ds;
        }
    }
}
