using EVSFinalProjectClasses.ProductMgmt;
using EVSFinalProjectClasses.UserManagment;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVSFinalProject.Controllers
{
    public class BrandsController : Controller
    {
        // GET: Brands
        public ActionResult Index()
        {
            //ProductHandler h = new ProductHandler();
            //List<MobileBrand> mb = h.GetBrands();
            ViewBag.MobBrands = new dbcontext().MobileBrands.ToList();
            return View();
        }
        [HttpGet]
        public ActionResult AddBrands()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBrands(FormCollection data)
        {
            dbcontext db = new dbcontext();
            try
            {
                MobileBrand b = new MobileBrand();
                b.Name = data["Name"];
                long uno = DateTime.Now.Ticks;
                int counter = 0;
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        string abc = uno + "_" + ++counter +
                                     file.FileName.Substring(file.FileName.LastIndexOf("."));
                        //dont save the url of the Images Save the 
                        string url = "~/Content/Banners/" + abc;
                        string path = Request.MapPath(url);
                        b.ImageUrl = abc;
                        file.SaveAs(path);
                    }
                }
                db.MobileBrands.Add(b);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }




        }
        public ActionResult DeleteBrands(int id)
        {
            dbcontext db = new dbcontext();
            MobileBrand p = (from c in db.MobileBrands where c.Id == id select c).FirstOrDefault();
            if (p == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                db.Entry(p).State = EntityState.Deleted;

                db.SaveChanges();
                return Json("Delete", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult UpdatebyAjex(MobileBrand brand)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                if (brand?.Id != null)
                {
                    db.Entry(brand).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json("update", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.MobileBrands.Add(brand);
                    db.SaveChanges();
                    return Json("Added", JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}