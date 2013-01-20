using FallenNova.Web.Constants;
using System.Web.Mvc;

namespace FallenNova.Web.Areas.Secure.Controllers
{
    [AuthenticateAndAuthorizeAttribute(Roles = Roles.Member)]
    public class SearchController : BaseController
    {
        #region Index

        //
        // POST: /secure/search

        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
