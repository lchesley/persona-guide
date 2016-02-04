using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFakes.Controllers
{    
    public class HomeController : Controller
    {
        public ActionResult Insert()
        {
            ViewData["firstname"] = Request.Form["firstName"];
            ViewData["lastName"] = Request.Form["lastName"];
            ViewData["count"] = Request.Form.Count;
            return View();
        }


        public ActionResult Secret()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["userName"] = User.Identity.Name;
                return View("Secret");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult Admin()
        {
            if (User.IsInRole("Admin"))
            {
                return View("Secret");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public ViewResult Details()
        {
            ViewData["key1"] = Request.QueryString["key1"];
            ViewData["key2"] = Request.QueryString["key2"];
            ViewData["count"] = Request.QueryString.Count;
            
            return View();
        }


        public ViewResult TestCookie()
        {
            ViewData["key"] = Request.Cookies["key"].Value;
            return View();
        }


        public ViewResult TestSession()
        {
            ViewData["item1"] = Session["item1"];
            Session["item2"] = "cool!";
            return View();
        }

    }
}
