using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebApi.BLL;
using WebApi.Models;
using WebApi.Core;
using System.Data;
using System.Net.Http;
using WebApi.Interface;


namespace WebApi.Controller
{
    /// <summary>
    /// 预订单接口
    /// </summary>
    [RoutePrefix(WebApiGlobal._ROUTE_PREFIX_TMS_DD)]
    public class TMS_DDController : ApiControllerBases, Interface4tb_TMS_DD4Control
    {
        private static bll_TMS_DD<Interface4tb_TMS_DD4BLL> _BLLInstance;

        internal TMS_DDController()
        {
            _BLLBase = new bll_TMS_DD<Interface4tb_TMS_DD4BLL>();
            _BLLInstance = _BLLBase as bll_TMS_DD<Interface4tb_TMS_DD4BLL>;
        }
        //private void IniBLLInstance()
        //{
        //    if (_BLLInstance == null)
        //    {
        //        _BLLBase = new bll_TMS_DD<Interface4tb_TMS_DD4BLL>();
        //        _BLLInstance = _BLLBase as bll_TMS_DD<Interface4tb_TMS_DD4BLL>;
        //    }
        //}
        private tb_TMS_DD FromUri2tb_TMS_DD()
        {
            string requestJSON = Uri2JSON();
            //if (requestJSON == "")
            //{
            //    return null;
            //}
            tb_TMS_DD tb_TMS_DD = (tb_TMS_DD)JsonConvert.DeserializeObject(requestJSON.ToStringEx(), typeof(tb_TMS_DD));

            return tb_TMS_DD;
        }
        private tb_TMS_DD_Base FromUri2tb_TMS_DD_Base()
        {
            string requestJSON = Uri2JSON();
            //if (requestJSON == "")
            //{
            //    return null;
            //}
            tb_TMS_DD_Base tb_TMS_DD_Base = (tb_TMS_DD_Base)JsonConvert.DeserializeObject(requestJSON.ToStringEx(), typeof(tb_TMS_DD_Base));

            return tb_TMS_DD_Base;
        }
        protected override Params4ApiCRUD GenCRUDParam4RD()
        {
            tb_TMS_DD_Base tb_TMS_DD_Base = FromUri2tb_TMS_DD_Base();
            //if (tb_TMS_DD_Base == null)
            //{
            //    return null;
            //}
            //IniBLLInstance();
            Params4ApiCRUD P = new Params4ApiCRUD();
            P.fromUri = tb_TMS_DD_Base;
            return P;
        }
        protected override Params4ApiCRUD GenCRUDParam4CU()
        {
            tb_TMS_DD _tb_TMS_DD = FromUri2tb_TMS_DD();
            //if (_tb_TMS_DD == null)
            //{
            //    return null;
            //}
            //IniBLLInstance();
            Params4ApiCRUD P = new Params4ApiCRUD();
            P.fromUri = _tb_TMS_DD;
            return P;
        }

