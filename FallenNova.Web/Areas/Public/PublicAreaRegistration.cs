using FallenNova.Web.Constants;
using LowercaseRoutesMVC;
using System.Web.Mvc;

namespace FallenNova.Web.Areas.Public
{
    public class PublicAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Public"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            // URL: /

            context.MapRouteLowercase(
                "Public.Empty",
                string.Empty,
                new { controller = "Home", action = "Index" },
                new[] { ControllerNamespaces.Public }
            );

            // Removes the controller from the URL.
            // URL: /aboutus/

            context.MapRouteLowercase(  
                "Public.Action",
                "{action}",
                new { controller = "Home" },
                new[] { ControllerNamespaces.Public }
            );
        }
    }
}
