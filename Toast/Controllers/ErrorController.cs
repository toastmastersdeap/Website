using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Toast.Controllers
{
    public class ErrorController : Controller
    {
        
        // GET: Error
        public ActionResult Error(int statusCode)
        {
            // Return to the view accordingly unhandled exception to the Database

            return statusCode == 404 ? View("~/Views/Shared/Error.cshtml") : View();
        }
    }
}