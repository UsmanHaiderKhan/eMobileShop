using EVSFinalProject.Models;
using EVSFinalProjectClasses;
using EVSFinalProjectClasses.ProductMgmt;
using EVSFinalProjectClasses.UserManagment;
using SirAliClass.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EVSFinalProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [HttpGet]
        public ActionResult Latest()
        {
            ViewBag.LatestProduct = ModelHelper.ProductSummeryList(new ProductHandler().GetLatestMobiles(8));
            return View();
        }

        [HttpGet]
        public ActionResult Manage()
        {
            int qty = Convert.ToInt32(ConfigurationManager.AppSettings["TopProductsQuantity"]);
            ViewBag.ProductList =
                ModelHelper.ProductSummeryList(new ProductHandler().GetMobiles().Take((qty == 0) ? 4 : qty));
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Products", act = "Create" });

            ProductHandler mHandler = new ProductHandler();
            ViewBag.OSList = ModelHelper.ToSelectItemList(mHandler.GetOperatingSystem());
            ViewBag.BrandsList = ModelHelper.ToSelectItemList(mHandler.GetBrands());
            //TempData.Add("Message", new AlertMessage("Sorry Your Product Does Not Added.... ,Try Again...!", AlertMessageType.Error));
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection data)
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Products", act = "Create" });
            try
            {
                Mobile m = new Mobile();

                m.Name = data["name"];
                m.Price = Convert.ToSingle(data["price"]);
                m.MobileOsVersion = new MobileOSVersion { Id = Convert.ToInt32(data["osversion"]) };

                m.ReleaseDate = Convert.ToDateTime(data["ReleaseDate"]);
                m.Stock = Convert.ToInt32(data["Stock"]);
                m.MobileBrand = new MobileBrand { Id = Convert.ToInt32(data["brand"]) };
                m.Accessories = data["Accessories"];
                m.BackCam = Convert.ToInt32(data["BackCam"]);
                m.FrontCam = Convert.ToInt32(data["FrontCam"]);
                m.Battery = Convert.ToInt32(data["Battery"]);
                m.Color = data["Color"];
                m.Features = data["Features"];
                m.Warranty = Convert.ToDateTime(data["Warranty"]);
                m.ScreenSize = Convert.ToInt32(data["ScreenSize"]);
                m.Ram = Convert.ToInt32(data["Ram"]);
                m.Wifi = Convert.ToBoolean(data["wifi"].Split(',').First());
                m.BlueTooth = Convert.ToBoolean(data["bluetooth"].Split(',').First());
                m.ThreeG = Convert.ToBoolean(data["3g"].Split(',').First());
                m.FourG = Convert.ToBoolean(data["4g"].Split(',').First());
                m.Description = data["description"];
                long uno = DateTime.Now.Ticks;
                int counter = 0;
                foreach (string fcName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fcName];
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        string url = "/Content/Images/" + uno + "_" + ++counter +
                                     file.FileName.Substring(file.FileName.LastIndexOf("."));
                        string path = Request.MapPath(url);
                        //m.Images = file.FileName;
                        file.SaveAs(path);
                        m.Images.Add(new MobileImage { Url = url, Perority = counter });
                    }
                }

                new ProductHandler().Add(m);
                TempData.Add("Message",
                    new AlertMessage("Congratulation Product Added Successfully", AlertMessageType.Success));

            }
            catch (Exception e)
            {

                TempData.Add("Message",
                    new AlertMessage("Sorry Your Product Does Not Added.... ,Try Again...!", AlertMessageType.Error));
                ViewBag.OSList = ModelHelper.ToSelectItemList(new ProductHandler().GetOperatingSystem());
                ViewBag.BrandsList = ModelHelper.ToSelectItemList(new ProductHandler().GetBrands());
                throw e;
            }

            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult OsVersions(int id)
        {
            User user = (User)Session[WebUtils.Current_User];
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Products", act = "Create" });

            DDLLIstModel m = new DDLLIstModel
            {
                Name = "osversion",
                Caption = "- Operating System Version -",
                Values = ModelHelper.ToSelectItemList(new ProductHandler().GetMobileOsVersions(new MobileOS { Id = id }))
            };
            return PartialView("~/Views/Shared/_DDLListView.cshtml", m);
        }


        [HttpGet]
        public ActionResult Categories()
        {
            return PartialView("~/Views/Shared/_CategoryList.cshtml");
        }

        [HttpGet]
        public ActionResult PlaceOrder()
        {

            return View();
        }

        [HttpPost]
        public ActionResult PlaceOrder(FormCollection data)
        {
            dbcontext db = new dbcontext();
            PlaceOrder p = new PlaceOrder
            {
                BuyerName = data["BuyerName"],
                FullAddress = data["FullAddress"],
                EmailAddress = data["EmailAddress"],
                Phone = Convert.ToDouble(data["Phone"]),
                IsActive = false
            };
            List<CartItem> cartItems = new List<CartItem>();
            ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];

            foreach (var i in cart.Items)
            {
                CartItem ci = new CartItem
                {
                    Id = i.Id,
                    Name = i.Name,
                    ImageUrl = i.ImageUrl,
                    Qauntity = i.Quantity,
                    Price = i.Price,
                    Amount = i.Amount,
                    MobileId = i.MobileId
                };
                Mobile bMob = new ProductHandler().GetMobileById(ci.Id);
                //Mobile mm = (Mobile)new dbcontext().Mobiles.Find(ci.Id);

                bMob.Stock = bMob.Stock - 1;
                //mm.Stock = mm.Stock - 1;
                new ProductHandler().UpdateMobile(bMob);

                cartItems.Add(ci);
            }

            p.CartItems = cartItems;

            foreach (var i in cartItems)
            {
                db.CartItems.Add(i);
            }




            db.PlaceOrders.Add(p);
            db.SaveChanges();

            //Here We Sent the Email 
            string message = null;
            try
            {

                if (ModelState.IsValid)
                {
                    var sender = new MailAddress("usmanhaiderkhan4@gmail.com");
                    var reciver = new MailAddress(p.EmailAddress);
                    var password = "03349875662";

                    var p1 = new ProductHandler().GetOrderById(p.Id);
                    if (p1 != null)
                    {
                        var sub = "---- E-Mobile Shopping ----";

                        var body = $"<h2 style='color:#bc4b4b'><center>---Your Buyed Product Information Given Below --- </center></h2><br />" +
                                   $"<div><table style='width:60%;border:1px solid yellow;padding:10px; background-color: #eeeeee; margin: auto;border-collapse: collapse; transition: all 1s;'><tr onMouseOver=this.style.color = 'red''  onMouseOut=this.style.color = 'blue''  style='border:1px solid grey;padding:10px;background-color: #eee;'><th style='border:1px solid grey;padding:10px'>Customer Name:</th><td style='border:1px solid grey;padding:10px'>{p1.BuyerName}</td></tr><tr onMouseOver=this.style.color = 'red''  onMouseOut=this.style.color = 'blue'' style='border:1px solid grey;padding:10px;background-color: pink;'><th style='border:1px solid grey;padding:10px'>Customer Address:</th><td style='border:1px solid grey;padding:10px'>{p1.FullAddress}</td></tr><tr onMouseOver=this.style.color = 'red''  onMouseOut=this.style.color = 'blue'' style='border:1px solid grey;padding:10px;background-color:  #eee;'><th style='border:1px solid grey;padding:10px'>Customer PhoneNo:</th><td style='border:1px solid grey;padding:10px'>{p1.Phone}</td></tr><tr onMouseOver=this.style.color = 'red''  onMouseOut=this.style.color = 'blue'' style='border:1px solid grey;padding:10px;background-color: pink;'><th style='border:1px solid grey;padding:10px'>Product Name:</th><td style='border:1px solid grey;padding:10px'>{p1.CartItems.First().Name}</td></tr><tr onMouseOver=this.style.color = 'red''  onMouseOut=this.style.color = 'blue'' style='background-color:  #eee;border:1px solid grey;padding:10px'><th style='border:1px solid grey;padding:10px'>Product Quantity:</th><td style='border:1px solid grey;padding:10px'>{p1.CartItems.First().Qauntity}</td></tr><tr onMouseOver=this.style.color = 'red'' onMouseOut=this.style.color = 'blue'' style='background-color: pink;border:1px solid grey;padding:10px'><th style='border:1px solid grey;padding:10px'>Product Amount:</th><td style='border:1px solid grey;padding:10px'>{p1.CartItems.First().Amount}</td></tr></table></div>";
                        var smtp1 = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(sender.Address, password)

                        };
                        using (var mailMessage = new MailMessage(sender, reciver)
                        {
                            IsBodyHtml = true,
                            BodyEncoding = UTF8Encoding.UTF8,
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp1.Send(mailMessage);
                            message = "Your Order Details Has Been Sent By Email SuccessFully";

                        }
                    }
                    else
                    {

                        message = "Their is Some Problem in NetWork Sending Email Failed";
                    }
                    //Session.Clear();
                    Session[WebUtils.Cart] = null;
                    return RedirectToAction("Complete");
                }
            }
            catch (Exception e)
            {
                ViewBag.OrderMessageError = $"There Is Some Problem Here Please Try Agin{e}";
            }

            return RedirectToAction("Complete", message);


        }

        //public ActionResult EmailSendToCutomer()
        //{
        //    bool result=false;
        //    result = PlaceOrder(null);
        //    return Json(result,JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Complete(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }

        public int GetOrdersCount()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from o in db.PlaceOrders select o).Count();
            }
        }

        public ActionResult Empty()
        {
            return View();
        }

        public int GetMobileStock()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Mobiles select c).Count();
            }
        }
    }
}
