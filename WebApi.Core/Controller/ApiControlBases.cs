using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebApi.BLL;
using WebApi.Core;
using WebApi.Models;
using WebApi.Interface;
using WebApi.Public;

namespace WebApi.Controller
{
    public abstract class ApiControllerBases : ApiController, IApiControlBaseInterface
    {
        protected BLLBase _BLLBase;
        private Dictionary<string, string> _UriParam;
        protected virtual Dictionary<string, string> UriParam
        {
            get
            {
                if (_UriParam == null) _UriParam = new Dictionary<string, string>();
                _UriParam.Clear();
                var qnvp = this.Request.GetQueryNameValuePairs();

                foreach (var pair in qnvp)
                {
                    if (_UriParam.ContainsKey(pair.Key) == false)
                    {
                        _UriParam[pair.Key] = pair.Value;
                    }
                }
                return _UriParam;
            }
        }
        protected JsonSerializerSettings _JsonSetting = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatString = "yyyy-MM-dd HH:mm",
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        //protected virtual bool IsSysKey()
        //{
        //    try
        //    {
        //        string UserKey = this.Request.Headers.GetValues(WebApiGlobal._USERKEY).ToArray()[0];
        //        //string requestUri = this.Request.RequestUri.OriginalString;
        //        string requestUri = this.Request.Headers.GetValues(WebApiGlobal._ORI_REQUEST_URL).ToArray()[0];
        //        //string requestUri = this.Request.RequestUri.OriginalString;                
        //        requestUri = System.Web.HttpUtility.UrlDecode(requestUri);
        //        DataTable dt = RAMCache.Instance.UserKeyAndSalt;
        //        DataRow[] dr = dt.Select(WebApiGlobal._USERKEY + " = '" + UserKey + "'");
        //        if (dr.Length == 1)
        //        {
        //            string keyDataBase = dr[0][WebApiGlobal._DATABASENAME].ToString();
        //            string userCode = dr[0][WebApiGlobal._USERCODE].ToString();

        //            if (keyDataBase == ChooseDataBase.System.ToString())
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                AutoNLog.Log4Warn(CustomErrorMessage.UserKey跨库使用.ToString() + ",UserCode:" + userCode + ",RequestUri:" + requestUri);
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            AutoNLog.Log4Warn(CustomErrorMessage.UserKey无效.ToString() + ",UserKey:" + UserKey + ",RequestUri:" + requestUri);
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        AutoNLog.Log4Exception(CustomErrorMessage.发生异常.ToString(), ex);
        //        return false;
        //    }
        //}
        //protected virtual bool VerifyUserKey(Params4ApiCRUD P)
        //{
        //    string UserKey = this.Request.Headers.GetValues(WebApiGlobal._USERKEY).ToArray()[0];
        //    //string SHA512UserKey = Encryption.Instance.StringToSHA512Hash(UserKey);
        //    DataTable dt = RAMCache.Instance.UserKeyAndSalt;
        //    DataRow[] dr = dt.Select(WebApiGlobal._USERKEY + " = '" + UserKey + "'");
        //    string chooseDataBase = DataBaseConnFactory.Instance.GetUserKeyAndDataBase(P.chooseDataBase);
        //    string keyDataBase = dr[0][WebApiGlobal._DATABASENAME].ToString();
        //    string userCode = dr[0][WebApiGlobal._USERCODE].ToString();
        //    if (chooseDataBase == keyDataBase)
        //    {
        //        P.UserCode = userCode;
        //        return true;
        //    }
        //    else
        //    {
        //        string ClientTS = this.Request.Headers.GetValues(WebApiGlobal._TIMESPAN).ToArray()[0];

        //        //string requestUri = this.Request.RequestUri.AbsoluteUri;
        //        string requestUri = this.Request.RequestUri.OriginalString;
        //        requestUri = System.Web.HttpUtility.UrlDecode(requestUri);

        //        string str4Log = "ChooseDataBase:" + chooseDataBase + ",UserCode:" + userCode + ",ClientTS:" + ClientTS + ",requestUri:" + requestUri;

