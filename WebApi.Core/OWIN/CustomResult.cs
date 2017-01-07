using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
//using Guards;

namespace WebApi.OWIN
{
    //internal class RemoveHttpHeadersModule : IHttpModule
    //{
    //    internal void Init(HttpApplication context)
    //    {
    //        Guard.ArgumentNotNull(context, "context");

    //        context.PreSendRequestHeaders += OnPreSendRequestHeaders;
    //    }

    //    internal void Dispose() { }

    //    void OnPreSendRequestHeaders(object sender, EventArgs e)
    //    {
    //        var application = sender as HttpApplication;

    //        if (application != null)
    //        {
    //            HttpResponse response = application.Response;
    //            response.Headers.Remove("Server");
    //            response.Headers.Remove("X-Powered-By");
    //        }
    //    }
    //}
    internal class MethodOverrideHandler : DelegatingHandler
    {
        readonly string[] _methods = { "DELETE", "HEAD", "PUT" };
        const string _header = "X-HTTP-Method-Override";

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Check for HTTP POST with the X-HTTP-Method-Override header.
            if (request.Method == HttpMethod.Post && request.Headers.Contains(_header))
            {
                // Check if the header value is in our methods list.
                var method = request.Headers.GetValues(_header).FirstOrDefault();
                if (_methods.Contains(method, StringComparer.InvariantCultureIgnoreCase))
                {
                    // Change the request method.
                    request.Method = new HttpMethod(method);
                }
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
    #region .NET 4.5
    //internal class CustomHeaderHandler : DelegatingHandler
    //{
    //    async protected override Task<HttpResponseMessage> SendAsync(
    //            HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
    //        response.Headers.Add("X-Custom-Header", "This is my custom header.");
    //        return response;
    //    }
    //}
    #endregion

    #region .NET 4.0
    internal class CustomHeaderHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith(
                (task) =>
                {
                    HttpResponseMessage response = task.Result;
                    response.Headers.Remove("Server");
                    response.Headers.Remove("X-Powered-By");
                    //response.Headers.Add("X-Custom-Header", "This is my custom header.");
                    return response;
                }
            );
        }
    }
    #endregion
    internal class MessageHandler1 : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Process request");
            // Call the inner handler.
            var response = await base.SendAsync(request, cancellationToken);
            response.Headers.Remove("Server");
            Debug.WriteLine("Process response");
            return response;
        }
    }
    //internal class MessageHandler2 : DelegatingHandler
    //{
    //    protected override Task<HttpResponseMessage> SendAsync(
    //        HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        // Create the response.
    //        var response = new HttpResponseMessage(HttpStatusCode.OK)
    //        {                
    //            Content = new StringContent("Hello!")
    //        };

    //        // Note: TaskCompletionSource creates a task that does not contain a delegate.
    //        var tsc = new TaskCompletionSource<HttpResponseMessage>();
    //        tsc.SetResult(response);   // Also sets the task state to "RanToCompletion"
    //        return tsc.Task;
    //    }
    //}

    internal class ApiKeyHandler : DelegatingHandler
    {
        internal string Key { get; set; }

        internal ApiKeyHandler(string key)
        {
            this.Key = key;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!ValidateKey(request))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }

        private bool ValidateKey(HttpRequestMessage message)
        {
            var query = message.RequestUri.ParseQueryString();
            string key = query["key"];
            return (key == Key);
        }
    }

}
