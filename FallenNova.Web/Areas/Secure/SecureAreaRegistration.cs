using System.Web.Mvc;

using LowercaseRoutesMVC;

using FallenNova.Web.Constants;

namespace FallenNova.Web.Areas.Secure
{
    public class SecureAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Secure"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            // URL: /secure/
            // URL: /secure/corp/details/117

            context.MapRouteLowercase(
                RouteNames.Secure.Default,
                RouteUrls.Secure.Default,
                new 
                { 
                    controller = "Home", 
                    action = "Index", 
                    id = UrlParameter.Optional 
                },
                new[] { ControllerNamespaces.Secure }
            );

            // URL: /secure/corp/1/10/member/true
            // URL: /secure/corp/1/10/member/false
            // URL: /secure/corp/2/50/corporation/false
            // URL: /secure/itemdatabase/1/10/name/true?keywords=vexor >> Html form submissions [GET] aren't aware of MVC routes so will generate a querystring by default. Ambient parameters make no difference.

            context.MapRouteLowercase(
                RouteNames.Secure.Data,
                RouteUrls.Secure.Data,
                new 
                { 
                    controller = "Home", 
                    action = "Index", 
                    pageIndex = UrlParameter.Optional, 
                    pageSize = UrlParameter.Optional,
                    sortBy = UrlParameter.Optional,
                    sortAscending = UrlParameter.Optional
                },
                new[] { ControllerNamespaces.Secure }
            );
        }
    }
}
