using System;
using System.Collections.Generic;
using System.Text;

namespace $NameSpace.$TableName
{
    /// <summary>
    /// 
    /// </summary>
    public class $TableName
    {
#foreach( $columnInfo in $ColumnInfos )
        ///<summary>$columnInfo.ColumnDesc</summary>
        public $columnInfo.ColumnType $columnInfo.ColumnName { get; set; }

#end
    }
}
