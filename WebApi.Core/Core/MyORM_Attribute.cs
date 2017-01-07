using System;
using System.Data;

namespace WebApi.Core
{
    public class ORM_ConcurrentAttribute : Attribute
    {
        private string _TimestampFieldName;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="timestampFieldName"></param>
        public ORM_ConcurrentAttribute(string timestampFieldName)
        {
            _TimestampFieldName = timestampFieldName;
        }

        /// <summary>
        /// 用于控制并发操作时间戳的字段名
        /// </summary>
        public string TimestampFieldName
        {
            get { return _TimestampFieldName; }
            set { _TimestampFieldName = value; }
        }
    }



    public class ORM_TableAttribute : Attribute
    {
        private string _TableName; //物理表名,用于控制生成SQL语句Update (表) ....
        private string _PrimaryKey; //主键, 用于控制生成SQL语句的 Where @key=key
        private bool _isSummaryTable;//是否主表，明细表为false

        /// <summary>
        /// 是否主表，明细表为false
        /// </summary>
        public bool IsSummaryTable { get { return _isSummaryTable; } }

        /// <summary>
        /// 物理表名,用于控制生成SQL语句Update (表) ....
        /// </summary>
        public string TableName { get { return _TableName; } }

        /// <summary>
        /// 主键, 用于控制生成SQL语句的 Where @key=key, 复合主键(多个字段)用";"隔开。
        /// </summary>
        public string PrimaryKey { get { return _PrimaryKey; } }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="tableName">物理表名</param>
        /// <param name="primaryKey">主键,复合主键(多个字段)用";"隔开</param>
        /// <param name="isSummaryTable">是否主表</param>
        public ORM_TableAttribute(string tableName, string primaryKey, bool isSummaryTable)
        {
            _TableName = tableName;
            _PrimaryKey = primaryKey;
            _isSummaryTable = isSummaryTable;
        }
    }



    public class ORM_FieldAttribute : Attribute
    {
        private DbType _type; //数据类型
        private int _size; //字段长度
        private bool _isLookup;//是否是视图或Lookup字段
        private bool _isAddOrUpdate;//是否需要更新的字段
        private bool _isPrimaryKey; //是否主键字段  isid/ 32bit string
        private bool _isForeignKey;//是否外键字段 isid /32bit string
        private bool _isDocFieldName;//是否单据号码 

        /// <summary>
        /// SqlDbType数据类型
        /// </summary>
        public DbType Type { get { return _type; } }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int Size { get { return _size; } }

        /// <summary>
        /// 是否视图或Lookup参照字段(参照字段不能新增和修改)
        /// </summary>
        public bool IsLookup { get { return _isLookup; } }

        /// <summary>
        /// 是否更新字段
        /// </summary>
        public bool IsAddOrUpdate { get { return _isAddOrUpdate; } }

        /// <summary>
        /// 是否主键字段
        /// </summary>
        public bool IsPrimaryKey { get { return _isPrimaryKey; } }

        /// <summary>
        /// 是否外键字段
        /// </summary>
        public bool IsForeignKey { get { return _isForeignKey; } }

        /// <summary>
        /// 是否单据号码
        /// </summary>
        public bool IsDocFieldName { get { return _isDocFieldName; } }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <param name="size">字段长度</param>
        /// <param name="islookup">是否是视图或Lookup字段</param>
        /// <param name="isAddorUpdate">是否需要更新的字段</param>
        /// <param name="isPrimaryKey">是否主键字段</param>
        /// <param name="isForeignKey">是否外键字段</param>
        /// <param name="isDocFieldName">是否单据号码</param>
        public ORM_FieldAttribute(DbType type, int size,
            bool islookup, bool isAddorUpdate, bool isPrimaryKey,
            bool isForeignKey, bool isDocFieldName)
        {
            _type = type;
            _size = size;
            _isLookup = islookup;
            _isAddOrUpdate = isAddorUpdate;
            _isPrimaryKey = isPrimaryKey;
            _isForeignKey = isForeignKey;
            _isDocFieldName = isDocFieldName;
        }

    }

    //验证使用ORM
    public partial class ORM_ValidateAttribute : Attribute
    {
        //数据长度
        public int DbSize { get; set; }
        public bool IsRequired { get; set; } = false;
        public string ErrInfo4IsRequired { get; set; } = "必填";

        public bool IsMobile { get; set; } = false;
        public string ErrInfo4IsMobile { get; set; } = "手机号错误";

        public bool IsPersonID { get; set; } = false;
        public string ErrInfo4IsPersonID { get; set; } = "身份证号错误";

        public bool IsCarPlateNo { get; set; } = false;
        public string ErrInfo4IsCarPlateNo { get; set; } = "车牌号错误";

        public bool IsDateTime { get; set; } = false;
        public string ErrInfo4IsDateTime { get; set; } = "日期错误";

        public bool IsInt { get; set; } = false;
        public string ErrInfo4IsInt { get; set; } = "必须为整数";
        public bool IsNum { get; set; } = false;
        public string ErrInfo4IsNum { get; set; } = "必须为数字";
        public bool IsPositive { get; set; } = false;
        public string ErrInfo4IsPositive { get; set; } = "不可为负";
        public bool IsCanNotZero { get; set; } = false;
        public string ErrInfo4IsCanNotZero { get; set; } = "不可为零";
        //限定数值范围
        public bool IsValueArea { get; set; } = false;
        public string ErrInfo4IsValueArea { get; set; } = "数值错误";
        public decimal MinValue { get; set; } = 0;
        public decimal MaxValue { get; set; } = decimal.MaxValue;
        //限制字符串长度
        public bool IsLimitLenth { get; set; } = false;
        public string ErrInfo4IsLimitLenth { get; set; } = "输入长度错误";
        public int MinLenth { get; set; } = 0;
        public int MaxLenth { get; set; } = 100;
    }


    internal class ColumnProperty
    {
        private string _columnName;
        private bool _isPrimaryKey;

        /// <summary>
        /// 字段名
        /// </summary>
        internal string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        internal bool IsPrimaryKey
        {
            get { return _isPrimaryKey; }
            set { _isPrimaryKey = value; }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="isPrimaryKey">是否主键</param>
        internal ColumnProperty(string columnName, bool isPrimaryKey)
        {
            _columnName = columnName;
            _isPrimaryKey = isPrimaryKey;
        }
    }
}
