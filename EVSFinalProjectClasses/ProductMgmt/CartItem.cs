using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSFinalProjectClasses.ProductMgmt
{
    public class CartItem
    {
        public int Id { get; set; }
        public int MobileId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public int Qauntity { get; set; }
        public long Amount { get; set; }
    }
}
