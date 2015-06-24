using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseFront.Entity
{
    public class ModelEntity
    {
        /// <summary>变量名称</summary>
        public string PrivateColumnName { get; set; }

        /// <summary>属性名称</summary>
        public string ColumnName { get; set; }

        /// <summary>字段类型</summary>
        public string ColumnType { get; set; }

        /// <summary>字段描述</summary>
        public string ColumnDesc { get; set; }
    }
}
