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
        //$columnInfo.ColumnDesc
        private $columnInfo.ColumnType $columnInfo.PrivateColumnName;

        ///<summary>$columnInfo.ColumnDesc</summary>
        public $columnInfo.ColumnType $columnInfo.ColumnName
        {
            get { return $columnInfo.PrivateColumnName; }
            set { $columnInfo.PrivateColumnName = value; }
        }

#end
    }
}
