using System;
using System.ServiceProcess;
using Microsoft.Owin.Hosting;
using WebApi.Core;

namespace WebApi.WinService
{
    partial class WebApiService : ServiceBase
    {
        private IDisposable _server = null;
        private string _WEB_API_SERVER_ADD = "http://localhost:8341/";
        public WebApiService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //#if (DEBUG)
                //     Debugger.Launch();
                //#endif
                _server = WebApp.Start<Startup>(url: _WEB_API_SERVER_ADD);
                AutoNLog.Log4Info("成功启动WebApiServer");
            }
            catch (Exception ex)
            {
                AutoNLog.Log4Exception("启动WebApiServer失败，", ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                if (_server != null)
                {
                    _server.Dispose();
                }
                base.OnStop();
                AutoNLog.Log4Info("成功停止WebApiServer");
            }
            catch (Exception ex)
            {
                AutoNLog.Log4Exception("停止WebApiServer失败，", ex);
            }
        }
    }
}
