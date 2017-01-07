using Newtonsoft.Json;
using WebApi.Core;

namespace WebApi.Models
{
    /// <summary>
    /// 增删改查传参通用类
    /// </summary>
    public class Params4ApiCRUD
    {
        /// <summary>
        /// 通过URL传递的参数
        /// </summary>
        internal object fromUri { get; set; }
        /// <summary>
        /// 选择的数据库
        /// </summary>
        internal ChooseDataBase chooseDataBase { get; set; } = ChooseDataBase.Firs;
        /// <summary>
        /// 数据库类型
        /// </summary>
        internal DbProviderType dbProviderType { get; set; } = DbProviderType.SqlServer;
        /// <summary>
        /// 用户代码，UserKey表中的值，可能为Customer，也可能为Supplier
        /// </summary>
        internal string UserCode { get; set; } = "";
        /// <summary>
        /// 备用
        /// </summary>
        internal object otherParam { get; set; }
    }
    /// <summary>
    /// 自定义返回消息
    /// </summary>
    public class CustomHttpResponseMessage
    {
        /// <summary>
        /// 返回的数据
        /// </summary>
        [JsonProperty("RespData")]
        internal object RespData { get; set; }
        /// <summary>
        /// 返回状态，默认成功
        /// </summary>
        [JsonProperty("RespStatus")]
        internal string RespStatus { get; set; } = CustomHttpResponseMessageStatus.OK.ToString();
        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("ErrorMessage")]
        internal string ErrorMessage { get; set; } = "";
    }

    /// <summary>
    /// 路由记录
    /// </summary>
    internal sealed class Trace4NLog
    {
        [JsonProperty("userkey")]
        internal string userkey { get; set; }

        [JsonProperty("requesturl")]
        internal string requesturl { get; set; }

        [JsonProperty("methodJSON")]
        internal string methodJSON { get; set; }

        [JsonProperty("urlJSON")]
        internal string urlJSON { get; set; }

        [JsonProperty("clientIPJSON")]
        internal string clientIPJSON { get; set; }

        [JsonProperty("respStatusCodeInt")]
        internal int respStatusCodeInt { get; set; }

        [JsonProperty("respStatusCodeString")]
        internal string respStatusCodeString { get; set; }
    }
    /// <summary>
    /// 异常记录
    /// </summary>
    internal sealed class Exception4NLog
    {
        [JsonProperty("ErrDescription")]
        internal string ErrDescription { get; set; }

        [JsonProperty("ExMessage")]
        internal string ExMessage { get; set; }

        [JsonProperty("InnerExMessage")]
        internal string InnerExMessage { get; set; }

        [JsonProperty("InnerExSource")]
        internal string InnerExSource { get; set; }
    }
    /// <summary>
    /// 正常记录
    /// </summary>
    internal sealed class Info4NLog
    {
        [JsonProperty("InfoDescription")]
        internal string InfoDescription { get; set; }
    }

    /// <summary>
    /// 返回的数据
    /// </summary>
    internal class CustomHttpResponseMessage4Common
    {
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("RespStatus")]
        internal string RespStatus { get; set; } = "";
        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("ErrorMessage")]
        internal string ErrorMessage { get; set; } = "";
        /// <summary>
        /// 返回的数据
        /// </summary>
        [JsonProperty("RespData")]
        internal string RespData { get; set; }

    }

    /// <summary>
    /// 编辑单据返回的状态
    /// </summary>
    internal class CustomHttpResponseMessage4Edit
    {
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("RespStatus")]
        internal string RespStatus { get; set; } = "";
        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("ErrorMessage")]
        internal string ErrorMessage { get; set; } = "";
        /// <summary>
        /// 返回的数据
        /// </summary>
        [JsonProperty("RespData")]
        internal string RespData { get; set; }

    }
    /// <summary>
    /// 获取加密字符串
    /// </summary>
    internal class CustomHttpResponseMessage4Hash
    {
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("RespStatus")]
        internal string RespStatus { get; set; } = "";
        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("ErrorMessage")]
        internal string ErrorMessage { get; set; } = "";

        /// <summary>
        /// 返回的Hash值
        /// </summary>
        [JsonProperty("RespData")]
        internal Value4Hash RespData { get; set; } = new Value4Hash();

    }
    /// <summary>
    /// 密文
    /// </summary>
    internal class Value4Hash
    {
        /// <summary>
        /// MD5
        /// </summary>
        [JsonProperty("MD5")]
        internal string MD5 { get; set; }
        /// <summary>
        /// SHA1
        /// </summary>
        [JsonProperty("SHA1")]
        internal string SHA1 { get; set; }
        /// <summary>
        /// SHA256
        /// </summary>
        [JsonProperty("SHA256")]
        internal string SHA256 { get; set; }
        /// <summary>
        /// SHA384
        /// </summary>
        [JsonProperty("SHA384")]
        internal string SHA384 { get; set; }
        /// <summary>
        /// SHA512
        /// </summary>
        [JsonProperty("SHA512")]
        internal string SHA512 { get; set; }
    }
    /// <summary>
    /// 查询预订单
    /// </summary>
    internal class CustomHttpResponseMessage4TMSDD
    {
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("RespStatus")]
        internal string RespStatus { get; set; } = "";
        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("ErrorMessage")]
        internal string ErrorMessage { get; set; } = "";
        /// <summary>
        /// 预订单信息
        /// </summary>
        [JsonProperty("RespData")]
        internal tb_TMS_DD RespData { get; set; }

    }
}
