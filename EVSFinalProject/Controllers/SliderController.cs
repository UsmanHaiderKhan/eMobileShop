using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EVSFinalProjectClasses;
using EVSFinalProjectClasses.ProductMgmt;
using EVSFinalProjectClasses.UserManagment;

namespace EVSFinalProject.Controllers
{
    public class SliderController : Controller
    {
        // GET: Slider
        public ActionResult Index()
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Products", act = "Create" });
            ProductHandler h = new ProductHandler();
            List<Slider> b = h.GetBrand();
            return View(b);

        }
        [HttpGet]
        public ActionResult AddImages()
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });
            return View();
        }

        [HttpPost]
        public ActionResult AddImages(FormCollection data)
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });
            dbcontext db = new dbcontext();
            Slider brand = new Slider();


            brand.Title = data["Title"];
            brand.Description = data["Description"];

            int counter = 0;
            long uno = DateTime.Now.Ticks;
            foreach (string fcName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fcName];
                if (!string.IsNullOrWhiteSpace(file.FileName))
                {
                    string url = "/Content/Images/Slider/" + $"{uno}_{++counter}{file.FileName.Substring(file.FileName.LastIndexOf('.'))}";
                    string path = Request.MapPath(url);
                    file.SaveAs(path);
                    brand.BrandImage = url;
                    brand.Priority = counter;
                    db.Sliders.Add(brand);
                    db.SaveChanges();
                }
            }


            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            dbcontext db = new dbcontext();
            Slider p = (from c in db.Sliders where c.Id == id select c).FirstOrDefault();
            db.Entry(p).State = EntityState.Deleted;
            db.SaveChanges();
            return Json("Delete", JsonRequestBehavior.AllowGet);
        }
        public ActionResult PostImages()
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Slider", act = "AddImages" });

            ProductHandler h = new ProductHandler();
            List<Slider> b = h.GetBrand();
            ViewBag.brand = b;
            return PartialView("~/Views/Slider/_PostImage.cshtml");
        }
    }
}