using EVSFinalProject.Models;
using EVSFinalProjectClasses.ProductMgmt;
using EVSFinalProjectClasses.UserManagment;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EVSFinalProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ProductHandler h = new ProductHandler();
            List<Slider> b = h.GetBrand();

            ViewBag.products = ModelHelper.ProductSummeryList(new ProductHandler().GetLatestMobiles(12));
            return View(b);
        }
        [HttpGet]
        public ActionResult ByCategory(int id)
        {
            ViewBag.ByCategory = ModelHelper.ProductSummeryList(new ProductHandler().GetMobilesByBrands(new MobileBrand { Id = id }));
            MobileBrand mb = new ProductHandler().GetBrandById(id);
            return View(mb);
        }

        public ActionResult ViewDetail(int id)
        {
            ProductHandler ph = new ProductHandler();
            List<Mobile> p = ph.GetMobiles();
            dbcontext db = new dbcontext();

            using (db)
            {
                return View((from c in db.Mobiles
                    .Include(x => x.Images)
                    .Include(x => x.MobileOsVersion)
                    .Include(x => x.MobileBrand)
                    .Include(x => x.MobileOsVersion.MobileOS)
                             where c.Id == id
                             select c).ToList());
            }
        }
        public ActionResult WellCome()
        {
            return View();
        }
        public JsonResult GetWeather()
        {
            Weather weather = new Weather();
            return Json(weather.GetWeatherForcast(), JsonRequestBehavior.AllowGet);

        }
    }
}
