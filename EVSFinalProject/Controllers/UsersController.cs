using EVSFinalProject.Models;
using EVSFinalProjectClasses;
using EVSFinalProjectClasses.UserManagment;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace EVSFinalProject.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        [HttpGet]
        //Second Step
        public ActionResult Login()
        {
            HttpCookie hcs = Request.Cookies["logsec"];
            if (hcs != null)
            {
                User u = new UserHandler().GetUser(hcs.Values["logid"], hcs.Values["psd"]);
                if (u != null)

                {
                    hcs.Expires = DateTime.Today.AddDays(7);
                    Session.Add(WebUtils.Current_User, u);
                    if (u.IsInRole(WebUtils.Admin))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            //here we get the the Countries Because we Render The signup at Login Page
            ViewBag.Countries = ModelHelper.ToSelectItemList(new LocationHandler().Getcountry());
            ViewBag.GenderList = ModelHelper.ToSelectItemList(new UserHandler().GetGender());

            ViewBag.Controller = Request.QueryString["ctl"];
            ViewBag.Action = Request.QueryString["act"];

            return View();
        }

        //first Step
        [HttpPost]
        public ActionResult Login(LoginViewModel data)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            User u = new UserHandler().GetUser(data.Loginid, data.Password);

            if (u != null)
            {
                if (data.RememberMe)
                {
                    HttpCookie hc = new HttpCookie("logsec");
                    hc.Expires = DateTime.Today.AddDays(7);
                    //name of file Where we Want to Store the Name Of Email and Login
                    hc.Values.Add("logid", u.LoginId);
                    hc.Values.Add("psd", u.Password);
                    Response.SetCookie(hc);

                }
                Session.Add(WebUtils.Current_User, u);
                string ctl = Request.QueryString["c"];
                string act = Request.QueryString["a"];
                if (u.IsInRole(WebUtils.Admin))
                {

                    return RedirectToAction("index", "Admin");
                }

                if (!string.IsNullOrEmpty(ctl) && string.IsNullOrEmpty(act))
                {
                    return RedirectToAction(act, ctl);
                }

                return RedirectToAction("index", "Home");
            }
            else
            {

                ViewBag.ErrorMessage = "Your Login Id and Password is Incorrect";
            }
            ViewBag.Countries = ModelHelper.ToSelectItemList(new LocationHandler().Getcountry());
            ViewBag.GenderList = ModelHelper.ToSelectItemList(new UserHandler().GetGender());
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            HttpCookie hc = Request.Cookies["logsec"];
            if (hc != null)
            {
                hc.Expires = DateTime.Now;
                Response.SetCookie(hc);
            }

            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            ViewBag.Countries = ModelHelper.ToSelectItemList(new LocationHandler().Getcountry());
            ViewBag.GenderList = ModelHelper.ToSelectItemList(new UserHandler().GetGender());
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection formdata)
        {
            //if (User != null && User.IsInRole(WebUtils.Current_User))) return RedirectToAction("Login", "Users", new { ctl = "Products", act = "Create" });\
            User u = new User();
            if (u != null)
            {
                try
                {
                    u.FullName = Convert.ToString(formdata["FullName"]);
                    u.Email = Convert.ToString(formdata["Email"]);
                    u.LoginId = Convert.ToString(formdata["LoginId"]);
                    u.Phone = Convert.ToInt64(formdata["Phone"]);
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
                            string url = "~/Content/Images/" + abc;
                            string path = Request.MapPath(url);
                            u.ImageUrl = abc;
                            file.SaveAs(path);
                        }
                    }
                    u.Gender = new Gender { Id = Convert.ToInt32(formdata["gender.Name"]) };//this thing
                    u.BirthDate = Convert.ToDateTime(formdata["BirthDate"]);
                    u.Password = Convert.ToString(formdata["Password"]);
                    u.ConfirmPassword = Convert.ToString(formdata["ConfirmPassword"]);
                    u.SecurityQuestion = Convert.ToString(formdata["SecurityQuestion"]);
                    u.SecurityAnswer = Convert.ToString(formdata["SecurityAnswer"]);
                    u.Role = new Role() { Id = 2 };
                    u.Country = formdata["Country"];
                    u.Country = new LocationHandler().Getcountry().Where(x => x.Id == Convert.ToInt32(u.Country)).FirstOrDefault().Name;

                    u.City = new City() { Id = Convert.ToInt32(formdata["City"]) };

                    User user = new UserHandler().AddUser(u);

                    return View("~/Views/Users/Message.cshtml", user);
                }

                catch (Exception exception)
                {
                    Console.Write(exception);
                    return View();
                }

            }
            else
            {
                return View();
            }
        }

        public ActionResult Message()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Dropdownlist(int id)
        {
            DDLLIstModel l = new DDLLIstModel
            {
                Name = "City",
                Caption = "--Select City--",
                Values = ModelHelper.ToSelectItemList(new LocationHandler().GetCities(new Country { Id = id }))
            };

            return PartialView("~/Views/Shared/_DDLListView.cshtml", l);
        }

        [HttpGet]
        public ActionResult PasswordRecovery()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PasswordRecovery(Email data)
        {
            try
            {
                dbcontext db = new dbcontext();
                if (ModelState.IsValid)

                {
                    User user = new UserHandler().GetUserByEmail(data.email);
                    var sub = user.FullName + " Password Recovered";
                    string c = Path.GetRandomFileName().Replace(".", "");
                    user.Password = Convert.ToString(c);
                    user.ConfirmPassword = Convert.ToString(c);
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(data.email));
                    message.Subject = "-No-Reply- Password Recovery Email OnLineMobile-SHOP";
                    message.Body = $"Dear:{sub} Please Login next time with This Given Password:" + c;
                    message.IsBodyHtml = false;
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Success = "Email has been Sent To You Successfully!";
                    }
                    return View();
                }
            }
            catch (Exception)
            {

                ViewBag.Error = "Error Sending Email.  Try Again Later.Ooop's";
            }

            return View();
        }
        [HttpGet]
        public ActionResult UserSetting(int id)
        {
            User u = new UserHandler().GetUserById(id);
            ViewBag.Countries = ModelHelper.ToSelectItemList(new LocationHandler().Getcountry());
            return View(new UserHandler().GetUserById(id));
        }
        [HttpPost]
        public ActionResult UserSetting(User u)
        {
            dbcontext db = new dbcontext();

            using (db)
            {

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
                        string url = "~/Content/Images/" + abc;
                        string path = Request.MapPath(url);
                        u.ImageUrl = abc;
                        file.SaveAs(path);
                    }
                }
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("UserSetting", "Users");
        }
    }
}