        //        AutoNLog.Log4Warn(CustomErrorMessage.UserKey跨库使用.ToString() + str4Log);
        //        return false;
        //    }
        //}

        protected virtual bool VerifyUserKey(Params4ApiCRUD P, decimal timespan = 600)//默认允许时差十分钟
        {
            try
            {
                bool iscorrect = false;
                string ClientTS = this.Request.Headers.GetValues(WebApiGlobal._TIMESPAN).ToArray()[0];
                string ServerTS = CommonMethod.UTCTS;
                //string requestUri = this.Request.RequestUri.AbsoluteUri;
                string requestUri = this.Request.Headers.GetValues(WebApiGlobal._ORI_REQUEST_URL).ToArray()[0];
                //string requestUri = this.Request.RequestUri.OriginalString;
                requestUri = System.Web.HttpUtility.UrlDecode(requestUri);
                string UserKey = this.Request.Headers.GetValues(WebApiGlobal._USERKEY).ToArray()[0];

                string chooseDataBase = P.chooseDataBase.ToString();

                DataTable dt = RAMCache.Instance.UserKeyAndSalt;
                DataRow[] dr = dt.Select(WebApiGlobal._USERKEY + " = '" + UserKey + "'");
                string str4ErrLog = "";
                if (dr.Length == 1)
                {
                    string UserCode = dr[0][WebApiGlobal._USERCODE].ToString();
                    string UserSalt = dr[0][WebApiGlobal._USERSALT].ToString();
                    string OriKey = dr[0][WebApiGlobal._DECODE_USERKEY].ToString();
                    string KeyDataBase = dr[0][WebApiGlobal._DATABASENAME].ToString();
                    str4ErrLog = "UserCode:" + UserCode + ",ClientTS:" + ClientTS + ",requestUri:" + requestUri;

                    decimal tsc = ClientTS.ToDecimalEx(0);
                    decimal tss = ServerTS.ToDecimalEx(0);
                    decimal diff = tss - tsc;
                    if (diff > timespan || diff < -5)
                    {
                        AutoNLog.Log4Warn(CustomErrorMessage.TimeSpan错误.ToString() + str4ErrLog);
                        return iscorrect;
                    }

                    if (chooseDataBase != KeyDataBase)
                    {
                        AutoNLog.Log4Warn(CustomErrorMessage.UserKey跨库使用.ToString() + "ChooseDataBase:" + chooseDataBase + str4ErrLog);
                        return iscorrect;
                    }

                    string ClientSHA256Sign = this.Request.Headers.GetValues(WebApiGlobal._SHA256).ToArray()[0];
                    string ServerSHA256Sign = CommonMethod.StringToSHA256Hash(OriKey + requestUri + ClientTS + UserSalt);
                    if (ClientSHA256Sign != ServerSHA256Sign)
                    {
                        AutoNLog.Log4Warn(CustomErrorMessage.Hash校验错误.ToString() + str4ErrLog + ",ClientSHA256Sign:" + ClientSHA256Sign + ",ServerSHA256Sign:" + ServerSHA256Sign);
                        return iscorrect;
                    }
                    P.UserCode = UserCode;
                    iscorrect = true;
                    return iscorrect;
                }
                else
                {
                    AutoNLog.Log4Warn(CustomErrorMessage.UserKey无效.ToString() + UserKey);
                    return iscorrect;
                }
            }
            catch (Exception ex)
            {
                AutoNLog.Log4Exception(CustomErrorMessage.Hash校验异常.ToString(), ex);
                return false;
            }
        }
        protected virtual string Uri2JSON()
        {
            //if (!this.VerifyUserKey())
            //{
            //    return "";
            //}
            //string dataset_json_string1 = JsonConvert.SerializeObject(qnvp);//[{"Key":"MD5sign","Value":"56789"},{"Key":"DDNO","Value":"SHDD1609"},{"Key":"user_key","Value":"12345"}]
            return JsonConvert.SerializeObject(UriParam);//这个才是需要的格式            
        }
        protected virtual bool SimpleVerifyDocNo(DocType docType, string docNo)
        {
            if (docNo.IsNullOrEmpty() || docNo.Length < 8)
            {
                return false;
            }
            if (docType == DocType.预订单)
            {
                if (docNo.Length != 13)
                    return false;
            }
            return true;
        }

