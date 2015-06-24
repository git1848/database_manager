using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseFront.DB.DbParams
{
    [Serializable]
    public class MySqlParam : IDbParam
    {
        public string ConnectIcon { get { return "mysql"; } }
        public string UnConnectIcon { get { return "mysql_un"; } }
        public DbProvider DbProvider { get; set; }
        public string DbName { get; set; }
        public string ServerName { get; set; }
        public int Port { get; set; }
        public string UserID { get; set; }
        public string UserPass { get; set; }
    }
}
