﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Toast.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            // example js datadictionary
            //var jsData =
            //{
            //    FirstName = "Javier",
            //    LastName = "Espinosa",
            //};
            return View();
        }
    }
}