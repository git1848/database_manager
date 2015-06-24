using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseFront.DB.DbParams
{
    [Serializable]
    public class SqlServerParam : IDbParam
    {
        public string ConnectIcon { get { return "sqlserver2005"; } }
        public string UnConnectIcon { get { return "sqlserver2005_un"; } }
        public DbProvider DbProvider { get; set; }
        public string DbName { get; set; }
        public string ServerName { get; set; }
        public string UserID { get; set; }
        public string UserPass { get; set; }
    }
}
