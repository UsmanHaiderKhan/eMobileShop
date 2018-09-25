using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVSFinalProject.Controllers
{
    public class BannerController : Controller
    {
        // GET: Banner
        public ActionResult Index()
        {
            return View();
        }
       public ActionResult AddBanner()
        {
            return View();
        }
        public ActionResult UpdateBanner()
        {
            return View();

        }
        public ActionResult DeleteBanner()
        {
            return View();
        }

    }
}