using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Toast.Controllers
{
    //[Authorize] // ******** Commented it out only for debugging ******** WILL BE REMOVE
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index(string memberId)
        {
            ViewBag.Title = "Toasty";

            return View();
        }
    }
}