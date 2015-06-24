using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseFront
{
    public static class Extensions_String
    {
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str">格式项</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static string Format(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        /// <summary>
        /// 将String类型转换成Int32类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt32(this string str)
        {
            int Int32 = 0;
            try
            {
                Int32 = Convert.ToInt32(str);
            }
            catch
            {
                throw new Exception("该对象转换成Int32类型失败");
            }
            return Int32;
        }

        /// <summary>
        ///  将String类型转换成DateTime类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime toDateTime(this string str)
        {
            return Convert.ToDateTime(str);
        }

        /// <summary>
        /// 将字符串转换成decimal类型，但是不会报错，如转换不成功则返回0;
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal TryToDecimal(this string str)
        {
            try
            {
                return Convert.ToDecimal(str);

            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 检查字符串是否是空(IsNullOrEmpty)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 检查字符串是否是空(IsNullOrEmpty)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 比较两个字符串的大小，大于或等于都返回True，小于则返回False
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str1"></param>
        /// <returns></returns>
        public static bool BiggerThan(this string str, string str1)
        {
            int intResult = string.CompareOrdinal(str, str1);
            if (intResult == 1)
                return true;
            return false;
        }

        /// <summary>
        /// 比较两个字符串的大小，小于或等于都返回True，小于则返回False
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str1"></param>
        /// <returns></returns>
        public static bool SmallerThan(this string str, string str1)
        {
            int intResult = string.CompareOrdinal(str, str1);
            if (intResult == -1)
                return true;
            return false;
        }

        public static bool BetweenIn(this string str, string strStart, string strEnd)
        {
            return (str.BiggerThan(strStart) && str.SmallerThan(strEnd));
        }

        /// <summary>
        /// 用于多条件查询时候来确定查询条件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str1"></param>
        /// <returns></returns>
        public static bool IfAnd(this string str, string str1)
        {
            return (string.IsNullOrEmpty(str1) || (str == str1));
        }

        /// <summary>
        /// 用于多条件查询时候来确定查询条件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <returns></returns>
        public static bool IfWhere(this string str, string strStart, string strEnd)
        {
            if (string.IsNullOrEmpty(strStart) || string.IsNullOrEmpty(strEnd))
                return true;
            return (str.BiggerThan(strStart) && str.SmallerThan(strEnd));
        }

        /// <summary>
        /// 将字符串转换成Datetim再比较 ,用于多条件查询时候来确定查询条件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <returns></returns>
        public static bool DataTimeIfWhere(this string str, string strStart, string strEnd)
        {
            if (string.IsNullOrEmpty(strStart) || string.IsNullOrEmpty(strEnd))
                return true;
            DateTime dt = new DateTime();
            DateTime dtStart = new DateTime();
            DateTime dtEnd = new DateTime();
            try
            {
                dt = Convert.ToDateTime(str);
                dtStart = Convert.ToDateTime(strStart);
                dtEnd = Convert.ToDateTime(strEnd);
            }
            catch
            {
                throw new Exception("将字符串转换成DateTime失败");
            }

            return (dt > dtStart && dt < dtEnd);
        }

        /// <summary>
        /// 将字符串转换成Int再比较 ,用于多条件查询时候来确定查询条件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <returns></returns>
        public static bool IntIfWhere(this string str, string strStart, string strEnd)
        {
            if (string.IsNullOrEmpty(strStart) || string.IsNullOrEmpty(strEnd))
                return true;
            int var = 0;
            int varStart = 0;
            int varEnd = 0;
            try
            {
                var = Convert.ToInt32(str);
                varStart = Convert.ToInt32(strStart);
                varEnd = Convert.ToInt32(strEnd);
            }
            catch
            {
                throw new Exception("将字符串转换成DateTime失败");
            }

            return (var > varStart && var < varEnd);
        }
    }
}
