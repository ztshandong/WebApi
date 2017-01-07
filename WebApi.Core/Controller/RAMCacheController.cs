using System;
using System.Net.Http;
using System.Web.Http;
using WebApi.BLL;
using WebApi.Core;
using WebApi.Models;

namespace WebApi.Controller
{
    [RoutePrefix(WebApiGlobal._ROUTE_PREFIX_RAM_MGR)]
    public class RAMCacheController : ApiControllerBases
    {
        /// <summary>
        /// 新增UserKey之后要更新缓存
        /// </summary>
        [HttpGet]
        [Route(WebApiGlobal._ROUTE_REFRESH_USERKEY)]
        public HttpResponseMessage RefreshUserKey()
        {
           return  DoRefreshUserKey();
        }

        private HttpResponseMessage DoRefreshUserKey()
        {
            try
            {
                //if(!IsSysKey())
                //{
                //    return RespFailMsg();
                //}
                Params4ApiCRUD P = new Params4ApiCRUD();
                P.chooseDataBase = ChooseDataBase.System;
                if (!VerifyUserKey(P, 5))
                {
                    return RespFailMsg();
                }
                RAMCache.Instance.RefreshUserKey();
                return RespOkMsg();
            }
            catch (Exception ex)
            {
                return RespExMsg(ex);
            }
        }
        /// <summary>
        /// 刷新所有缓存，然后更新UserKey缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(WebApiGlobal._ROUTE_REFRESH_ALL_CACHE)]
        public HttpResponseMessage RefreshAllCache()
        {
            return DoRefreshAllCache();
        }

        private HttpResponseMessage DoRefreshAllCache()
        {
            try
            {
                //if (!IsSysKey())
                //{
                //    return RespFailMsg();
                //}
                Params4ApiCRUD P = new Params4ApiCRUD();
                P.chooseDataBase = ChooseDataBase.System;
                if (!VerifyUserKey(P, 5))
                {
                    return RespFailMsg();
                }
                RAMCache.Instance.ClearCache();
                RAMCache.Instance.RefreshUserKey();
                return RespOkMsg();
            }
            catch (Exception ex)
            {
                return RespExMsg(ex);
            }
        }
        protected override Params4ApiCRUD GenCRUDParam4RD()
        {
            throw new NotImplementedException();
        }

        protected override Params4ApiCRUD GenCRUDParam4CU()
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage DoSearch()
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage DoGet()
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage DoPost()
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage DoPut()
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage DoDelete()
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage DoTestSearch()
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage DoTestGet()
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage DoTestPost()
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage DoTestPut()
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage DoTestDelete()
        {
            throw new NotImplementedException();
        }
    }
}

