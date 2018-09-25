using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVSFinalProjectClasses.ProductMgmt
{
    public class MobileOSVersion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual MobileOS MobileOS { get; set; }

    }
}