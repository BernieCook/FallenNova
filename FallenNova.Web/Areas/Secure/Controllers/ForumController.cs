using System.Web.Mvc;

using FallenNova.Web.Constants;

namespace FallenNova.Web.Areas.Secure.Controllers
{
    [AuthenticateAndAuthorizeAttribute(Roles = Roles.Member)]
    public class ForumController : BaseController
    {
        //
        // GET: /secure/forum/

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
