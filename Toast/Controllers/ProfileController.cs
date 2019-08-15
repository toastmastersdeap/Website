using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toast.Models;

namespace Toast.Controllers
{
    //[Authorize] // ******** Commented it out only for debugging ******** WILL BE REMOVE
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index(string memberId)
        {
            ViewBag.Title = "Profile";

            return View();
        }

        // GET: AhTrend
        public ActionResult AhTrend(string memberId)
        {
            ViewBag.Title = "AhTrend";

            return View();
        }

        // GET: Meetings
        public ActionResult Meetings(string memberId)
        {
            ViewBag.Title = "Meetings";

            return View();
        }

        // GET: Summary
        public ActionResult Summary(string memberId)
        {
            ViewBag.Title = "Summary";

            return View();
        }

    }
}