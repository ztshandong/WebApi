using System;
using System.Web.Http;
using Owin;
using System.Web.Http.Routing;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Owin.Extensions;
using WebApi.Core;
using WebApi.Public;

namespace WebApi.OWIN
{

    public class IniControllerType
    {
        //   http://stackoverflow.com/a/17227764/19020 
        //  http://www.asp.net/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2

        public static void Ini(IAppBuilder appBuilder)
        {
            //DateTime dt = DateTime.Parse("2017-01-02");
            //try
            //{
            //    DateTime dt = GetNetTime.GetStandardTime();
            //    SetNetTime.SetTime(GetNetTime.GetStandardTime());
            //}
            //catch (Exception ex)
            //{
            //    AutoNLog.Log4Info("同步时间失败" + ex);
            //}
            //string authKey = System.Configuration.ConfigurationManager.ConnectionStrings[WebApiGlobal._AUTHKEY].ConnectionString;


            try
            {
                //DateTime dt = GetNetTime.GetStandardTime();
                //SetNetTime.SetTime(dt);
                MyLicense lic = ReadFromLic.ReadLic(AuthKeys._AuthFileKEY, AuthKeys._AuthFilePath);
                if (lic == null)
                {
                    throw new Exception("未解密Lic");
                }
                string CpuID = GetComputerInfo.GetCpuID();
                string HDid = GetComputerInfo.GetHDid();
                string ComputerInfo = CpuID + HDid + lic.PeriodDate.ToStringEx() + "4x}ty#N3*w[2bXK2ne(DRLKov%NhmJ#Z";
                RSAAuth _RSA = new RSAAuth();
                string _PublicKey = _RSA.ReadPublicKey(AuthKeys._PublicKeyPath);
                string Hash1 = _RSA.GetSHA512Hash(ComputerInfo);
                string Hash4Validate = _RSA.GetSHA512Hash(Hash1);
                if (_RSA.SignatureDeformatter(_PublicKey, Hash4Validate, lic.SignValue.ToStringEx()))
                {
                    if (DateTime.Parse(lic.PeriodDate) < DateTime.Now)
                        throw new Exception("已过期,PeriodDate:" + lic.PeriodDate.ToStringEx() + "SignValue:" + lic.SignValue.ToStringEx());
                }
                else
                {
                    throw new Exception("非法使用,PeriodDate:" + lic.PeriodDate.ToStringEx() + "SignValue:" + lic.SignValue.ToStringEx());
                }
            }
            catch (Exception ex)
            {
                AutoNLog.Log4Info("检验错误" + ex.Message.ToStringEx() + ex);
                throw ex;
            }

            Type RAMCacheController = typeof(WebApi.Controller.RAMCacheController);
            Type web_Api_HelperController = typeof(WebApi.Controller.web_Api_HelperController);
            Type tb_TMS_DDController = typeof(WebApi.Controller.TMS_DDController);
            HttpConfiguration config = new HttpConfiguration();

            config.Formatters.JsonFormatter.SerializerSettings.Formatting =
       Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;


            appBuilder.Use((context, next) =>
            {
                context.Response.Headers.Remove("Server");

                return next.Invoke();
            });
            appBuilder.UseStageMarker(PipelineStage.PostAcquireState);
            //            // List of delegating handlers.
            //            DelegatingHandler[] handlers = new DelegatingHandler[] {
            //            new MessageHandler3()
            //            };        

            //            // Create a message handler chain with an end-point.
            //            var routeHandlers = HttpClientFactory.CreatePipeline(
            //                new HttpControllerDispatcher(config), handlers);

            // Web API 配置和服务
            // 将 Web API 配置为仅使用不记名令牌身份验证。
            config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
           
            //config.MapHttpAttributeRoutes();
            //可将路由放到基类中
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());

            //没有action，则两个方法都是Get开头会报错，改为api/{controller}/{action}/{id}
            config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                    //constraints: null,
                    //handler: new MessageHandler1()
                    //handler: routeHandlers
                    );


