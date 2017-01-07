using System;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;

namespace WebApi.Core
{
    internal class MyActionFilterAttribute : ActionFilterAttribute
    {
        private string name;

        internal MyActionFilterAttribute() { }

        internal MyActionFilterAttribute(string name)
        {
            this.name = name;
        }
        /// <summary>
        /// Action执行前检查
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            base.OnActionExecuting(actionContext);
            //获取request的参数列表【获取API的参数】
            // Dictionary actionargument = actionContext.ActionArguments;
            //找出请求参数
            // RequestBaseEntity requestData = actionargument["requestData"] as RequestBaseEntity;
            //requestData是参数的名字
            //直接从上下文中回去请求参数，这个方法在HttpContext.Current.Request将无法获取到
            //获取请求信息中的token和用户信息
            string myname = HttpContext.Current.Request["myname"].ToString();
            Console.WriteLine(myname);
            //现在用户token信息是否有效和过期
            if (!myname.Equals("tmm"))
            {
                //如果token已经失效，重定向至token过期返回页面
                HttpContext.Current.Response.Redirect("~/Swagger");
                //创建响应对象，初始化为成功，没有指定的话本次请求将不会被拦截
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
        }

    }
}
