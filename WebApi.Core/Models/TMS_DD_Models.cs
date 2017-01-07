using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using WebApi.Core;

namespace WebApi.Models
{

    /// <summary>
    /// 反序列化DataSet的JSON字符串要用此类型
    /// </summary>
    internal class tb_TMS_DD4DataSet
    {
        //internal string Status { get; set; }//添加其他自定义字段

        [JsonProperty(TbName.tb_TMS_DD)]
        internal List<tb_TMS_DD> tb_TMS_DD { get; private set; }
        //internal tb_TMS_DD[] tb_TMS_DD { get; private set; }

        //[JsonProperty("datadetail")]//DataSet中如果包含多个表也是在这里添加
        //internal List<tb_TMS_DD> tb_TMS_DD_Details { get; private set; }

        //internal tb_TMS_DD4GetEx others { get; set; } = new tb_TMS_DD4GetEx(); //添加其他自定义字段
    }
    //internal class tb_TMS_DD4GetEx
    //{
    //    internal string other1 { get; set; }
    //    internal string other2 { get; set; }
    //}
    public interface Itb_TMS_DD
    {
        string DDNO { get; set; }
    }
    /// <summary>
    /// 预订单基类
    /// </summary>
    internal class tb_TMS_DD_Base : Itb_TMS_DD
    {
        private string _DDNO;
        /// <summary>
        /// *预订单号
        /// </summary>
        [ORM_Validate(DbSize = 13)]
        [JsonProperty("DDNO")]
        public string DDNO { get { return _DDNO.ToStringEx(); } set { _DDNO = value; } }


        private string _CustomerChildAccountID;
        /// <summary>
        /// *子级账号，手机号
        /// </summary>
        [ORM_Validate(DbSize = 11, IsRequired = true, IsMobile = true)]
        [JsonProperty("CustomerChildAccountID")]
        internal string CustomerChildAccountID { get { return _CustomerChildAccountID; } set { _CustomerChildAccountID = value; } }


    }
    /// <summary>
    /// 预订单信息
    /// </summary>
    internal sealed class tb_TMS_DD : tb_TMS_DD_Base
    {

        private int _FromCityID;
        /// <summary>
        /// *发货城市，6位区位码，不是邮编，请参考ApiHelper
        /// </summary>
        [ORM_ValidateAttribute(DbSize = 6, IsRequired = true, ErrInfo4IsRequired = "始发城市必填")]
        [JsonProperty("FromCityID")]
        internal int? FromCityID { get { return _FromCityID.ToIntEx(); } set { _FromCityID = value.ToIntEx(); } }
        private string _FromProvince;
        [JsonIgnore]
        [ORM_Validate(DbSize = 100)]
        internal string FromProvince { get { return _FromProvince.ToStringEx(); } set { _FromProvince = value; } }

        private string _FromRealCity;
        [JsonIgnore]
        [ORM_Validate(DbSize = 100)]
        internal string FromRealCity { get { return _FromRealCity.ToStringEx(); } set { _FromRealCity = value; } }

        private string _FromCity;
        [JsonIgnore]
        [ORM_Validate(DbSize = 100)]
        internal string FromCity { get { return _FromCity.ToStringEx(); } set { _FromCity = value; } }

        private int _DestCityID;
        /// <summary>
        /// *收货城市，6位区位码，不是邮编，请参考ApiHelper
        /// </summary>
        [ORM_Validate(DbSize = 6)]
        [JsonProperty("DestCityID")]
        internal int? DestCityID { get { return _DestCityID.ToIntEx(); } set { _DestCityID = value.ToIntEx(); } }
        private string _DestProvince;
        [JsonIgnore]
        [ORM_Validate(DbSize = 100)]
        internal string DestProvince { get { return _DestProvince.ToStringEx(); } set { _DestProvince = value; } }

        private string _DestRealCity;
        [JsonIgnore]
        [ORM_Validate(DbSize = 100)]
        internal string DestRealCity { get { return _DestRealCity.ToStringEx(); } set { _DestRealCity = value; } }

        private string _DestCity;
        [JsonIgnore]
        [ORM_Validate(DbSize = 100)]
        internal string DestCity { get { return _DestCity.ToStringEx(); } set { _DestCity = value; } }

        private string _TransportType;
        /// <summary>
        /// *运输类型（四选一）：同城整车，同城零担，长途整车，长途零担
        /// </summary>
        [ORM_Validate(DbSize = 20)]
        [JsonProperty("TransportType")]
        internal string TransportType { get { return _TransportType.ToStringEx(); } set { _TransportType = value; } }

