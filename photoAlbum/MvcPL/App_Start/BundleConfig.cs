using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MvcPL.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js").Include(
                "~/Scripts/jquery-ui-{version}.js").Include("~/Scripts/jquery.fancybox.js")
                .Include("~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryOnly").Include(
                    "~/Scripts/jquery-{version}.js").Include(
                    "~/Scripts/jquery-ui-{version}.js")
                .Include("~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/search").Include("~/Scripts/search.js"));

            bundles.Add(new ScriptBundle("~/bundles/popover").Include(
                "~/Scripts/jquery-{version}.js").Include(
                "~/Scripts/jquery-ui-{version}.js").Include("~/Scripts/popover.js")
                .Include("~/Scripts/photoDetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/uploadImage").Include("~/Scripts/uploadImage.js"));

            bundles.Add(new ScriptBundle("~/bundles/profile").Include("~/Scripts/profilePhoto.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //    "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //    "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site.css")
                .Include("~/Content/bootstrap.css").Include("~/Content/jquery.fancybox.css")
                .Include("~/Content/ionicons.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/jquery-ui.css"
                //"~/Content/themes/base/jquery.ui.core.css",
                //"~/Content/themes/base/jquery.ui.resizable.css",
                //"~/Content/themes/base/jquery.ui.selectable.css",
                //"~/Content/themes/base/jquery.ui.accordion.css",
                //"~/Content/themes/base/jquery.ui.autocomplete.css",
                //"~/Content/themes/base/jquery.ui.button.css",
                //"~/Content/themes/base/jquery.ui.dialog.css",
                //"~/Content/themes/base/jquery.ui.slider.css",
                //"~/Content/themes/base/jquery.ui.tabs.css",
                //"~/Content/themes/base/jquery.ui.datepicker.css",
                //"~/Content/themes/base/jquery.ui.progressbar.css",
                //"~/Content/themes/base/jquery.ui.theme.css")
                ));
        }
    }
}