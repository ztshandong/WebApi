using System;
using System.Net.Http;

using WebApi.Core;

namespace WebApi.OWIN
{
    internal class NLogHandler : DelegatingHandler
    {
        //REF: http://blog.kkbruce.net/2012/05/aspnet-web-api-8-http-http-message.html
        //REF: http://bit.ly/16lpGKM
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
             HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                return base.SendAsync(request, cancellationToken).ContinueWith((task) =>
                {
                    HttpResponseMessage resp = task.Result as HttpResponseMessage;
                    resp.Headers.Remove("Server");
                    resp.Headers.Remove("X-Powered-By");
                    AutoNLog.Log4Trace(request, resp);

                    return resp;
                });
            }
            catch (Exception ex)
            {
                AutoNLog.Log4Exception(CustomErrorMessage.Trace发生异常.ToString(), ex);
                throw ex;
            }
        }
    }

}