        protected virtual CustomHttpResponseMessage NoDocResp
        {
            get
            {
                CustomHttpResponseMessage r = new CustomHttpResponseMessage();
                r.RespData = CustomErrorMessage.无此数据.ToString();
                r.RespStatus = CustomHttpResponseMessageStatus.Fail.ToString();
                r.ErrorMessage = CustomErrorMessage.操作失败.ToString();
                return r;
            }
        }
        protected virtual HttpResponseMessage RespFailMsg(object o = null)
        {
            CustomHttpResponseMessage r = new CustomHttpResponseMessage();
            r.RespData = o;
            r.ErrorMessage = CustomErrorMessage.操作失败.ToString();
            r.RespStatus = CustomHttpResponseMessageStatus.Fail.ToString();
            return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(r, _JsonSetting), Encoding.GetEncoding("UTF-8"), "application/json") };
        }

        protected virtual HttpResponseMessage RespOkMsg(object o = null)
        {

            CustomHttpResponseMessage r = new CustomHttpResponseMessage();
            r.RespData = o;
            HttpResponseMessage h = new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(r, _JsonSetting), Encoding.GetEncoding("UTF-8"), "application/json") };
            h.Headers.Remove("Server");
            return h;
            //return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(r, _JsonSetting), Encoding.GetEncoding("UTF-8"), "application/json") };
        }
        protected virtual HttpResponseMessage RespExMsg(Exception ex)
        {
            MyLog log = new MyLog(WebApiGlobal._MyLogPath);
            log.log(ex.Message);
            AutoNLog.Log4Exception(CustomErrorMessage.发生异常.ToString(), ex);
            CustomHttpResponseMessage r = new CustomHttpResponseMessage();
            r.RespData = "";
            r.ErrorMessage = CustomErrorMessage.操作失败.ToString();
            r.RespStatus = CustomHttpResponseMessageStatus.Error.ToString();

            return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(r, _JsonSetting), Encoding.GetEncoding("UTF-8"), "application/json") };
        }
        protected virtual HttpResponseMessage RespMsg4CRUD(DataRow dr)
        {
            CustomHttpResponseMessage r = new CustomHttpResponseMessage();
            r.RespData = dr[0].ToStringEx();
            if (dr[1].ToString() == CRUDResult.Fail.ToString())
            {
                r.ErrorMessage = CustomErrorMessage.操作失败.ToString();
                r.RespStatus = CustomHttpResponseMessageStatus.Fail.ToString();
            }
            return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(r, _JsonSetting), Encoding.GetEncoding("UTF-8"), "application/json") };
        }
        protected virtual HttpResponseMessage RespMsg(CustomHttpResponseMessage o)
        {
            return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(o, _JsonSetting), Encoding.GetEncoding("UTF-8"), "application/json") };
        }

        protected abstract Params4ApiCRUD GenCRUDParam4RD();// { return null; }
        protected abstract Params4ApiCRUD GenCRUDParam4CU();// { return null; }
        //[HttpGet]
        //[Route(WebApiGlobal._ROUTE_VERSION1+WebApiGlobal._ROUTE_HELP_GET_COMMON)]
        //internal abstract HttpResponseMessage Search4Common([FromUri]string privatesign);
        /// <summary>
        /// 查找多个单据
        /// </summary>
        /// <param name="authBase"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(WebApiGlobal._ROUTE_VERSION1 + WebApiGlobal._ROUTE_SEARCH)]
        public abstract HttpResponseMessage DoSearch();// { return RespMsg(NoDocResp); }
        /// <summary>
        /// 查找一个单据
        /// </summary>
        /// <param name="authBase"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(WebApiGlobal._ROUTE_VERSION1 + WebApiGlobal._ROUTE_GET)]
        public abstract HttpResponseMessage DoGet();// { return RespMsg(NoDocResp); }
        /// <summary>
        /// 新增单据
        /// </summary>
        /// <param name="authBase"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(WebApiGlobal._ROUTE_VERSION1 + WebApiGlobal._ROUTE_POST)]
        public abstract HttpResponseMessage DoPost();// { return RespMsg(NoDocResp); }
        /// <summary>
        /// 更新单据
        /// </summary>
        /// <param name="authBase"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(WebApiGlobal._ROUTE_VERSION1 + WebApiGlobal._ROUTE_PUT)]
        public abstract HttpResponseMessage DoPut();// { return RespMsg(NoDocResp); }
        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="authBase"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(WebApiGlobal._ROUTE_VERSION1 + WebApiGlobal._ROUTE_DELETE)]
        public abstract HttpResponseMessage DoDelete();// { return RespMsg(NoDocResp); }
        /// <summary>
        /// 测试Api查找多个单据
        /// </summary>
        /// <param name="authBase"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(WebApiGlobal._ROUTE_VERSION1 + WebApiGlobal._ROUTE_TEST_SEARCH)]
        public abstract HttpResponseMessage DoTestSearch();// { return RespMsg(NoDocResp); }
        /// <summary>
        /// 测试Api查找一个单据
        /// </summary>
        /// <param name="authBase"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(WebApiGlobal._ROUTE_VERSION1 + WebApiGlobal._ROUTE_TEST_GET)]
        public abstract HttpResponseMessage DoTestGet();// { return RespMsg(NoDocResp); }
        /// <summary>
        /// 测试Api新增单据
        /// </summary>
        /// <param name="authBase"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(WebApiGlobal._ROUTE_VERSION1 + WebApiGlobal._ROUTE_TEST_POST)]
        public abstract HttpResponseMessage DoTestPost();// { return RespMsg(NoDocResp); }
        /// <summary>
        /// 测试Api修改单据
        /// </summary>
        /// <param name="authBase"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(WebApiGlobal._ROUTE_VERSION1 + WebApiGlobal._ROUTE_TEST_PUT)]
        public abstract HttpResponseMessage DoTestPut();// { return RespMsg(NoDocResp); }
        /// <summary>
        /// 测试Api删除单据
        /// </summary>
        /// <param name="authBase"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(WebApiGlobal._ROUTE_VERSION1 + WebApiGlobal._ROUTE_TEST_DELETE)]
        public abstract HttpResponseMessage DoTestDelete();// { return RespMsg(NoDocResp); }
    }
}

















