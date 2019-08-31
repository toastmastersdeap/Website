using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toast.Models;

namespace Toast.Controllers
{
    public class HomeController : Controller
    {
        private DBStoredProcedure dbPROC = new DBStoredProcedure();

        public ActionResult Index()
        {
            //if (Request.IsAuthenticated)
            //{
            //    // TODO:
            //    //var memberIsAdmin = dbPROC.GetMemberAccountType();

            //    //if (memberIsAdmin)
            //    //{
            //    //    return RedirectToAction("Index", "Admin");
            //    //}

            //    // Go to the Member Profile
            //    return RedirectToAction("Index", "Profile");
            //}

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult OurWebsite()
        {
            ViewBag.Message = "Your website descritpion page.";

            return View(); 
        }

        public ActionResult LearnData()
        {
            ViewBag.Message = "Learn you data page.";

            return View(); 
        }

        public ActionResult UniqueDashboard()
        {
            ViewBag.Message = "Your Unique Dashboard description page.";

            return View();
        }
    }
}