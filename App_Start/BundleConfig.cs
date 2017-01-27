using System.Web;
using System.Web.Optimization;

namespace iGotTheRuns {
    public class BundleConfig {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725

        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular-route").Include(
            "~/Scripts/angular-route/angular-route.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        //"~/Content/bootstrap-theme.css",
                        "~/Content/bootstrap.min.css",
                        "~/Scripts/toastr/toastr.css",
                        //"~/Scripts/angular-ui-tree/angular-ui-tree.min.css", 
                        "~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
            //			"~/Scripts/bootstrap/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
            "~/Scripts/jquery/jquery-{version}.js",
            "~/Scripts/bootstrap/js/bootstrap.js",
            "~/Scripts/jquery.bootstrap.wizard.js",
            "~/Scripts/angular/angular.js",
            "~/Scripts/angular-plus/ngplus-overlay.js",
            "~/Scripts/angular-ui-tree/angular-ui-tree.js",
            "~/Scripts/angular-animate/angular-animate.js",
            "~/Scripts/angular-route/angular-route.js",
            "~/Scripts/angular-sanitize/angular-sanitize.js",
            "~/Scripts/toastr/toastr.js",
            "~/Scripts/lodash/lodash.js",
            "~/Scripts/angular-bootstrap/ui-bootstrap-{version}.js"
            ).IncludeDirectory("~/Scripts/angularjs-utilities/lib", "*.js", true)
            .IncludeDirectory("~/Scripts/angularjs-utilities/src", "*.js", true)
            );

        }
    }
}