        private string _DeliveryPerson;
        /// <summary>
        /// *发货联系人
        /// </summary>
        [ORM_Validate(DbSize = 20)]
        [JsonProperty("DeliveryPerson")]
        internal string DeliveryPerson { get { return _DeliveryPerson.ToStringEx(); } set { _DeliveryPerson = value; } }

        private string _DeliveryMobile;
        /// <summary>
        /// *发货人手机
        /// </summary>
        [ORM_Validate(DbSize = 11)]
        [JsonProperty("DeliveryMobile")]
        internal string DeliveryMobile { get { return _DeliveryMobile.ToStringEx(); } set { _DeliveryMobile = value; } }

        private string _DeliveryAddress;
        /// <summary>
        /// *发货地址
        /// </summary>
        [ORM_Validate(DbSize = 200)]
        [JsonProperty("DeliveryAddress")]
        internal string DeliveryAddress { get { return _DeliveryAddress.ToStringEx(); } set { _DeliveryAddress = value; } }

        private string _ReceiverName;
        /// <summary>
        /// *收货人名称
        /// </summary>
        [ORM_Validate(DbSize = 20)]
        [JsonProperty("ReceiverName")]
        internal string ReceiverName { get { return _ReceiverName.ToStringEx(); } set { _ReceiverName = value; } }

        private string _ReceiverMobile;
        /// <summary>
        /// *收货人手机
        /// </summary>
        [ORM_Validate(DbSize = 11)]
        [JsonProperty("ReceiverMobile")]
        internal string ReceiverMobile { get { return _ReceiverMobile.ToStringEx(); } set { _ReceiverMobile = value; } }

        private string _ReceiverAddress;
        /// <summary>
        /// *收货地址
        /// </summary>
        [ORM_Validate(DbSize = 200)]
        [JsonProperty("ReceiverAddress")]
        internal string ReceiverAddress { get { return _ReceiverAddress.ToStringEx(); } set { _ReceiverAddress = value; } }

        private string _ProductName;
        /// <summary>
        /// *货物名称
        /// </summary>
        [ORM_Validate(DbSize = 200)]
        [JsonProperty("ProductName")]
        internal string ProductName { get { return _ProductName.ToStringEx(); } set { _ProductName = value; } }

        private int _Quantity;
        /// <summary>
        /// *件数
        /// </summary>

        [JsonProperty("Quantity")]
        internal int? Quantity { get { return _Quantity.ToIntEx(); } set { _Quantity = value.ToIntEx(); } }

        private decimal _Weight;
        /// <summary>
        /// *重量（吨）
        /// </summary>

        [JsonProperty("Weight")]
        internal decimal? Weight { get { return _Weight.ToDecimalEx(); } set { _Weight = value.ToDecimalEx(); } }

        private decimal _Volumn;
        /// <summary>
        /// *体积（方）
        /// </summary>

        [JsonProperty("Volumn")]
        internal decimal? Volumn { get { return _Volumn.ToDecimalEx(); } set { _Volumn = value.ToDecimalEx(); } }

        private string _CarSize;
        /// <summary>
        /// *需求车长，请参考ApiHelper
        /// </summary>
        [ORM_Validate(DbSize = 5)]
        [JsonProperty("CarSize")]
        internal string CarSize { get { return _CarSize.ToStringEx(); } set { _CarSize = value; } }

        private string _TruckTypes;
        /// <summary>
        /// *需求车型，平板，高栏，厢式，其他
        /// </summary>
        [ORM_Validate(DbSize = 10)]
        [JsonProperty("TruckTypes")]
        internal string TruckTypes { get { return _TruckTypes.ToStringEx(); } set { _TruckTypes = value; } }

        private string _ArrivalDateEst;
        /// <summary>
        /// *提货时间，格式：2017-01-01 08:30
        /// </summary>
        [ORM_Validate(DbSize = 20)]
        [JsonProperty("ArrivalDateEst")]
        internal string ArrivalDateEst { get { return _ArrivalDateEst.ToStringEx(); } set { _ArrivalDateEst = value; } }

        private int _SettleType;
        /// <summary>
        /// *付款方式（请填写数字），1：现付；2：欠付；3：到付
        /// </summary>

        [JsonProperty("SettleType")]
        internal int? SettleType { get { return _SettleType.ToIntEx(); } set { _SettleType = value.ToIntEx(); } }

        private string _PackingType;
        /// <summary>
        /// 包装方式
        /// </summary>
        [JsonProperty("PackingType")]
        [ORM_Validate(DbSize = 20)]
        internal string PackingType { get { return _PackingType.ToStringEx(); } set { _PackingType = value; } }

