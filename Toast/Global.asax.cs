using Microsoft.AspNet.Identity;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Toast.Controllers;
using Toast.Models;
using Toast.Utilities;

namespace Toast
{
    public class MvcApplication : System.Web.HttpApplication
    {
        // Local variables
        private readonly DBStoredProcedure _dbSProc = new DBStoredProcedure();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            if (exception == null)
            {
                return;
            }

            var wrapper = new HttpRequestWrapper(Request);
            var userIP = GeoLocation.GetUserIP(wrapper).Split(':').First();
            var userCountry = GeoLocation.GetCountryFromIP(userIP);
            var userCity = GeoLocation.GetCityFromIP(userIP);
            var userJavascript = Request.Browser.Capabilities.Contains("javascriptversion") ? Request.Browser.Capabilities["javascriptversion"].ToString() : "Not Found";
            var userMobile = Request.Browser.Capabilities.Contains("isMobileDevice") && (Request.Browser.Capabilities["isMobileDevice"].ToString() != "false");
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : "Not Available";
            var lineNumber = new StackTrace(exception, true).GetFrame(0).GetFileLineNumber().ToString();

            try
            {
                Server.ClearError();

                var routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                routeData.Values.Add("action", "Error");

                routeData.Values.Add("statusCode",
                   exception.GetType() == typeof(HttpException) ? ((HttpException)exception).GetHttpCode() : 500);

                // Send email with the exception
                var emailService = new EmailService();
                var response = emailService.SendWebsiteError(
                   "support@speakeranalytics.com",
                   userId,
                   routeData.Values["statuscode"].ToString(),
                   exception.Message,
                   exception.StackTrace);

                // Log it to the database
                _dbSProc.InsertExceptionLog(exception.Message, exception.StackTrace, lineNumber, userId, userIP, userCountry, userCity, Request.Browser.Type, userJavascript, userMobile);

                Response.TrySkipIisCustomErrors = true;
                IController controller = new ErrorController();
                controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                Response.End();
            }
            catch (Exception exc)
            {
                // An error occured when handling the incoming error
                _dbSProc.InsertExceptionLog(exc.Message, exc.StackTrace, lineNumber, userId);
            }
        }
    }
}
