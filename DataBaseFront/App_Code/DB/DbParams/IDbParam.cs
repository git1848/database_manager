using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseFront.DB.DbParams
{
    public interface IDbParam
    {
        string ConnectIcon { get; }
        string UnConnectIcon { get; }
        DbProvider DbProvider { get; set; }
        string DbName { get; set; }
    }
}
