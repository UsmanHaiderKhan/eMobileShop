﻿using EVSFinalProjectClasses.ProductMgmt;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EVSFinalProjectClasses.UserManagment;

namespace EVSFinalProject.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderView(int id)
        {
            ProductHandler ph = new ProductHandler();
            List<PlaceOrder> pl = ph.GetOrders();
            ViewBag.POrders = pl;
            dbcontext db = new dbcontext();
            using (db)
            {
                return View((from s in db.PlaceOrders
                             .Include(t => t.CartItems)
                             where s.Id == id
                             select s).ToList());
            }
        }

        [HttpGet]
        public ActionResult ChangePassword(int id)
        {
            // TempData["Id"] = id;
            ViewBag.userId = id;
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(FormCollection formdata, int id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                User user = db.Users.Find(id);
                if (user != null)
                {

                    user.Password = formdata["Password"];
                    user.ConfirmPassword = formdata["ConfirmPassword"];
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("login", "Users");
                }
            }
            return View();
        }

        public int GetOrderByCount()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.CartItems select c).Count();
            }
        }
        public int GetUserByCount()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Users select c).Count();
            }
        }
        public int GetProductsByCount()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Mobiles select c).Count();
            }
        }
        public int GetBrandsByCount()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.MobileBrands select c).Count();
            }
        }
        public int GetOsByCount()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.MobileOss select c).Count();
            }
        }
        public int GetcountryByCount()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Countries select c).Count();
            }
        }
    }
}