        private bool VerifyParam(Params4ApiCRUD P, bool VerifyDDNO = true)
        {
            //if (P == null)
            //{
            //    return false;
            //}
            if (VerifyDDNO)
            {
                if (!SimpleVerifyDocNo(DocType.预订单, ((Itb_TMS_DD)P.fromUri).DDNO.ToStringEx()))
                {
                    return false;
                }
            }
            if (!VerifyUserKey(P))
            {
                return false;
            }

            return true;
        }
        public override HttpResponseMessage DoGet()
        {
            try
            {
                Params4ApiCRUD P = GenCRUDParam4RD();
                if (!VerifyParam(P))
                {
                    return RespFailMsg(new tb_TMS_DD());
                    //return RespMsg(NoDocResp);
                }
                DataSet ds = _BLLInstance.DoGet(P);
                if (ds.Tables[TbName.tb_TMS_DD].Rows.Count <= 0)
                {
                    return RespFailMsg(new tb_TMS_DD());
                    //return RespMsg(NoDocResp);
                }
                string dataset_json_string = JsonConvert.SerializeObject(ds, _JsonSetting);
                tb_TMS_DD4DataSet dataset_json_class = (tb_TMS_DD4DataSet)JsonConvert.DeserializeObject(dataset_json_string, typeof(tb_TMS_DD4DataSet));
                return RespOkMsg(dataset_json_class.tb_TMS_DD[0]);
            }
            catch (Exception ex)
            {
                return RespExMsg(ex);
            }
        }
        public override HttpResponseMessage DoTestGet()
        {
            try
            {
                Params4ApiCRUD P = GenCRUDParam4RD();
                //if (P == null)
                //{
                //    return RespFailMsg(new tb_TMS_DD());
                //    //return RespMsg(NoDocResp);
                //}
                P.chooseDataBase = ChooseDataBase.Test;
                if (!VerifyParam(P))
                {
                    return RespFailMsg(new tb_TMS_DD());
                    //return RespMsg(NoDocResp);
                }
                DataSet ds = _BLLInstance.DoGet(P);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    return RespFailMsg(new tb_TMS_DD());
                }
                //return RespOkMsg(ds.Tables[0]);

                //否则日期中间会多出来个T，莫名其妙
                string dataset_json_string = JsonConvert.SerializeObject(ds, _JsonSetting);
                tb_TMS_DD4DataSet dataset_json_class = (tb_TMS_DD4DataSet)JsonConvert.DeserializeObject(dataset_json_string, typeof(tb_TMS_DD4DataSet));

                return RespOkMsg(dataset_json_class.tb_TMS_DD[0]);
            }
            catch (Exception ex)
            {
                return RespExMsg(ex);
            }
        }

        public override HttpResponseMessage DoPost()
        {
            try
            {
                Params4ApiCRUD P = GenCRUDParam4CU();
                if (!VerifyParam(P, false))
                {
                    return RespMsg(NoDocResp);
                }
                DataSet ds = _BLLInstance.DoPost(P);
                return RespMsg4CRUD(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                return RespExMsg(ex);
            }
        }
        public override HttpResponseMessage DoTestPost()
        {
            try
            {
                Params4ApiCRUD P = GenCRUDParam4CU();
                if (P == null)
                {
                    return RespMsg(NoDocResp);
                }
                P.chooseDataBase = ChooseDataBase.Test;
                if (!VerifyParam(P, false))
                {
                    return RespMsg(NoDocResp);
                }
                DataSet ds = _BLLInstance.DoPost(P);
                return RespMsg4CRUD(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                return RespExMsg(ex);
            }
        }


        public override HttpResponseMessage DoPut()
        {
            try
            {
                Params4ApiCRUD P = GenCRUDParam4CU();
                if (!VerifyParam(P))
                {
                    return RespMsg(NoDocResp);
                }
                DataSet ds = _BLLInstance.DoPut(P);
                return RespMsg4CRUD(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                return RespExMsg(ex);
            }
        }
        public override HttpResponseMessage DoTestPut()
        {
            try
            {
                Params4ApiCRUD P = GenCRUDParam4CU();
                if (P == null)
                {
                    return RespMsg(NoDocResp);
                }
                P.chooseDataBase = ChooseDataBase.Test;
                if (!VerifyParam(P))
                {
                    return RespMsg(NoDocResp);
                }
                DataSet ds = _BLLInstance.DoPut(P);
                return RespMsg4CRUD(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                return RespExMsg(ex);
            }
        }

        public override HttpResponseMessage DoDelete()
        {
            try
            {
                Params4ApiCRUD P = GenCRUDParam4RD();
                if (!VerifyParam(P))
                {
                    return RespMsg(NoDocResp);
                }
                DataSet ds = _BLLInstance.DoDelete(P);
                return RespMsg4CRUD(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                return RespExMsg(ex);
            }
        }
        public override HttpResponseMessage DoTestDelete()
        {
            try
            {
                Params4ApiCRUD P = GenCRUDParam4RD();
                if (P == null)
                {
                    return RespMsg(NoDocResp);
                }
                P.chooseDataBase = ChooseDataBase.Test;
                if (!VerifyParam(P))
                {
                    return RespMsg(NoDocResp);
                }
                DataSet ds = _BLLInstance.DoDelete(P);
                return RespMsg4CRUD(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                return RespExMsg(ex);
            }
        }

        public override HttpResponseMessage DoSearch()
        {
            return RespMsg(NoDocResp);
        }

        public override HttpResponseMessage DoTestSearch()
        {
            return RespMsg(NoDocResp);
        }

    }

}
