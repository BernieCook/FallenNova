using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

using AutoMapper;
using Elmah;
using FallenNova.Service;
using FallenNova.Web.Areas.Public.Models;
using FallenNova.Web.Constants;
using FallenNova.Web.Controllers;

namespace FallenNova.Web.Areas.Public.Controllers
{
    [Authenticate]
    public class HomeController : BaseController
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IContactUsService _contactUsService;
        private readonly IUserLogService _userLogService;

        public HomeController(
            IAuthenticateService authenticateService,
            IContactUsService contactUsService,
            IUserLogService userLogService)
        {
            _authenticateService = authenticateService;
            _contactUsService = contactUsService;
            _userLogService = userLogService;
        }

        #region Index

        //
        // GET: /

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region About Us

        //
        // GET: /aboutus

        [AllowAnonymous]
        [HttpGet]
        public ActionResult AboutUs()
        {
            return View();
        }

        #endregion

        #region Error

        //
        // GET: /error

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        #endregion

        #region Join

        //
        // GET: /join

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Join()
        {
            return View();
        }

        #endregion

        #region Contact Us

        //
        // GET: /contactus

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ContactUs()
        {
            if (CurrentUser != null)
            {
                var contactUsModel = new ContactUsModel
                {
                    Name = CurrentUser.FullName,
                    EmailAddress = CurrentUser.EmailAddress
                };

                return View(contactUsModel);
            }

            return View();
        }

        //
        // POST: /contactus

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ContactUs(ContactUsModel contactUsModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<ContactUsModel, ContactUsDetailsDto>();
                var contactUsDetailsDto = Mapper.Map<ContactUsModel, ContactUsDetailsDto>(contactUsModel);

                IList<string> errorMessages = new List<string>();

                if (!_contactUsService.Insert(
                    contactUsDetailsDto,
                    (CurrentUser != null) ? CurrentUser.UserId : (int?)null,
                    ref errorMessages))
                {
                    ModelState.AddModelErrors(errorMessages);
                }
                else
                {
                    return RedirectToAction("ContactUsSuccessful");
                }
            }

            // Reaching this point means something went wrong.
            return View(contactUsModel);
        }

        //
        // GET: /contactussuccessful

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ContactUsSuccessful()
        {
            return View();
        }

        #endregion

        #region Log In/Log Off

        //
        // GET: /login

        [AllowAnonymous]
        [HttpGet]
        // TODO: Get HTTPS working on these action methods. 
        // [RequireHttps]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /login

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        // TODO: Get HTTPS working on these action methods.
        // [RequireHttps]
        public ActionResult Login(
            LoginModel loginModel,
            string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authenticateService.ValidateUser(loginModel.EmailAddress, loginModel.Password))
                {
                    Login(HttpContext, loginModel.RememberMe);

                    _userLogService.LoginSuccessful(UserId);

                    // Handle open redirection attacks by checking if the return URL is local.
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        // Log the exception in ELMAH and redirect the user to an error page.
                        ErrorSignal.FromCurrentContext().Raise(
                            new System.Security.SecurityException(string.Format("An open redirect attack to \"{0}\" was detected.", returnUrl)));

                        return RedirectToAction("Error", "Home");
                    }

                    return RedirectToAction("Index", "Home", new { Area = "Secure" });
                }

                _userLogService.LoginUnsuccessful(loginModel.EmailAddress);

                ModelState.AddModelError("The email address and/or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form.
            return View(loginModel);
        }

        //
        // GET: /logoff

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOff()
        {
            if (User.Identity.IsAuthenticated)
            {
                _userLogService.LoggedOut(Int32.Parse(User.Identity.Name));

                FormsAuthentication.SignOut();

                // Redirect once the cookie is removed to update the authentication status.
                return RedirectToAction("LogOff");
            }

            HttpContext.Cache.Remove(string.Concat(CacheKeyNames.Identity, HttpContext.User.Identity.Name));

            Session.Abandon();

            return View();
        }

        #endregion
    }
}
