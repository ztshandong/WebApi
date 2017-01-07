namespace WebApi.Core
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DbProviderType : byte
    {
        SqlServer,
        MySql,
        SQLite,
        Oracle,
        ODBC,
        //OleDb,//Excel单独处理
        Firebird,
        PostgreSql,
        DB2,
        Informix,
        SqlServerCe
    }
    /// <summary>
    /// 选择连接哪个数据库
    /// </summary>
    public enum ChooseDataBase
    {
        Test,
        Firs,
        System
    }
    /// <summary>
    /// 自定义返回消息的状态
    /// </summary>
    public enum CustomHttpResponseMessageStatus
    {
        OK,
        Fail,
        Error
    }
    public enum CustomErrorMessage
    {
        非法操作,
        操作失败,
        尚未实现,
        无此数据,
        TimeSpan错误,
        Hash校验错误,
        UserKey无效,
        Hash校验异常,
        没有UserKey,
        没有URL,
        没有IP,
        UserKey跨库使用,
        清理缓存异常,
        Trace发生异常,
        解密发生异常,
        发生异常
    }
    /// <summary>
    /// 新增，修改，删除，从后台返回的状态
    /// </summary>
    public enum CRUDResult
    {
        Success,
        Fail
    }
    /// <summary>
    /// http请求的类型，不用系统自带的
    /// </summary>
    public enum CustomRequestMethods
    {
        Search = 1,
        Get,
        Post,
        Put,
        Delete
    }

    public enum DocType
    {
        预订单
    }
}
