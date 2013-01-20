using FallenNova.Service;
using FallenNova.Web.Constants;
using MvcAuthorizationContext = System.Web.Mvc.AuthorizationContext;
using Ninject;
using System;
using System.Linq;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace FallenNova.Web
{
    // TODO: The BaseController and it's attributes need some work.
    public class BaseController : Controller, IBaseController
    {
        public CustomClaimsIdentity CurrentUser
        {
            get { return ((ClaimsPrincipal)HttpContext.User).Identity as CustomClaimsIdentity; }
        }

        public static int UserId
        {
            get
            {
                var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

                return Int32.Parse(claimsPrincipal.Identity.Name);
            }
        }

        public static void Login(
            HttpContextBase httpContext,
            bool rememberMe)
        {
            var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

            // Encrypts the claims principal using a 128-bit key before storing it into cache.
            var sessionSecurityToken = new SessionSecurityToken(claimsPrincipal);

            httpContext.Cache[string.Concat(CacheKeyNames.Identity, UserId)] = sessionSecurityToken;

            FormsAuthentication.SetAuthCookie(UserId.ToString(Format.English), rememberMe);
        }
  
        [AttributeUsage(AttributeTargets.Class)]
        public sealed class AuthenticateAndAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
        {
            [Inject]
            public IAuthenticateService AuthenticateService { get; set; }

            public string Roles { get; set; }

            public void OnAuthorization(MvcAuthorizationContext context)
            {
                var isAuthorised = false;

                if (AuthenticateUser(context, AuthenticateService))
                {
                    if (Roles.Split(Separators.Comma).ToList().Any(role => context.HttpContext.User.IsInRole(role.Trim())))
                    {
                        isAuthorised = true;
                    }
                }

                if (!isAuthorised)
                {
                    // TODO: Change this to throw a 401 (unauthorised), and have the application handle the 401 gracefully.
                    // context.Result = new HttpUnauthorizedResult();

                    // Don't be concerned with a 401 (authorised), go for a 302 (found) but redirect for simplicity.
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "area", "Public" },
                            { "controller", "Home" },
                            { "action", "Login" },
                            { "ReturnUrl", context.HttpContext.Request.RawUrl }
                        });
                }
            }
        }

        [AttributeUsage(AttributeTargets.Class)]
        public sealed class AuthenticateAttribute : ActionFilterAttribute
        {
            [Inject]
            public IAuthenticateService AuthenticateService { get; set; }

            public override void OnActionExecuting(ActionExecutingContext context)
            {
                AuthenticateUser(context, AuthenticateService);
            }
        }

        private static bool AuthenticateUser(ControllerContext context, IAuthenticateService authenticateService)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = context.HttpContext.User.Identity.Name;

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    var sessionSecurityToken = context.HttpContext.Cache[string.Concat(CacheKeyNames.Identity, userId)] as SessionSecurityToken;

                    if (sessionSecurityToken != null)
                    {
                        // If the user's claims principal was located in cache retrieve it from there.
                        context.HttpContext.User = sessionSecurityToken.ClaimsPrincipal;

                        return true;
                    }

                    if (authenticateService.ValidateUser(Int32.Parse(userId)))
                    {
                        // If the user's claims principal was not located in cache set it and repeat the login steps.
                        Login(context.HttpContext, true);

                        return true;
                    }
                }
            }

            return false;
        }
    }
}