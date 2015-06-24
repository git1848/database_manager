using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseFront.DB.DbParams
{
    [Serializable]
    public class SqliteParam : IDbParam
    {
        public string ConnectIcon { get { return "sqlite"; } }
        public string UnConnectIcon { get { return "sqlite_un"; } }
        public DbProvider DbProvider { get; set; }
        public string DbName { get; set; }
        public string DbPath { get; set; }
    }
}
