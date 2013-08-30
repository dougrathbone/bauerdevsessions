using System.Web;
using System.Web.Optimization;

namespace CompressionDemo
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerytable").Include(
                        "~/Scripts/jquery.dataTables.js",
                        "~/Scripts/jquery.jeditable.js"));

            bundles.Add(new ScriptBundle("~/bundles/fuelux").Include(
                        "~/Scripts/require.js",
                        "~/Scripts/loader.js",
                        "~/Scripts/util.js",
                        "~/Scripts/wizard.js"));

            bundles.Add(new ScriptBundle("~/bundles/demo").Include(
                        "~/Scripts/compression.demo.js",
                        "~/Scripts/kinetic-v{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css"));

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

            bundles.Add(new ScriptBundle("~/Content/jquerytable").Include(
                        "~/Content/DataTables-1.9.4/media/css/jquery.dataTables.css"));

            bundles.Add(new ScriptBundle("~/bundles/impress").Include(
                        "~/Scripts/impress.js"));

            bundles.Add(new StyleBundle("~/Content/impress").Include(
                "~/Content/impress.css"));

        }
    }
}