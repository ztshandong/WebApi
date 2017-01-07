using WebApi.DAL;
using WebApi.Interface;

namespace WebApi.BLL
{
    internal partial class bll_TMS_DD<TBLLBase> : BLLBase, Interface4tb_TMS_DD4BLL where TBLLBase : Interface4tb_TMS_DD4BLL//, new()
    {
        //活化剂类不用添加new()
        //private TBLLBase _BLLBase = System.Activator.CreateInstance<TBLLBase>();
        protected dal_TMS_DD<Interface4tb_TMS_DD4DAL> _DALInstance;

        internal bll_TMS_DD()
        {
            _DALBase = new dal_TMS_DD<Interface4tb_TMS_DD4DAL>();
            _DALInstance = _DALBase as dal_TMS_DD<Interface4tb_TMS_DD4DAL>;
        }

        //private string GetErrMsg(tb_TMS_DD dd)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    //DataRow[] fromcity = RAMCache.Instance.ChinaCity.Select(" CityID = '" + dd.FromCityID + "'");
        //    //DataRow[] destcity = RAMCache.Instance.ChinaCity.Select(" CityID = '" + dd.DestCityID + "'");
        //    //var ss = RAMCache.Instance.ChinaCity.AsEnumerable().Where(c => c.Field<string>("CityID") == dd.FromCityID.ToString()).ToList();
        //    var FromCityInfos = RAMCache.Instance.ChinaCity.Select().Where(c => c.Field<string>("CityID") == dd.FromCityID.ToString()).ToList();
        //    if (FromCityInfos.Count == 1)
        //    {
        //        var FromCityInfo = FromCityInfos[0];
        //        dd.FromCity = FromCityInfo["County"].ToString();
        //        dd.FromRealCity = FromCityInfo["City"].ToString();
        //        dd.FromProvince = FromCityInfo["Province"].ToString();
        //    }
        //    else
        //    {
        //        sb.Append("始发城市有误，");
        //    }
        //    var DestCityInfos = RAMCache.Instance.ChinaCity.Select().Where(c => c.Field<string>("CityID") == dd.DestCityID.ToString()).ToList();
        //    if (DestCityInfos.Count == 1)
        //    {
        //        var DestCityInfo = DestCityInfos[0];
        //        dd.DestCity = DestCityInfo["County"].ToString();
        //        dd.DestRealCity = DestCityInfo["City"].ToString();
        //        dd.DestProvince = DestCityInfo["Province"].ToString();
        //    }
        //    else
        //    {
        //        sb.Append("目的城市有误，");
        //    }
        //    if (dd.DeliveryPerson.IsNullOrEmpty())
        //    {
        //        sb.Append("发货联系人有误，");
        //    }
        //    if (!dd.DeliveryMobile.IsMobile())
        //    {
        //        sb.Append("发货方手机号有误，");
        //    }
        //    if (dd.DeliveryAddress.IsNullOrEmpty())
        //    {
        //        sb.Append("发货地址有误，");
        //    }
        //    if (dd.ReceiverName.IsNullOrEmpty())
        //    {
        //        sb.Append("收货人有误，");
        //    }
        //    if (!dd.ReceiverMobile.IsMobile())
        //    {
        //        sb.Append("收货方手机号有误，");
        //    }
        //    if (dd.ReceiverAddress.IsNullOrEmpty())
        //    {
        //        sb.Append("收货地址有误，");
        //    }
        //    if (dd.ProductName.IsNullOrEmpty())
        //    {
        //        sb.Append("货物名称有误，");
        //    }
        //    if (dd.Quantity.ToIntEx() <= 0)
        //    {
        //        sb.Append("货物件数有误，");
        //    }
        //    if (dd.Weight.ToDecimalEx() <= 0)
        //    {
        //        sb.Append("货物重量有误，");
        //    }
        //    if (dd.Volumn.ToDecimalEx() <= 0)
        //    {
        //        sb.Append("货物体积有误，");
        //    }
        //    if (!Enum.IsDefined(typeof(TruckTypes), dd.TruckTypes))
        //    {
        //        sb.Append("需求车型有误，");
        //    }
        //    var CarSize = RAMCache.Instance.CarSize.Select().Where(c => c.Field<string>("TypeName") == dd.CarSize.ToString()).ToList();
        //    if (CarSize.Count != 1)
        //    {
        //        sb.Append("车长有误，");
        //    }
        //    var TransportType = RAMCache.Instance.TransportType.Select().Where(c => c.Field<string>("TypeName") == dd.TransportType.ToString()).ToList();
        //    if (TransportType.Count != 1)
        //    {
        //        sb.Append("运输类型有误，");
        //    }
        //    DateTime dt = dd.ArrivalDateEst.ToSqlDateTime();
        //    if (dt < WebApiGlobal.MinSqlDate)
        //    {
        //        sb.Append("提货时间有误，");
        //    }
        //    else
        //    {
        //        dd.ArrivalDateEst = dt.ToString("yyyy-MM-dd HH:mm");
        //    }
        //    if (Enum.GetName(typeof(SettleType), (SettleType)dd.SettleType).IsNullOrEmpty())
        //    {
        //        sb.Append("付款方式有误，");
        //    }
        //    if (dd.FlagHandling.IsNullOrEmpty()) dd.FlagHandling = "N";
        //    if (dd.FlagInsurance.IsNullOrEmpty()) dd.FlagInsurance = "N";
        //    if (dd.FlagInvoice.IsNullOrEmpty()) dd.FlagInvoice = "N";
        //    if (dd.FlagReceipt.IsNullOrEmpty()) dd.FlagReceipt = "N";
        //    if (dd.FlagSH.IsNullOrEmpty()) dd.FlagSH = "N";
        //    if (dd.FlagTH.IsNullOrEmpty()) dd.FlagTH = "N";
        //    if (
        //       (dd.FlagHandling.ToUpper() != "N" && dd.FlagHandling.ToUpper() != "Y")
        //    || (dd.FlagInsurance.ToUpper() != "N" && dd.FlagInsurance.ToUpper() != "Y")
        //    || (dd.FlagInvoice.ToUpper() != "N" && dd.FlagInvoice.ToUpper() != "Y")
        //    || (dd.FlagReceipt.ToUpper() != "N" && dd.FlagReceipt.ToUpper() != "Y")
        //    || (dd.FlagSH.ToUpper() != "N" && dd.FlagSH.ToUpper() != "Y")
        //    || (dd.FlagTH.ToUpper() != "N" && dd.FlagTH.ToUpper() != "Y")
        //    )
        //    {
        //        sb.Append("是否字段请填写Y或N，");
        //    }
        //    if(dd.ReceiptNum==null)
        //    {
        //        dd.ReceiptNum = 0;
        //    }
        //    if(dd.GoodsValue==null)
        //    {
        //        dd.GoodsValue = 0;
        //    }
        //    if(dd.ReceiptNum.ToIntEx()<0)
        //    {
        //        sb.Append("回单份数有误，");
        //    }
        //    if (dd.FlagInsurance.ToUpper() == "Y")
        //    {
        //        if (dd.GoodsValue.ToDecimalEx() <= 0)
        //        {
        //            sb.Append("需要保险请填写货物价值，");
        //        }
        //    }
        //    return sb.ToString();
        //}
        //protected override DataSet Get(Params4ApiCRUD P)
        //{           
        //    DataSet ds = _DALInstance.Get(P);
        //    return ds;
        //}
        //protected override DataSet Put(Params4ApiCRUD P)
        //{
        //    DataSet ds = _DALInstance.Put(P);
        //    return ds;            
        //}


        //protected override DataSet Post(Params4ApiCRUD P)
        //{
        //    DataSet ds = _DALInstance.Post(P);
        //    return ds;
        //}
        //protected override DataSet Delete(Params4ApiCRUD P)
        //{
        //    DataSet ds = _DALInstance.Delete(P);
        //    return ds;
        //}

        //protected override DataSet Search(Params4ApiCRUD P)
        //{
        //    DataSet ds = _DALInstance.Search(P);
        //    return ds;
        //}
    }
}
