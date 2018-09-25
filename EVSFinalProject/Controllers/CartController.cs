using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EVSFinalProject.Models;
using EVSFinalProjectClasses;
using EVSFinalProjectClasses.UserManagment;
using EVSFinalProjectClasses.ProductMgmt;

namespace EVSFinalProject.Controllers
{
    public class CartController : Controller
    {
        // GET: Cartt
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public int Add(ShoppingCartItems item)
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];
            if (cart == null) cart = new ShoppingCart();
            cart.Add(item);
            Session.Add(WebUtils.Cart, cart);
            return cart.NumberOfItems;
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];
            if (cart != null)
            {
                cart.Remove(id);
                Session.Add(WebUtils.Cart, cart);
            }
            return RedirectToAction("ViewCart");
        }

        public int Update()
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];
            if (cart != null)
            {
                cart.Update(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["qty"]));
                Session.Add(WebUtils.Cart, cart);
            }
            return cart.NumberOfItems;
        }

        [HttpGet]
        public ActionResult ViewCart()
        {

            ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];
            if (cart == null)
            {
                return RedirectToAction("Empty", "Product");
            }
            if (cart.GrandTotal == null)
            {
                ViewBag.GrandTotal = null;
                return View();
            }
            ViewBag.GrandTotal = cart.GrandTotal;
            return View();
        }


        public int ItemsCount()
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];
            return (cart == null) ? 0 : cart.NumberOfItems;
        }

        //here we post Our Data 
        //[HttpPost]
        //public ActionResult PlaceOrder(FormCollection fm)
        //{
        //    dbcontext context = new dbcontext();
        //    FinalOrder fo = new FinalOrder();
        //    fo.FullName = fm["fullname"];
        //    fo.Address = fm["address"];
        //    fo.IsActive = false;
        //    //fo.Cartitems=
        //    List<CartItems> crtlist = new List<CartItems>();
        //    ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];//here we get The data thr data from DB by using loop
        //    foreach (var i in cart.Items)
        //    {
        //        CartItems ci = new CartItems();//calling the Required Thing
        //        ci.Id = i.Id;
        //        ci.ImageUrl = i.ImageUrl;
        //        ci.Name = i.Name;
        //        ci.Price = i.Price;
        //        ci.Qauntity = i.Quantity;
        //        ci.Amount = i.Amount;
        //        crtlist.Add(ci);
        //    }
        //    fo.Cartitems = crtlist;
        //    foreach (var i in crtlist)
        //    {
        //        context.CartItemses.Add(i);
        //    }

        //    context.FinalOrders.Add(fo);
        //    context.SaveChanges();


        //    return RedirectToAction("Index", "Admin", null);
        //}

    }
}