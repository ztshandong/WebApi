using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Web.Http;
using WebApi.BLL;
using WebApi.Core;
using WebApi.Interface;
using WebApi.Models;
using WebApi.Public;

namespace WebApi.Controller
{
    //[MyActionFilterAttribute]
    [RoutePrefix(WebApiGlobal._ROUTE_PREFIX_HELP)]
    public class web_Api_HelperController : ApiControllerBases, Interface4ApiHelper
    {
        //private bll_Api_Helper _BLLInstance;
        //private void IniBLLInstance()
        //{
        //    if (_BLLInstance == null)
        //    {
        //        _BLLBase = new bll_Api_Helper();
        //        _BLLInstance = _BLLBase as bll_Api_Helper;
        //    }
        //}
        [HttpGet]
        [Route(WebApiGlobal._ROUTE_HELP_GET_COMMON_BY_USP)]
        public HttpResponseMessage Search4CommonByUsp()
        {
            try
            {

                string uspname = UriParam[WebApiGlobal._API_HELP_CRUD_PARAM_USP_NAME];
                DataTable dt = RAMCache.Instance.FindByUspName(uspname);
                if (dt.Columns.Count == 1)
                {
                    List<string> list = new List<string>();
                    foreach (DataRow item in dt.Rows)
                    {
                        list.Add(item[0].ToStringEx());
                    }

                    return RespOkMsg(list);
                }
                else
                {
                    return RespOkMsg(dt);
                }
            }
            catch (Exception ex)
            {
                string uspname = UriParam[WebApiGlobal._API_HELP_CRUD_PARAM_USP_NAME];
                MyLog log = new MyLog(WebApiGlobal._MyLogPath);
                log.log(uspname);
                return RespExMsg(ex);
            }
        }

        [HttpGet]
        [Route(WebApiGlobal._ROUTE_HELP_GET_COMMON_BY_ENUM_NAME)]
        public HttpResponseMessage Search4CommonByEnumName()
        {

            try
            {
                //if (!IsTestKey())
                //{
                //    return RespMsg(NoDocResp);
                //}
                //if (!VerifyUserKey())
                //{
                //    return RespMsg(NoDocResp);
                //}
                string Enumname = UriParam[WebApiGlobal._API_HELP_CRUD_PARAM_ENUM_NAME];

                DataTable dt = RAMCache.Instance.FindByEnumName(Enumname);

                List<string> list = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item[0].ToStringEx());
                }
                if (list.Count > 0)
                {
                    return RespOkMsg(list);
                }
                else
                {
                    return RespFailMsg();
                }
            }
            catch (Exception ex)
            {
                string Enumname = UriParam[WebApiGlobal._API_HELP_CRUD_PARAM_ENUM_NAME];
                MyLog log = new MyLog(WebApiGlobal._MyLogPath);
                log.log(Enumname);
                

                return RespExMsg(ex);
            }
        }

        protected override Params4ApiCRUD GenCRUDParam4RD()
        {
            return null;
        }

        protected override Params4ApiCRUD GenCRUDParam4CU()
        {
            return null;
        }

        public override HttpResponseMessage DoSearch()
        {
            return RespMsg(NoDocResp);
        }

        public override HttpResponseMessage DoGet()
        {
            return RespMsg(NoDocResp);
        }

        public override HttpResponseMessage DoPost()
        {
            return RespMsg(NoDocResp);
        }

        public override HttpResponseMessage DoPut()
        {
            return RespMsg(NoDocResp);
        }

        public override HttpResponseMessage DoDelete()
        {
            return RespMsg(NoDocResp);
        }

        public override HttpResponseMessage DoTestSearch()
        {
            return RespMsg(NoDocResp);
        }

        public override HttpResponseMessage DoTestGet()
        {
            return RespMsg(NoDocResp);
        }

        public override HttpResponseMessage DoTestPost()
        {
            return RespMsg(NoDocResp);
        }

        public override HttpResponseMessage DoTestPut()
        {
            return RespMsg(NoDocResp);
        }

        public override HttpResponseMessage DoTestDelete()
        {
            return RespMsg(NoDocResp);
        }
    }
}
