using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSFinalProjectClasses.ProductMgmt
{
    public class PlaceOrder
    {
        public int Id { get; set; }
        public string BuyerName { get; set; }
        public string EmailAddress { get; set; }
        public string FullAddress { get; set; }
        public bool IsActive { get; set; }
        public double Phone { get; set; }

        public List<CartItem> CartItems { get; set; }

    }
}
