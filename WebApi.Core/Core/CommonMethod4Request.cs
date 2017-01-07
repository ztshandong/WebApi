using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Net.Http;
using WebApi.Public;
namespace WebApi.Core
{
    internal static class CommonMethod4Request
    { 
      internal static void AddHeader4GetData(HttpWebRequest webrequest, HttpRequestMessage httpRequest, bool useTestKey = false)
        {
            webrequest.Headers.Clear();

            foreach (var item in httpRequest.Headers)
            {
                string headerName = item.Key.ToString();
                string headerValue = item.Value.ToArray()[0].ToString();
                CommonMethod.SetHeaderValue(webrequest.Headers, headerName, headerValue);
            }

            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            webrequest.Headers.Add(WebApiGlobal._CLIENT_IP, ip);
            //string requestUri = httpRequest.RequestUri.AbsoluteUri;
            string orirequestUri = httpRequest.RequestUri.OriginalString;
            string orirequestUricode = System.Web.HttpUtility.UrlPathEncode(orirequestUri);
            string orirequestUriDecode = System.Web.HttpUtility.UrlDecode(orirequestUri);
            webrequest.Headers.Add(WebApiGlobal._ORI_REQUEST_URL, orirequestUricode);

            string TS = httpRequest.Headers.GetValues(WebApiGlobal._TIMESPAN).ToArray()[0];
            if (useTestKey)
            {
                TS = CommonMethod.UTCTS;
                string UserKey = AuthKeys._ROUTE_HELP_GET_COMMON_KEY;
                string SHA256Sign = CommonMethod.StringToSHA256Hash(UserKey + orirequestUriDecode + TS + AuthKeys._ROUTE_HELP_GET_COMMON_SALT);
                webrequest.Headers.Set(WebApiGlobal._TIMESPAN, TS);
                webrequest.Headers.Set(WebApiGlobal._USERKEY, CommonMethod.StringToSHA256Hash(UserKey));
                webrequest.Headers.Set(WebApiGlobal._SHA256, SHA256Sign);
            }
        }
  
        internal static string RequestUri2JSON(List<object> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var classes in list)
            {
                Type T = classes.GetType();
                PropertyInfo[] properties = T.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    object propvalue = property.GetValue(classes);
                    string propname = property.Name;

                    if (!propvalue.IsNullOrEmpty())
                    {
                        if (property.PropertyType == typeof(int) || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(float)
                           || property.PropertyType == typeof(int?) || property.PropertyType == typeof(decimal?) || property.PropertyType == typeof(float?))
                        {
                            if (propvalue.ToDecimalEx() == 0) continue;
                        }
                        if (sb.Length <= 0)
                        {
                            sb.Append(propname + "=" + propvalue);
                        }
                        else
                        {
                            sb.Append("&" + propname + "=" + propvalue);
                        }
                    }
                }
            }
            if (sb.Length > 0)
                sb.Insert(0, "?");
            return sb.ToString();
        }
    
    }
}
