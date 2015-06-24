using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseFront
{
    public static partial class Extensions_StringSql
    {

        #region ------------String类型的扩展-----------

        /// <summary>
        /// return  " and UserId='Admin'"
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.strEquals(txtUserId.text,"UserID")
        /// </example>
        public static string strEquals(this string strSql, string strValue, string ColName)
        {
            if (!string.IsNullOrEmpty(strValue))
                return string.Format(strSql + "  and {0}='{1}'  ", ColName, strValue);
            else
                return strSql;
        }

        /// <summary>
        /// return "  and VBELN > '107100000'  "
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.strBigger(txtVBELB.text,"VBELN");
        /// </example>
        public static string strBigger(this string strSql, string strValue, string ColName)
        {
            if (!string.IsNullOrEmpty(strValue))
                return string.Format(strSql + " and {0}>'{1}' ", ColName, strValue);
            else
                return strSql;
        }

        /// <summary>
        /// return "  and VBELN < '107100000'  "
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.strSmaler(txtVBELB.text,"VBELN");
        /// </example>
        public static string strSmaller(this string strSql, string strValue, string ColName)
        {
            if (!string.IsNullOrEmpty(strValue))
                return string.Format(strSql + " and {0}<'{1}' ", ColName, strValue);
            else
                return strSql;
        }

        /// <summary>
        /// return "  and UserName <like '%Tom%'  "
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.strLike(txtUserName.text,"Tom");
        /// </example>
        public static string strLike(this string strSql, string strValue, string ColName)
        {
            if (!string.IsNullOrEmpty(strValue))
                return string.Format(strSql + " and {0} like '%{1}%' ", ColName, strValue);
            else
                return strSql;
        }

        /// <summary>
        /// return "  and UserName <like 'Tom%'  "
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.strStartLike(txtUserName.text,"UserName");
        /// </example>
        public static string strStartLike(this string strSql, string strValue, string ColName)
        {
            if (!string.IsNullOrEmpty(strValue))
                return string.Format(strSql + " and {0} like '{1}%' ", ColName, strValue);
            else
                return strSql;
        }

        /// <summary>
        /// return "  and UserName <like '%Tom'  "
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.strEndLike(txtUserName.text,"UserName");
        /// </example>
        public static string strEndLike(this string strSql, string strValue, string ColName)
        {
            if (!string.IsNullOrEmpty(strValue))
                return string.Format(strSql + " and {0} like '%{1}' ", ColName, strSql);
            else
                return strValue;
        }

        /// <summary>
        /// Return " VBELN>'107111110' and VBELN <'107111113'"
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <remarks>
        /// 针对字符串区间的比较，如果起始都没有值的话，则不加限定条件，如果都有值的话则取中间，
        /// 如果其中一个有值的话去相等
        /// </remarks>
        /// <example>
        /// strSql.strEqualsOrBetween("107111110","107111113",VBELN)
        /// </example>
        public static string strEqualsOrBetween(this string strSql, string strStart, string strEnd, string ColName)
        {
            if (string.IsNullOrEmpty(strStart) && string.IsNullOrEmpty(strEnd))
                return strSql;
            else if (!string.IsNullOrEmpty(strStart) && !string.IsNullOrEmpty(strEnd))
            {
                return strSql.strBigger(strStart, ColName).strSmaller(strEnd, ColName);
            }
            else if (string.IsNullOrEmpty(strStart) && !string.IsNullOrEmpty(strEnd))
                return strSql.strEquals(strEnd, ColName);
            else
                return strSql.strEquals(strStart, ColName);
        }

        #endregion



        #region --------------Int类型的扩展-------------

        /// <summary>
        /// return " and Age=18"
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="IntValue"></param>
        /// <param name="IntDefault"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.IntEquals(IntAge,0,"Age")
        /// </example>
        /// <remarks>
        /// IntDefault是对比的默认值，只有当输入值不等于IntDefault时候，才加入对比条件
        /// </remarks>
        public static string IntEquals(this string strSql, int IntValue, int IntDefault, string ColName)
        {
            if (IntValue != IntDefault)
                return string.Format(strSql + " {0}={1} ", ColName, IntValue);
            else
                return strSql;
        }

        /// <summary>
        /// Default值为零,Return"  and Age=18  "
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="IntValue"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.IntEquals(IntAge,"Age")
        /// </example>
        public static string IntEquals(this string strSql, int IntValue, string ColName)
        {
            return IntEquals(strSql, IntValue, 0, ColName);
        }

        /// <summary>
        ///  return " and Age>18"
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="IntValue"></param>
        /// <param name="IntDefault"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.IntBigger(IntAge,0,"Age")
        /// </example>
        /// <remarks>
        /// IntDefault是对比的默认值，只有当输入值不等于IntDefault时候，才加入对比条件
        /// </remarks>
        public static string IntBigger(string strSql, int IntValue, int IntDefault, string ColName)
        {
            if (IntValue != IntDefault)
                return string.Format(strSql + " {0}>{1} ", ColName, IntDefault);
            else
                return strSql;
        }

        /// <summary>
        /// Default值为零,Return"  and Age=18  "
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="IntValue"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.IntBigger(IntAge,"Age")
        /// </example>
        public static string IntBigger(this string strSql, int IntValue, string ColName)
        {
            return IntBigger(strSql, IntValue, 0, ColName);
        }

        /// <summary>
        ///  return " and Age<18"
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="IntValue"></param>
        /// <param name="IntDefault"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.IntSmaller(IntAge,0,"Age")
        /// </example>
        /// <remarks>
        /// IntDefault是对比的默认值，只有当输入值不等于IntDefault时候，才加入对比条件
        /// </remarks>
        public static string IntSmaller(this string strSql, int IntValue, int IntDefault, string ColName)
        {
            if (IntValue != IntDefault)
                return string.Format(strSql + " {0}<{1} ", ColName, IntDefault);
            else
                return strSql;
        }

        /// <summary>
        /// Default值为零,Return"  and Age=18  "
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="IntValue"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.IntSmaller(IntAge,"Age")
        /// </example>
        public static string IntSmaller(this string strSql, int IntValue, string ColName)
        {
            return IntSmaller(strSql, IntValue, 0, ColName);
        }

        /// <summary>
        /// Return " Age>18 and Age <20"
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="IntStart"></param>
        /// <param name="IntEnd"></param>
        /// <param name="IntDefault"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <remarks>
        /// 针对整形区间的比较，如果起始都没有值的话，则不加限定条件，如果都有值的话则取中间，
        /// 如果其中一个有值的话取 相等
        /// </remarks>
        /// <example>
        /// strSql.IntEqualsOrBetween(18,20,0,Age)
        /// </example>
        public static string IntEqualsOrBetween(this string strSql, int IntStart, int IntEnd, int IntDefault, string ColName)
        {
            if (IntStart == IntDefault && IntEnd == IntDefault)
                return strSql;
            else if (IntStart != IntDefault && IntEnd != IntDefault)
                return strSql.IntBigger(IntStart, ColName)
                             .IntSmaller(IntEnd, ColName);
            else if (IntStart == IntDefault && IntEnd != IntDefault)
                return strSql.IntEquals(IntEnd, ColName);
            else
                return strSql.IntEquals(IntStart, ColName);
        }

        /// <summary>
        /// Return " Age>18 and Age <20"
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="IntStart"></param>
        /// <param name="IntEnd"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <remarks>
        /// 设定的默认比较值为零
        /// </remarks>
        public static string IntEqualsOrBetween(this string strSql, int IntStart, int IntEnd, string ColName)
        {
            return IntEqualsOrBetween(strSql, IntStart, IntEnd, 0, ColName);
        }

        #endregion



        #region --------------Datetime类型的扩展-------------

        /// <summary>
        /// Return " and CreateDate='2012-9-10 12:47:44' "
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="DtValue"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.DtEquals("2012-9-10 12:47:44",CreateTime);
        /// </example>
        public static string DtEquals(this string strSql, DateTime DtValue, string ColName)
        {
            if (!string.IsNullOrEmpty(DtValue.ToString()))
                return string.Format(strSql + " and {0}='{1}' ", ColName, DtValue);
            else
                return strSql;
        }

        /// <summary>
        /// Return " and CreateDate>'2012-9-10 12:47:44' "
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="DtValue"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.DtBigger("2012-9-10 12:47:44",CreateTime);
        /// </example>
        public static string DtBigger(this string strSql, DateTime DtValue, string ColName)
        {
            if (!string.IsNullOrEmpty(DtValue.ToString()))
                return string.Format(strSql + " and {0}>'{1}' ", ColName, DtValue);
            else
                return strSql;
        }

        /// <summary>
        /// Return " and CreateDate<'2012-9-10 12:47:44' "
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="DtValue"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <example>
        /// strSql.DtSmaller("2012-9-10 12:47:44",CreateTime);
        /// </example>
        public static string DtSmaller(this string strSql, DateTime DtValue, string ColName)
        {
            if (!string.IsNullOrEmpty(DtValue.ToString()))
                return string.Format(strSql + " and {0}<'{1}' ", ColName, DtValue);
            else
                return strSql;
        }

        /// <summary>
        /// Return " CreateDate>'2012-9-10 13:00:38' and CreateDate <'2012-9-10 13:00:42'"
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        /// <remarks>
        /// 针对时间区间的比较，如果起始都没有值的话，则不加限定条件，如果都有值的话则取中间，
        /// 如果其中一个有值的话去相等
        /// </remarks>
        /// <example>
        /// strSql.DtEqualsOrBetween("2012-9-10 13:00:38","2012-9-10 13:00:42",VBELN)
        /// </example>
        public static string DtEqualsOrBetween(this string strSql, DateTime DtStart, DateTime DtEnd, string ColName)
        {
            if (!string.IsNullOrEmpty(DtStart.ToString()) && !string.IsNullOrEmpty(DtEnd.ToString()))
                return strSql.DtBigger(DtStart, ColName)
                             .DtSmaller(DtEnd, ColName);
            else if (string.IsNullOrEmpty(DtStart.ToString()) && string.IsNullOrEmpty(DtEnd.ToString()))
                return strSql;
            else if (string.IsNullOrEmpty(DtStart.ToString()) && !string.IsNullOrEmpty(DtEnd.ToString()))
                return strSql.DtEquals(DtEnd, ColName);
            else
                return strSql.DtEquals(DtStart, ColName);
        }

        #endregion

    }
}