        private string _DeliveryReq;
        /// <summary>
        /// 提货要求
        /// </summary>
        [JsonProperty("DeliveryReq")]
        [ORM_Validate(DbSize = 200)]
        internal string DeliveryReq { get { return _DeliveryReq.ToStringEx(); } set { _DeliveryReq = value; } }

        private string _FlagReceipt;
        /// <summary>
        /// 是否需要回单（Y/N）
        /// </summary>
        [JsonProperty("FlagReceipt")]
        [ORM_Validate(DbSize = 2)]
        internal string FlagReceipt { get { return _FlagReceipt.ToStringEx(); } set { _FlagReceipt = value; } }

        private int _ReceiptNum;
        /// <summary>
        /// 回单份数
        /// </summary>
        [JsonProperty("ReceiptNum")]
        internal int? ReceiptNum { get { return _ReceiptNum.ToIntEx(); } set { _ReceiptNum = value.ToIntEx(); } }

        private string _ReceiptNo;
        /// <summary>
        /// 回单号码
        /// </summary>
        [JsonProperty("ReceiptNo")]
        [ORM_Validate(DbSize = 50)]
        internal string ReceiptNo { get { return _ReceiptNo.ToStringEx(); } set { _ReceiptNo = value; } }

        private string _FlagHandling;
        /// <summary>
        /// 是否需要装卸（Y/N）
        /// </summary>
        [JsonProperty("FlagHandling")]
        [ORM_Validate(DbSize = 2)]
        internal string FlagHandling { get { return _FlagHandling.ToStringEx(); } set { _FlagHandling = value; } }

        private string _FlagTH;
        /// <summary>
        /// 是否需要提货（Y/N）
        /// </summary>
        [JsonProperty("FlagTH")]
        [ORM_Validate(DbSize = 2)]
        internal string FlagTH { get { return _FlagTH.ToStringEx(); } set { _FlagTH = value; } }

        private string _FlagSH;
        /// <summary>
        /// 是否需要送货（Y/N）
        /// </summary>
        [JsonProperty("FlagSH")]
        [ORM_Validate(DbSize = 2)]
        internal string FlagSH { get { return _FlagSH.ToStringEx(); } set { _FlagSH = value; } }

        private string _FlagInvoice;
        /// <summary>
        /// 是否需要开票（Y/N）
        /// </summary>
        [JsonProperty("FlagInvoice")]
        [ORM_Validate(DbSize = 2)]
        internal string FlagInvoice { get { return _FlagInvoice.ToStringEx(); } set { _FlagInvoice = value; } }

        private string _FlagInsurance;
        /// <summary>
        /// 是否需要保险（Y/N）
        /// </summary>
        [JsonProperty("FlagInsurance")]
        [ORM_Validate(DbSize = 2)]
        internal string FlagInsurance { get { return _FlagInsurance.ToStringEx(); } set { _FlagInsurance = value; } }

        private decimal _GoodsValue;
        /// <summary>
        /// 货物价值，需要保险时请填写
        /// </summary>
        [JsonProperty("GoodsValue")]
        internal decimal? GoodsValue { get { return _GoodsValue.ToDecimalEx(); } set { _GoodsValue = value.ToDecimalEx(); } }

        private string _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("Remark")]
        [ORM_Validate(DbSize = 1000)]
        internal string Remark { get { return _Remark.ToStringEx(); } set { _Remark = value; } }


        //[JsonProperty("TotalAmount")]
        //internal decimal? TotalAmount { get { return TotalAmount.ToDecimalEx(); } set { TotalAmount = value; } }

        //[JsonProperty("InsuranceAmount")]
        //internal decimal? InsuranceAmount { get { return InsuranceAmount.ToDecimalEx(); } set { InsuranceAmount = value; } }

        //[JsonProperty("DeliveryAmount")]
        //internal decimal? DeliveryAmount { get { return DeliveryAmount.ToDecimalEx(); } set { DeliveryAmount = value; } }

        //[JsonProperty("HandlingAmount")]
        //internal decimal? HandlingAmount { get { return HandlingAmount.ToDecimalEx(); } set { HandlingAmount = value; } }

        //[JsonProperty("OtherAmount")]
        //internal decimal? OtherAmount { get { return OtherAmount.ToDecimalEx(); } set { OtherAmount = value; } }

