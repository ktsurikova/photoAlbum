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

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                "~/Scripts/angular.js").Include("~/Scripts/app.js")
                .Include("~/Scripts/photo-list.component.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site.css")
                .Include("~/Content/bootstrap.css").Include("~/Content/jquery.fancybox.css")
                .Include("~/Content/ionicons.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/jquery-ui.css"));
        }
    }
}