using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVSFinalProjectClasses.ProductMgmt;

namespace EVSFinalProject.Models
{
    public class ShoppingCart
    {
        private List<ShoppingCartItems> items;


        public ShoppingCart()
        {
            items = new List<ShoppingCartItems>();
        }

        public List<ShoppingCartItems> Items
        {
            get { return items; }
        }


        public void Add(ShoppingCartItems newItem)
        {
            ShoppingCartItems itemFound = items.Find(i => i.Id == newItem.Id);

            if (itemFound == null)
            {
                items.Add(newItem);
                items.TrimExcess();
            }
            else
            {
                itemFound.Quantity += newItem.Quantity;
            }
        }

        internal void Remove(int id)
        {
            items.RemoveAt(items.FindIndex(i => i.Id == id));
        }

        public int NumberOfItems
        {
            get
            {
                return items.Count();
            }
        }

        internal void Update(int id, int qty)
        {
            ShoppingCartItems itemFound = items.Find(i => i.Id == id);
            if (itemFound != null)
            {
                itemFound.Quantity = qty;
            }
        }
        public float Calculate(IEnumerable<ShoppingCartItems> items)
        {
            return items.Sum(cartItem => (cartItem.Price * cartItem.Quantity));
        }

        public float GrandTotal
        {
            get { return Calculate(Items); }
        }
    }
}