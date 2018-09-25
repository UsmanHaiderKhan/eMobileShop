using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EVSFinalProjectClasses.ProductMgmt
{
    public class Mobile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public bool BlueTooth { get; set; }
        public bool ThreeG { get; set; }
        public bool FourG { get; set; }
        public bool Wifi { get; set; }
        public int Ram { get; set; }
        public int FrontCam { get; set; }
        public int BackCam { get; set; }
        public string Color { get; set; }
        public int ScreenSize { get; set; }
        public int Battery { get; set; }
        public int? Stock { get; set; }
        public string Accessories { get; set; }
        public DateTime Warranty { get; set; }
        public string Features { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public virtual MobileBrand MobileBrand { get; set; }
        public virtual MobileOSVersion MobileOsVersion { get; set; }
        public virtual ICollection<MobileImage> Images { get; set; }

        public Mobile()
        {
            Images = new List<MobileImage>();
        }

    }
}