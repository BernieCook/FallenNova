using System.Web.Mvc;

namespace FallenNova.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // TODO: Enable HTTPS when ready.
            // filters.Add(new RequireHttpsAttribute());

            filters.Add(new HandleErrorAttribute());
        }
    }
}