        //[JsonProperty("SupplierAddress")]
        //internal string SupplierAddress { get; set; } = "";
        //[JsonProperty("FlagYD")]
        //internal string FlagYD { get; set; }
        //[JsonProperty("FlagApp")]
        //internal string FlagApp { get; set; }
        //[JsonProperty("AppUser")]
        //internal string AppUser { get; set; }
        //[JsonProperty("AppDate")]
        //internal DateTime AppDate { get; set; }
        //[JsonProperty("DocAppDate")]
        //internal DateTime DocAppDate { get; set; }
        //[JsonProperty("DocAppUser")]
        //internal string DocAppUser { get; set; }
        //[JsonProperty("FlagDocApp")]
        //internal string FlagDocApp { get; set; }
        //[JsonProperty("CreationDate")]
        //internal DateTime CreationDate { get; set; }
        //[JsonProperty("CreatedBy")]
        //internal string CreatedBy { get; set; }
        //[JsonProperty("LastUpdateDate")]
        //internal DateTime LastUpdateDate { get; set; }
        //[JsonProperty("LastUpdatedBy")]
        //internal string LastUpdatedBy { get; set; }
        //[JsonProperty("Sales")]
        //internal string Sales { get; set; }
        //[JsonProperty("ProjectManager")]
        //internal string ProjectManager { get; set; }
        //[JsonProperty("SalesDeputy")]
        //internal string SalesDeputy { get; set; }
        //[JsonProperty("Status")]
        //internal string Status { get; set; }
        //[JsonProperty("isid")]
        //internal string isid { get; set; }
        //[JsonProperty("TS")]
        //internal byte[] TS { get; set; }
        //[JsonProperty("OrgCode")]
        //internal string OrgCode { get; set; }
        //[JsonProperty("HYNO")]
        //internal string HYNO { get; set; }
        //[JsonProperty("FlagHY")]
        //internal string FlagHY { get; set; }
        //[JsonProperty("DataType")]
        //internal string DataType { get; set; }
        //[JsonProperty("CustomerCode")]
        //internal string CustomerCode { get; set; }
        //[JsonProperty("CustomerOrderDate")]
        //internal string CustomerOrderDate { get; set; }
        //[JsonProperty("CustomerChildAccountID")]
        //internal string CustomerChildAccountID { get; set; }
        //[JsonProperty("CustomerName")]
        //internal string CustomerName { get; set; }
        //private string _DocDate;
        //[JsonProperty("DocDate")]
        //internal string DocDate { get { return _DocDate.ToStringEx(); } set { _DocDate = value; } }

        //private string _DestProvince;
        //[JsonProperty("DestProvince")]
        //internal string DestProvince { get { return _DestProvince.ToStringEx(); } set { _DestProvince = value; } }

        //private string _DestRealCity;
        //[JsonProperty("DestRealCity")]
        //internal string DestRealCity { get { return _DestRealCity.ToStringEx(); } set { _DestRealCity = value; } }

        //private string _DestCounty;
        //[JsonProperty("DestCounty")]
        //internal string DestCounty { get { return _DestCounty.ToStringEx(); } set { _DestCounty = value; } }

        //private string _FromProvince;
        //[JsonProperty("FromProvince")]
        //internal string FromProvince { get { return _FromProvince.ToStringEx(); } set { _FromProvince = value; } }

        //private string _FromRealCity;
        //[JsonProperty("FromRealCity")]
        //internal string FromRealCity { get { return _FromRealCity.ToStringEx(); } set { _FromRealCity = value; } }

        //private string _FromCounty;
        //[JsonProperty("FromCounty")]
        //internal string FromCounty { get { return _FromCounty.ToStringEx(); } set { _FromCounty = value; } }

        //private string _ReceiverContactTel;
        //
        //[JsonProperty("ReceiverContactTel")]
        //internal string ReceiverContactTel { get { return _ReceiverContactTel.ToStringEx(); } set { _ReceiverContactTel = value; } }

        //private string _DeliveryTel;
        //[JsonProperty("DeliveryTel")]
        //internal string DeliveryTel { get { return _DeliveryTel.ToStringEx(); } set { _DeliveryTel = value; } }

        //private string _DeliveryContactTel;
        //[JsonProperty("DeliveryContactTel")]
        //internal string DeliveryContactTel { get { return _DeliveryContactTel.ToStringEx(); } set { _DeliveryContactTel = value; } }

        //private string _ReceiverContact;
        //[JsonProperty("ReceiverContact")]
        //internal string ReceiverContact { get { return _ReceiverContact.ToStringEx(); } set { _ReceiverContact = value; } }

        //private string _ReceiverTel;
        //
        //[JsonProperty("ReceiverTel")]
        //internal string ReceiverTel { get { return _ReceiverTel.ToStringEx(); } set { _ReceiverTel = value; } }

    }


}
