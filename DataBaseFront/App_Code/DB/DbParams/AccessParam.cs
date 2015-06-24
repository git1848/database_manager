using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseFront.DB.DbParams
{
    [Serializable]
    public class AccessParam : IDbParam
    {
        public string ConnectIcon { get { return "access2007"; } }
        public string UnConnectIcon { get { return "access2007_un"; } }
        public DbProvider DbProvider { get; set; }
        public string DbName { get; set; }
        public string DbPath { get; set; }
        public bool HasPassword { get; set; }
        public string DbPassword { get; set; }
    }
}
