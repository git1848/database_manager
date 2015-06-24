using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseFront
{
    public class MGDataBase
    {
        /// <summary>数据库名称</summary>
        public string Name { get; set; }
    }

    public class MGTable
    {
        /// <summary>表名称</summary>
        public string Name { get; set; }
    }

    public class MGColumn
    {
        /// <summary>序号</summary>
        public int Index { get; set; }

        /// <summary>字段名称</summary>
        public string Name { get; set; }

        /// <summary>字段备注</summary>
        public string Remark { get; set; }

        /// <summary>是否是自动增长列</summary>
        public bool AutoIncrement { get; set; }

        /// <summary>是否是主键</summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>字段类型</summary>
        public string DbType { get; set; }

        /// <summary>字段长度</summary>
        public int Length { get; set; }

        /// <summary>字段精度</summary>
        public int NumericPrecision { get; set; }

        /// <summary>小数点位数</summary>
        public int NumericScale { get; set; }

        /// <summary>是否允许为空</summary>
        public bool AllowNull { get; set; }

        /// <summary>默认值</summary>
        public string DefaultValue { get; set; }
    }

    public class MGView
    {
        /// <summary>视图名称</summary>
        public string Name { get; set; }
    }

    public class MGProcedure
    {
        /// <summary>存储过程名称</summary>
        public string Name { get; set; }
    }

    public class MGTableInfo
    {
        /// <summary>表</summary>
        public MGTable Table { get; set; }

        /// <summary>列集合</summary>
        public IList<MGColumn> Columns { get; set; }
    }

    public class MGPrimaryKeyColumn
    {
        /// <summary>字段名称</summary>
        public string ColumnName { get; set; }

        /// <summary>主键的序号</summary>
        public int Seq { get; set; }

        /// <summary>主键名称</summary>
        public string PrimaryKeyName { get; set; }
    }
}
