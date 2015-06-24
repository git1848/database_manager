using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseFront.DB.DbParams
{
    [Serializable]
    public class OracleParam : IDbParam
    {
        public string ConnectIcon { get { return "oracle"; } }
        public string UnConnectIcon { get { return "oracle_un"; } }
        public DbProvider DbProvider { get; set; }
        public string DbName { get; set; }
        public string ServerName { get; set; }
        public string UserID { get; set; }
        public string UserPass { get; set; }
    }
}
