using System.Web;
using System.Web.Optimization;

namespace Toast
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Toasty style
            bundles.Add(new StyleBundle("~/bundles/toasty/css").Include(
                      "~/Content/style.css", new CssRewriteUrlTransform()));

            // Toasty script
            bundles.Add(new ScriptBundle("~/bundles/toasty/js").Include(
                      "~/Scripts/metisMenu.min.js",
                      "~/Vendor/slimScroll/jquery.slimscroll.min.js",
                      "~/Vendor/iCheck/icheck.min.js",
                      "~/Vendor/peity/jquery.peity.min.js",
                      "~/Vendor/sparkline/index.js",
                      "~/Scripts/toasty.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include(
                        "~/Scripts/jquery-3.3.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Animate.css
            bundles.Add(new StyleBundle("~/bundles/animate/css").Include(
                      "~/Vendor/animate.css/animate.min.css"));

            // Pe-icon-7-stroke
            bundles.Add(new StyleBundle("~/bundles/peicon7stroke/css").Include(
                      "~/Icons/pe-icon-7-stroke/css/pe-icon-7-stroke.css", new CssRewriteUrlTransform()));

            // Font Awesome icons style
            bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include(
                      "~/Vendor/fontawesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            //bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
            //    "~/Content/bootstrap.css"));
            ////"~/Content/site.css"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
            //     "~/Scripts/bootstrap.js"));

            // Bootstrap style
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                      "~/Vendor/bootstrap/dist/css/bootstrap.min.css", new CssRewriteUrlTransform()));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
                      "~/Vendor/bootstrap/dist/js/bootstrap.min.js"));

            // Star rating
            bundles.Add(new ScriptBundle("~/bundles/starRating/js").Include(
                "~/Vendor/bootstrap-star-rating/js/star-rating.min.js"));

            // Star rating style
            bundles.Add(new StyleBundle("~/bundles/starRating/css").Include(
                "~/Vendor/bootstrap-star-rating/css/star-rating.min.css", new CssRewriteUrlTransform()));

            // Sweetalert
            bundles.Add(new ScriptBundle("~/bundles/sweetAlert/js").Include(
                      "~/Vendor/sweetalert/lib/sweet-alert.min.js"));

            // Sweetalert style
            bundles.Add(new StyleBundle("~/bundles/sweetAlert/css").Include(
                      "~/Vendor/sweetalert/lib/sweet-alert.css"));

            // Full Calendar
            bundles.Add(new ScriptBundle("~/bundles/fullCalendar/js").Include(
                      "~/Vendor/fullcalendar/dist/fullcalendar.min.js"));

            // Full Calendar style
            bundles.Add(new StyleBundle("~/bundles/fullCalendar/css").Include(
                      "~/Vendor/fullcalendar/dist/fullcalendar.min.css"));

            // Chart JS
            bundles.Add(new ScriptBundle("~/bundles/chartjs/js").Include(
                      "~/Scripts/Chart.min.js"));

            // Flot chart
            bundles.Add(new ScriptBundle("~/bundles/flot/js").Include(
                      "~/Vendor/flot/jquery.flot.js",
                      "~/Vendor/flot/jquery.flot.tooltip.min.js",
                      "~/Vendor/flot/jquery.flot.resize.js",
                      "~/Vendor/flot/jquery.flot.pie.js",
                      "~/Vendor/flot.curvedlines/curvedLines.js",
                      "~/Vendor/jquery.flot.spline/index.js"));
        }
    }
}
