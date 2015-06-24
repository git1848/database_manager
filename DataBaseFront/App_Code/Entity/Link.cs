using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseFront.DB.DbOperates;
using DataBaseFront.DB.DbParams;

namespace DataBaseFront.Entity
{
    public class Link
    {
        public string AliasName { get; set; }

        public IDbParam DbParam { get; set; }

        public IDbOperate DbOperate { get; set; }

        public bool HasOpen { get; set; }
    }
}
