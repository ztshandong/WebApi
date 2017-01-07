using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace System
{
    public static class ExtensionMethods
    {
        public static string ToStringEx(this object str)
        {
            if (str == null) return "";
            return str.ToString().Trim(); //.Replace(" ", "");
        }
        public static bool IsNullOrEmpty(this object s)
        {
            return string.IsNullOrEmpty(s.ToStringEx());
        }
        public static string ToCheckEx(this object s)
        {
            if (s.IsNullOrEmpty())
                return "N";
            else
                return s.ToString();
        }
        public static bool NotNullOrEmpty(this object s)
        {
            return !string.IsNullOrEmpty(s.ToStringEx());
        }
        public static DateTime ToSqlDateTime(this object o)
        {
            DateTime defMinValue = DateTime.Parse("1753-01-01 00:00:00");
            DateTime defMaxValue = DateTime.Parse("9999-12-31 23:59:59");
            if (null == o) return defMinValue;//传入空值，返回预设值

            DateTime dt;
            if (DateTime.TryParse(o.ToString(), out dt))
            {
                if (dt < defMinValue || dt > defMaxValue)
                    return defMinValue;//无效日期，预设返回SQL支持的最小日期
                else
                    return dt;
            }
            return defMinValue;
        }
        public static float ToFloatEx(this object o)
        {
            if (null == o) return 0;
            try
            {
                return (float)Convert.ToDouble(o.ToString());
            }
            catch { return 0; }
        }
        public static int ToIntEx(this object o)
        {
            if (null == o) return 0;
            try
            {
                return Convert.ToInt32(o.ToString());
            }
            catch { return 0; }
        }
        public static decimal ToDecimalEx(this object o, int i = 2)
        {
            if (null == o) return 0;
            try
            {
                //Math.Round之后不自动补零
                //return decimal.Parse(Math.Round(Convert.ToDouble(s.ToString()), i).ToString());
                return decimal.Parse(Convert.ToDecimal(o.ToString()).ToString("F" + i));
            }
            catch { return 0; }
        }

        public static bool IsMobile(this object s)
        {
            return Regex.IsMatch(s.ToStringEx(), @"^0?[1][3-8]\d{9}$");
        }
        /// <summary>
        /// 判断是否是正确的身份证格式，只支持18位
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPersonID(this object obj)
        {
            if (obj.IsNullOrEmpty() | !Regex.IsMatch(obj.ToStringEx(), @"^((1[1-5])|(2[1-3])|(3[1-7])|(4[1-6])|(5[0-4])|(6[1-5])|71|(8[12])|91)\d{4}(((19|20)\d{2}(0[13-9]|1[012])(0[1-9]|[12]\d|30))|((19|20)\d{2}(0[13578]|1[02])31)|((19|20)\d{2}02(0[1-9]|1\d|2[0-8]))|((19|20)([13579][26]|[2468][048]|0[48])0229))\d{3}(\d|X|x)$"))
                return false;

            if (obj.ToString().Length == 18)
            {
                string birthday = string.Empty;
                birthday = String.Format("{0}-{1}-{2}", obj.ToString().Substring(6, 4), obj.ToString().Substring(10, 2), obj.ToString().Substring(12, 2));
                try
                {
                    if (DateTime.Parse(birthday) > DateTime.Now)
                        return false;
                }
                catch (Exception ex)
                {
                    return false;
                }

                char[] code = obj.ToString().ToArray();
                //加权因子
                int[] factor = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
                //校验位
                char[] parity = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum += code[i].ToIntEx() * factor[i];
                }
                char last = parity[sum % 11];
                try
                {
                    if (last != char.ToUpper(code[17]))
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    if (last != code[17])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsCarPlateNo(this object s)
        {
            return Regex.IsMatch(s.ToStringEx(), @"^[\u4e00-\u9fa5]{1}[A-Z]{1}[A-Z_0-9]{5}");
        }

        public static bool IsIP(this string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
