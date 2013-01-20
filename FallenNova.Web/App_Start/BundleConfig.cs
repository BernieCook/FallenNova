using System.Web.Optimization;

namespace FallenNova.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundleCollection)
        {
            // NOTE: Don't hang onto .min files. The minification logic takes care of this and reduces file management.
            // NOTE: The Html.css file must come before the bootstrap-responsive.css otherwise it will interfere with the resizing.
            bundleCollection.Add(
                new StyleBundle("~/assets/css")
                .Include("~/Assets/Bootstrap/Css/bootstrap.css")
                .Include("~/Assets/Styles/html.css")
                .Include("~/Assets/Styles/icons.css")
                .Include("~/Assets/Bootstrap/Css/bootstrap-responsive.css"));

            bundleCollection.Add(
                new ScriptBundle("~/assets/modernizr")
                    .Include("~/Assets/Scripts/modernizr-2.6.2.custom.js"));

            bundleCollection.Add(
                new ScriptBundle("~/assets/jquery")
                    .Include("~/Assets/Scripts/jquery-1.8.3.js"));

            bundleCollection.Add(
                new ScriptBundle("~/assets/public")
                .Include("~/Assets/Bootstrap/Js/bootstrap-alert.js")
                .Include("~/Assets/Bootstrap/Js/bootstrap-button.js"));

            bundleCollection.Add(
                new ScriptBundle("~/assets/secure")
                .Include("~/Assets/Scripts/secure.js")
                .Include("~/Assets/Scripts/bookmark.js")
                .Include("~/Assets/Bootstrap/Js/bootstrap-dropdown.js")
                .Include("~/Assets/Bootstrap/Js/bootstrap-tooltip.js")
                .Include("~/Assets/Bootstrap/Js/bootstrap-button.js")
                .Include("~/Assets/Bootstrap/Js/bootstrap-collapse.js"));

            // NOTE: Bundling and minification won't take place unless we've packaged (or published) the solution and the Web.Release.config <compilation> debug 
            // settings are applied. So we use a preprocessor directive to simulate this override during development.
#if RELEASE
            BundleTable.EnableOptimizations = true; 
#endif
        }
    }
}