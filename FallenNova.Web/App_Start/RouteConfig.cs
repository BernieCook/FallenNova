using System.Web.Mvc;
using System.Web.Routing;

namespace FallenNova.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // TODO: Currently a known MVC bug.
            routes.LowercaseUrls = true;

            // Avoid engines/crawlers/bots attempting to index this application.
            routes.IgnoreRoute("robots.txt");
            routes.IgnoreRoute("sitemap");
            routes.IgnoreRoute("sitemap.gz");
            routes.IgnoreRoute("sitemap.xml");
            routes.IgnoreRoute("sitemap.xml.gz");
            routes.IgnoreRoute("google_sitemap.xml");
            routes.IgnoreRoute("google_sitemap.xml.gz");

            // Required to support the call to Glimpse's "glimpse.axd".
            routes.IgnoreRoute("glimpse.axd/{*pathInfo}");

            // Register the areas manually to avoid the incorrect ordering of area routing.
            var secureAreaRegistration = new Areas.Secure.SecureAreaRegistration();
            var secureAreaRegistrationContext = new AreaRegistrationContext(secureAreaRegistration.AreaName, RouteTable.Routes);
            secureAreaRegistration.RegisterArea(secureAreaRegistrationContext);

            var publicAreaRegistration = new Areas.Public.PublicAreaRegistration();
            var publicAreaRegistrationContext = new AreaRegistrationContext(publicAreaRegistration.AreaName, RouteTable.Routes);
            publicAreaRegistration.RegisterArea(publicAreaRegistrationContext);
        }
    }
}