//tb_TMS_DD get_tb_TMS_DD = FromUri2tb_TMS_DD(authBase);
//if (!SimpleVerifyDocNo(DocType.预订单, get_tb_TMS_DD.DDNO.ToStringEx()))
//{
//    throw new Exception("tb_TMS_DD_Get订单号位数错误：" + "User_Key：" + authBase.User_Key.ToStringEx() + "，MD5sign：" + authBase.MD5sign.ToStringEx());
//}
//IniBLLInstance();
//Params4ApiCRUD P = new Params4ApiCRUD();
//P.fromUri = get_tb_TMS_DD;
//P.requestMethods = RequestMethods.Get;
//P.authBase = authBase;
//DataSet ds = _BLLBase.Get(P);//目前只有预订单，为了日后扩展，托运单，短驳单，中转单等
//return ds;
//return ds.Tables[0];
//return ds.Tables[0].Rows[0];//会掺进去很多其余信息，？！！

//DataSet转JSON，每个DataTable要有表名
//string dataset_json_string = JsonConvert.SerializeObject(ds);
////tb_TMS_DD4Get相当于DataSet,DataSet中的DataTable要在tb_TMS_DD4Get中以子类的形式表现，名字要对应
//tb_TMS_DD4Get dataset_json_class = (tb_TMS_DD4Get)JsonConvert.DeserializeObject(dataset_json_string, typeof(tb_TMS_DD4Get));
//return RespMsg(dataset_json_class);
//return dataset_json_class.tb_TMS_DD[0];//这样的JSON最外面是{}
//return dataset_json_class.tb_TMS_DD;//这样返回的JSON最外面是[]，代表是数组