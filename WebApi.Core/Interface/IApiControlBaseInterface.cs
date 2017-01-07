using System.Net.Http;

namespace WebApi.Interface
{
    internal interface IApiControlBaseInterface
    {
        //对应5个自定义enum  RequestMethods
        HttpResponseMessage DoSearch();
        HttpResponseMessage DoGet();
        HttpResponseMessage DoPost();
        HttpResponseMessage DoPut();
        HttpResponseMessage DoDelete();
        //带5个测试
        HttpResponseMessage DoTestSearch();
        HttpResponseMessage DoTestGet();
        HttpResponseMessage DoTestPost();
        HttpResponseMessage DoTestPut();
        HttpResponseMessage DoTestDelete();
    }
}
