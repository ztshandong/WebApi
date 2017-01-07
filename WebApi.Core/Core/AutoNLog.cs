using System;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using Newtonsoft.Json;
using NLog;
using WebApi.Models;
using WebApi.Public;

namespace WebApi.Core
{
    public class AutoNLog
    {
        private static Logger _web_Api_NLog = LogManager.GetCurrentClassLogger();
        public static void Log4Info(string infoDescription)
        {
            Info4NLog logger = new Info4NLog();
            logger.InfoDescription = infoDescription;
            string info4log = JsonConvert.SerializeObject(logger);
            _web_Api_NLog.Log(LogLevel.Info, info4log);
            
        }
        public static void Log4Warn(string infoDescription)
        {
            Info4NLog logger = new Info4NLog();
            logger.InfoDescription = infoDescription;
            string info4log = JsonConvert.SerializeObject(logger);
            _web_Api_NLog.Log(LogLevel.Warn, info4log);
        }
        public static void Log4Exception(string errDescription, Exception ex)
        {
            Exception4NLog logger = new Exception4NLog();
            logger.ErrDescription = errDescription;
            logger.ExMessage = ex.Message;
            if (ex.InnerException != null)
            {
                logger.InnerExMessage = ex.InnerException.Message;
                logger.InnerExSource = ex.InnerException.Source;
            }
            string err4log = JsonConvert.SerializeObject(logger);
            _web_Api_NLog.Log(LogLevel.Error, err4log);
            MyLog log = new MyLog(WebApiGlobal._MyLogPath);
            log.log(err4log);
        }
        public static void Log4Trace(HttpRequestMessage request, HttpResponseMessage resp)
        {
            string ip = "";
            if (request.Properties.ContainsKey("MS_OwinContext"))
            {
                // ipadd = Context.Request.Environment["server.RemoteIpAddress"].ToStri‌​ng();
                ip = ((Microsoft.Owin.OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress;
            }
            else if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                ip = ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                ip = ((RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name]).Address;
            }
            if (!ip.IsIP())
                ip = "未知IP" + ip;

            Trace4NLog logger = new Trace4NLog();
            logger.methodJSON = request.Method.ToStringEx();
            logger.urlJSON = request.RequestUri.ToStringEx();
            logger.clientIPJSON = ip;
            logger.respStatusCodeInt = (int)resp.StatusCode;
            logger.respStatusCodeString = resp.StatusCode.ToStringEx();
            try
            {
                logger.userkey = request.Headers.GetValues(WebApiGlobal._USERKEY).ToArray()[0];
            }
            catch (Exception ex)
            {
                logger.userkey = CustomErrorMessage.没有UserKey.ToString();
            }
            try
            {
                logger.requesturl = request.Headers.GetValues(WebApiGlobal._ORI_REQUEST_URL).ToArray()[0];
            }
            catch (Exception ex)
            {
                logger.requesturl = CustomErrorMessage.没有URL.ToString();
            }
            try
            {
                logger.clientIPJSON = request.Headers.GetValues(WebApiGlobal._CLIENT_IP).ToArray()[0] + "->" + ip;
            }
            catch (Exception ex)
            {
                logger.clientIPJSON = CustomErrorMessage.没有IP.ToString();
            }
            string trace4log = JsonConvert.SerializeObject(logger);

            _web_Api_NLog.Log(LogLevel.Trace, trace4log);
        }
    }
}
