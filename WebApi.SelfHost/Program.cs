using System;
using Microsoft.Owin.Hosting;
using WebApi.Core;

namespace WebApi.SelfHost
{
    class Program
    {
        private static string _WEB_API_SERVER_ADD = "http://localhost:8341/";
        static void Main(string[] args)
        {
            try
            {
                //System.Windows.Forms.Application.Run(new Form1());
                AutoNLog.Log4Info("开始");
                using (WebApp.Start<Startup>(url: _WEB_API_SERVER_ADD))
                {
                    //HttpClient client = new HttpClient();
                    //HttpResponseMessage response = client.GetAsync("http://localhost:9527/api/Help/GetCarSize").Result;
                    //response.Headers.Remove("Server");
                    //Console.WriteLine(response);
                    //Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                    AutoNLog.Log4Info("成功");
                    Console.ReadLine();
                }

            }
            catch (Exception ex)
            {
                AutoNLog.Log4Exception("WebApiSelfHost发生异常：", ex);
            }
        }
    }
}