            config.Filters.Add(new MyActionFilterAttribute());
            config.Filters.Add(new MyExceptionHandlingAttribute());
            //var traceWriter = new SystemDiagnosticsTraceWriter()
            //{
            //    IsVerbose = true
            //};
            ////config.Services.Replace(typeof(ITraceWriter), traceWriter);
            //config.EnableSystemDiagnosticsTracing();
            //config.MessageHandlers.Add(new ThrottlingHandler()
            //{
            //    Policy = new ThrottlePolicy(perSecond: 1, perMinute: 30)
            //    {
            //        //Ip限流，访问api/values后，所有后续访问api/values/xxx的请求都会被拒绝掉
            //        IpThrottling = true,
            //        //IpWhitelist = new List<string> { "::1", "192.168.0.0/24" },
            //        IpRules = new Dictionary<string, RateLimits>
            //        {
            //            { "192.168.1.1", new RateLimits { PerSecond = 2 } },
            //            { "192.168.2.0/24", new RateLimits { PerMinute = 30, PerHour = 30*60, PerDay = 30*60*24 } }
            //        },
            //        //如果同一个ip，在同一秒内，调用了2次api/values，其最后一次的调用将会被拒绝掉。
            //        //如果想接口通过唯一key去识别限制客户端，忽略客户端的ip地址限制，应该配置IpThrottling为false。
            //        ClientThrottling = true,
            //        ClientWhitelist = new List<string> { "admin-key" },
            //        ClientRules = new Dictionary<string, RateLimits>
            //        {
            //            { "api-client-key-1", new RateLimits { PerMinute = 40, PerHour = 400 } },
            //            { "api-client-key-9", new RateLimits { PerDay = 2000 } }
            //        },
            //        EndpointRules = new Dictionary<string, RateLimits>
            //        {
            //            { "api/RAM/Mgr/RefreshUserKey", new RateLimits { PerSecond = 2, PerMinute = 100, PerHour = 1000 } }
            //        },
            //        //拒绝累加技术
            //        StackBlockedRequests = true,
            //        //Ip端点限流，同一秒内你也访问api/values/1了，请求将不会被拒绝，因为它们走的是不同的路由。
            //        EndpointThrottling = true
            //    },
            //    //如果是owin寄宿，替换成PolicyMemoryCacheRepository
            //    //PolicyRepository= new PolicyMemoryCacheRepository(),
            //    //policyRepository: new PolicyCacheRepository(),
            //    //自寄宿在Owin上的WebApi用MemoryCacheRepository
            //    Repository = new MemoryCacheRepository(),
            //    Logger= new TracingThrottleLogger(traceWriter)
            //    //Repository = new CacheRepository()//CacheRepository使用的是Asp.net版本的缓存。
            //}
            //);
            ////config.MessageHandlers.Add(new CustomHeaderHandler());
            ////config.MessageHandlers.Add(new MessageHandler1());
            ////config.MessageHandlers.Add(new RemoveHttpHeadersModule());




            appBuilder.UseWebApi(config);
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.MessageHandlers.Add(new NLogHandler());
            //config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());
            //HelpPageConfig.Register(config);
            //SwaggerNet.PreStart();
            var jsonFormatter = new JsonMediaTypeFormatter();
            //optional: set serializer settings here
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));

            //AutoNLog.Log4Info("正在启动WebApiServer");
        }

        //internal void UpdateRateLimits()
        //{
        //    //初始化策略仓库
        //    var policyRepository = new PolicyCacheRepository();

        //    //从缓存中获取策略对象
        //    var policy = policyRepository.FirstOrDefault(ThrottleManager.GetPolicyKey());

        //    //更新客户端限制频率
        //    policy.ClientRules["api-client-key-1"] =
        //        new RateLimits { PerMinute = 80, PerHour = 800 };

        //    //添加新的客户端限制频率
        //    policy.ClientRules.Add("api-client-key-3",
        //        new RateLimits { PerMinute = 60, PerHour = 600 });

        //    //应用策略更新
        //    ThrottleManager.UpdatePolicy(policy, policyRepository);

        //}
    }
    //internal class TracingThrottleLogger : IThrottleLogger
    //{
    //    private readonly ITraceWriter traceWriter;

    //    internal TracingThrottleLogger(ITraceWriter traceWriter)
    //    {
    //        this.traceWriter = traceWriter;
    //    }

    //    internal void Log(ThrottleLogEntry entry)
    //    {
    //        if (null != traceWriter)
    //        {
    //            string s = String.Format("{0} Request {1} from {2} has been throttled (blocked), quota {3}/{4} exceeded by {5}",
    //                entry.LogDate, entry.RequestId, entry.ClientIp, entry.RateLimit, entry.RateLimitPeriod, entry.TotalRequests);
    //            traceWriter.Info(entry.Request, "WebApiThrottle",s);
    //            AutoNLog.Log4Info("访问太频繁"+s);
    //        }
    //    }
    //}

    //internal class CustomThrottlingHandler : ThrottlingHandler
    //{
    //    protected override RequestIdentity SetIdentity(HttpRequestMessage request)
    //    {
    //        return new RequestIdentity()
    //        {
    //            ClientKey = request.Headers.Contains("UserKey") ? request.Headers.GetValues("UserKey").First() : "anon",
    //            ClientIp = base.GetClientIp(request).ToString(),
    //            Endpoint = request.RequestUri.AbsolutePath.ToLowerInvariant()
    //        };
    //    }
    //}
    internal class JsonContentNegotiator : IContentNegotiator
    {
        private readonly JsonMediaTypeFormatter _jsonFormatter;

        public JsonContentNegotiator(JsonMediaTypeFormatter formatter)
        {
            _jsonFormatter = formatter;
        }

        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            var result = new ContentNegotiationResult(_jsonFormatter, new MediaTypeHeaderValue("application/json"));
            return result;
        }
    }
    internal class CustomDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory>
        GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(inherit: true);
        }
    }
}
