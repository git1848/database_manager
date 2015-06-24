using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseFront.Entity
{
    public class SyncObject
    {
        public MGTableInfo From { get; set; }
        public MGTableInfo To { get; set; }
    }

    public class SyncInfo
    {
        public string FromTableName { get; set; }
        public string ToTableName { get; set; }
        public Dictionary<string, string> SetColumns { get; set; }
    }
}
