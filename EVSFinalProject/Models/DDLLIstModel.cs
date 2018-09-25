using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVSFinalProject.Models
{
    public class DDLLIstModel
    {
        public string Name { get; set; }
        public IEnumerable<SelectListItem> Values { get; set; }
        public string Caption { get; set; }
    }
}