using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EVSFinalProjectClasses.ProductMgmt;
using EVSFinalProjectClasses.UserManagment;

namespace EVSFinalProject.Controllers
{
    public class OperatingSystemController : Controller
    {
        // GET: OperatingSystem
        public ActionResult Index()
        {
            List<MobileOS> os = new ProductHandler().GetOperatingSystem();

            return View(os);
        }
        [HttpGet]
        public ActionResult AddOperatingSystem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOperatingSystem(FormCollection data, MobileOS os)
        {
            dbcontext db = new dbcontext();
            if (ModelState.IsValid)
            {
                MobileOS mos = new MobileOS();
                mos.Id = Convert.ToInt32(data["Id"]);
                mos.Name = data["Name"];
                db.MobileOss.Add(mos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(os);
        }


        public ActionResult DeleteOS(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobileOS os = new ProductHandler().GetOperatingSystemById(id);
            if (os == null)
            {
                return HttpNotFound();
            }
            return View(os);

        }
        [HttpPost, ActionName("DeleteOS")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOSConfirmed(int id)
        {
            MobileOS operatingsystem = new ProductHandler().GetOperatingSystemById(id);
            new ProductHandler().DeleteOperatingSystem(operatingsystem);
            return RedirectToAction("Index");

        }


        public ActionResult OSVersion(int id)
        {
            MobileOS os = new ProductHandler().GetOperatingSystemById(id);
            List<MobileOSVersion> osv = new ProductHandler().GetOSVersionByOS(os);
            ViewBag.OSId = id;
            ViewBag.OperatingSystem = os.Name;
            return View(osv);
        }
        [HttpGet]
        public ActionResult AddOSVersion(int id)
        {
            ViewBag.OperatingSystem = new ProductHandler().GetMobileOSById(id).Name;
            return View();
        }
        [HttpPost]
        public ActionResult AddOSVersion(FormCollection data, int id)
        {
            MobileOSVersion c = new MobileOSVersion();
            c.Name = data["Name"];
            c.MobileOS = new ProductHandler().GetOperatingSystemById(id);
            new ProductHandler().AddOSVersion(c);

            return RedirectToAction("OSVersion", new { Id = id });
        }
        public ActionResult DeleteOSVersion(int? id, int osid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MobileOSVersion c = new ProductHandler().GetOSVersionById(osid);

            if (c == null)
            {
                return HttpNotFound();
            }
            ViewBag.OSId = osid;
            TempData["MobileOS"] = osid;
            return View(c);
        }
        [HttpPost, ActionName("DeleteOSVersion")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOSVersionConfirmed(int id)
        {
            MobileOSVersion ct = new ProductHandler().GetOSVersionById(id);
            new ProductHandler().DeleteOSVersion(ct);

            return RedirectToAction("OSVersion", new { Id = Convert.ToInt32(TempData["MobileOS"]) });
        }
    }
}