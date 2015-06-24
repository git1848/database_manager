using System;
using System.ComponentModel;

namespace DataBaseFront
{
    public enum DbProvider
    {
        [Description("SQL2005 数据库")]
        SQL2005 = 1,

        [Description("MySQL   数据库")]
        MySQL = 2,

        [Description("Sqlite  数据库")]
        Sqlite = 3,

        [Description("Access  数据库")]
        Access = 4,

        [Description("Oracle  数据库")]
        Oracle = 5,
    }
}