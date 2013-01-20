using System.Web.Optimization;

namespace FallenNova.Web
{
    public class BundleConfig
    {
        // Other bundling rules for the .Include() call.
        // Example: "~/Scripts/jquery-{version}.js"
        // Example: "~/Scripts/jquery-*"
        // Example: "~/Scripts/jquery-1.8.0.min.js"

        // Don't hang onto .min files. The minification logic takes care of this and reduces file management.

        public static void RegisterBundles(BundleCollection bundleCollection)
        {
            // NOTE: The Html.css file must come before the bootstrap-responsive.css otherwise it will interfere with the resizing.
            bundleCollection.Add(
                new StyleBundle("~/assets/css")
                .Include("~/Assets/Bootstrap/Css/bootstrap.css")
                .Include("~/Assets/Styles/html.css")
                .Include("~/Assets/Styles/icons.css")
                .Include("~/Assets/Bootstrap/Css/bootstrap-responsive.css"));

            /* 
               NOTE: Modernizr
               1. Modernizr includes Google's html5shiv
               2. Modernizr only addresses Internet Explorer as the other browsers don't need it
               3. Modernizr should be included after the stylesheet references and inside the <head> tag - http://modernizr.com/docs/#installing
               4. Modernizr does its "magic" by creating an element, setting a specific style instruction on that element and then immediately retrieving it. Browsers that understand the instruction will return something sensible; browsers that don’t understand it will return nothing or “undefined”.
               5. Modernizr supports IE6+, Firefox 3.5+, Opera 9.6+, Safari 2+, Chrome. On mobile, we support iOS's mobile Safari, Android's WebKit browser, Opera Mobile, Firefox Mobile and whilst we’re still doing more testing we believe we support Blackberry 6+.
               6. How to implement: http://www.alistapart.com/articles/taking-advantage-of-html5-and-css3-with-modernizr/
               7. When testing use the browser developer tools to inspect the resulting HTML
            */

            bundleCollection.Add(
                new ScriptBundle("~/assets/modernizr")
                    .Include("~/Assets/Scripts/modernizr-2.6.2.custom.js"));

            // TODO: Review the AJAX v1.9.0 AJAX issues. Seems ok in v1.8.3
            bundleCollection.Add(
                new ScriptBundle("~/assets/jquery")
                    .Include("~/Assets/Scripts/jquery-1.8.3.js"));

            // TODO: Replace with specific bootstrap library calls eventually.
            bundleCollection.Add(
                new ScriptBundle("~/assets/public")
                .Include("~/Assets/Bootstrap/Js/bootstrap-alert.js")
                .Include("~/Assets/Bootstrap/Js/bootstrap-button.js"));

            bundleCollection.Add(
                new ScriptBundle("~/assets/secure")
                .Include("~/Assets/Scripts/secure.js")
                .Include("~/Assets/Scripts/bookmark.js")
                //.Include("~/Assets/Bootstrap/Js/bootstrap-transition.js")
                //.Include("~/Assets/Bootstrap/Js/bootstrap-alert.js")
                //.Include("~/Assets/Bootstrap/Js/bootstrap-modal.js")
                .Include("~/Assets/Bootstrap/Js/bootstrap-dropdown.js")
                //.Include("~/Assets/Bootstrap/Js/bootstrap-scrollspy.js")
                //.Include("~/Assets/Bootstrap/Js/bootstrap-tab.js")
                .Include("~/Assets/Bootstrap/Js/bootstrap-tooltip.js")
                //.Include("~/Assets/Bootstrap/Js/bootstrap-popover.js")
                .Include("~/Assets/Bootstrap/Js/bootstrap-button.js")
                .Include("~/Assets/Bootstrap/Js/bootstrap-collapse.js"));
                //.Include("~/Assets/Bootstrap/Js/bootstrap-carousel.js")
                //.Include("~/Assets/Bootstrap/Js/bootstrap-typeahead.js"));

            // NOTE: Bundling and minification won't take place unless we've packaged (or published) the solution and the Web.Release.config <compilation> debug 
            // settings are applied. So we use a preprocessor directive to simulate this override during development.
#if RELEASE
            BundleTable.EnableOptimizations = true; 
#endif
        }
    }
}