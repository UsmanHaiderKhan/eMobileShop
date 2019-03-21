using EVSFinalProjectClasses;
using EVSFinalProjectClasses.ProductMgmt;
using EVSFinalProjectClasses.UserManagment;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EVSFinalProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });
            List<Country> countries = new LocationHandler().Getcountry();
            //int ordercount = new ProductHandler().GetOrdersCount();
            //ViewBag.ordercount = ordercount;

            return View(countries);
        }
        public ActionResult Customer()
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });
            UserHandler h = new UserHandler();
            List<User> u = h.GetUser();
            return View(u);
        }
        public ActionResult UserDetails(int id)
        {
            User user1 = (User)Session[WebUtils.Current_User];
            if (!(user1 != null && user1.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Admin", act = "UserDetails" });

            UserHandler user = new UserHandler();
            List<User> u = user.GetUser();
            ViewBag.udetails = u;
            dbcontext db = new dbcontext();
            using (db)
            {
                return View((from t in db.Users
                        .Include(x => x.Role)
                        .Include(x => x.Gender)
                        .Include(x => x.City)
                             where t.Id == id
                             select t).ToList());
            }
        }

        public ActionResult DeleteUser(int id)
        {
            dbcontext db = new dbcontext();
            User p = (from c in db.Users
                    .Include(x => x.Role)
                    .Include(x => x.Gender)
                      where c.Id == id
                      select c).FirstOrDefault();
            db.Entry(p).State = EntityState.Deleted;
            db.SaveChanges();
            return Json("Delete", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UpdateUser(int id)
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });
            User u = new UserHandler().GetUserById(id);
            return View(u);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UpdateUser(User newUser, FormCollection fdata)
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });
            dbcontext db = new dbcontext();
            if (ModelState.IsValid)
            {
                using (db)
                {
                    User oldUser = db.Users.Find(newUser.Id);
                    oldUser.Role = newUser.Role;
                    newUser.City = new City { Id = Convert.ToInt32(fdata["CityList"]) };
                    newUser.Gender = new UserHandler().GetGenderByName(newUser.Gender.Name);
                    oldUser.FullName = newUser.FullName;
                    oldUser.BirthDate = newUser.BirthDate;
                    oldUser.Email = newUser.Email;
                    oldUser.LoginId = newUser.LoginId;
                    oldUser.Password = newUser.Password;
                    oldUser.ConfirmPassword = newUser.ConfirmPassword;
                    oldUser.Phone = newUser.Phone;
                    oldUser.SecurityAnswer = newUser.SecurityAnswer;
                    oldUser.SecurityQuestion = newUser.SecurityQuestion;
                    oldUser.IsActive = newUser.IsActive;
                    db.Entry(newUser.Role).State = EntityState.Unchanged;
                    db.Entry(newUser.City).State = EntityState.Unchanged;
                    db.Entry(newUser.Gender).State = EntityState.Unchanged;
                    db.SaveChanges();
                }
                return RedirectToAction("Customer");
            }
            return View();
        }


        public ActionResult Messanger()
        {
            return View();
        }

        public ActionResult DashBoard()
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });
            return View();
        }

        public ActionResult Orders()
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });
            ProductHandler p = new ProductHandler();
            List<PlaceOrder> place = p.GetOrders();
            return View(place);
        }

        public ActionResult DeleteOrders(int id)
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });
            dbcontext db = new dbcontext();
            PlaceOrder p = (from c in db.PlaceOrders.Include(x => x.CartItems) where c.Id == id select c).FirstOrDefault();
            db.Entry(p).State = EntityState.Deleted;
            db.SaveChanges();
            return Json("Delete", JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public ActionResult Locations()
        {

            return View();
        }


        [HttpGet]
        public ActionResult AddCountry()
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });
            return View();
        }

        [HttpPost]
        public ActionResult AddCountry(FormCollection data, Country country)
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });
            dbcontext db = new dbcontext();
            if (ModelState.IsValid)
            {
                Country count = new Country
                {
                    Id = Convert.ToInt32(data["Id"]),
                    Name = data["Name"],
                    Code = Convert.ToInt32(data["Code"])
                };
                db.Countries.Add(count);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        public ActionResult DeleteCountry(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country cou = new LocationHandler().GetCountryById(id);
            if (cou == null)
            {
                return HttpNotFound();
            }
            return View(cou);

        }
        [HttpPost, ActionName("DeleteCountry")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCountryConfirmed(int id)
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });

            Country country = new LocationHandler().GetCountryById(id);
            new LocationHandler().DeleteCountry(country);
            return RedirectToAction("Index");

        }
        public ActionResult Cities(int id)
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });

            Country country = new LocationHandler().GetCountryById(id);
            List<City> cities = new LocationHandler().GetCitiesByCountry(country);
            ViewBag.countryId = id;
            ViewBag.countryName = country.Name;
            return View(cities);
        }
        [HttpGet]
        public ActionResult AddCity(int id)
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });

            ViewBag.countryName = new LocationHandler().GetCountryById(id).Name;
            return View();
        }
        [HttpPost]
        public ActionResult AddCity(FormCollection data, int id)
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });

            City c = new City
            {
                Name = data["Name"],
                Counrty = new LocationHandler().GetCountryById(id)
            };
            new LocationHandler().AddCity(c);

            return RedirectToAction("Cities", new { Id = id });
        }
        public ActionResult DeleteCity(int? id, int countryId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            City c = new LocationHandler().GetCityById(id);

            if (c == null)
            {
                return HttpNotFound();
            }
            ViewBag.countryId = countryId;
            TempData["Country"] = countryId;
            return View(c);
        }
        [HttpPost, ActionName("DeleteCity")]
        [ValidateAntiForgeryToken]
        public ActionResult DelCityConfirmed(int id)
        {
            City ct = new LocationHandler().GetCityById(id);
            new LocationHandler().DelCity(ct);

            return RedirectToAction("Cities", new { Id = Convert.ToInt32(TempData["Country"]) });
        }

        public ActionResult TotalProduct()
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Admin", act = "TotalProduct" });
            ProductHandler ph = new ProductHandler();
            List<Mobile> mobiles = ph.GetMobiles();
            return View(mobiles);
        }

        public ActionResult DeleteProduct(int id)
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "admin", act = "DeleteProduct" });
            dbcontext db = new dbcontext();
            Mobile p = (from c in db.Mobiles where c.Id == id select c).FirstOrDefault();
            db.Entry(p).State = EntityState.Deleted;
            db.SaveChanges();
            return Json("Delete", JsonRequestBehavior.AllowGet);
        }

    }
}
