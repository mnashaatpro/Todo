using System.Web.Optimization;

namespace Healty
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                     "~/scripts/app/services/attendanceService.js",
                     "~/scripts/app/services/followingService.js",
                     "~/scripts/app/controllers/todoController.js",
                     "~/scripts/app/controllers/gigDetailsController.js",
                     "~/scripts/app/app.js"
             ));
            
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/lib/jquery/dist/jquery.min.js",

                        "~/Scripts/underscore-min.js",
                        "~/Scripts/moment.js",
                        "~/lib/bootstrap/dist/js/bootstrap.bundle.min.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/sweetalert/sweetalert.min.js",
                        "~/Scripts/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                        //DataTable 
                        "~/Scripts/datatables/js/jquery.dataTables.min.js",
                        "~/Scripts/datatables/js/dataTables.bootstrap4.min.js",

                        "~/Scripts/bootbox.min.js"));

           
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/lib/bootstrap/dist/css/bootstrap.min.css",
                      "~/Scripts/bootstrap-datepicker/css/bootstrap-datepicker.min.css",
                      "~/Scripts/bootstrap-icons/font/bootstrap-icons.min.css",
                      "~/Scripts/animate.css/animate.min.css",
                      "~/Scripts/datatables/css/dataTables.bootstrap4.min.css",
                      "~/Content/site.css",
                      "~/Content/animate.css"));


        }
    }
}
