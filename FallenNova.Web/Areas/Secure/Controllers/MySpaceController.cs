using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using FallenNova.Service;
using FallenNova.Web.Areas.Secure.Models.MySpaceModel;
using FallenNova.Web.Constants;
using FallenNova.Web.Controllers;

namespace FallenNova.Web.Areas.Secure.Controllers
{
    [AuthenticateAndAuthorizeAttribute(Roles = Roles.Member)]
    public class MySpaceController : BaseController
    {
        private readonly IUserService _userService;

        public MySpaceController(IUserService userService)
        {
            _userService = userService;
        }

        //
        // GET: /secure/myspace

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #region Edit Details

        //
        // GET: /secure/myspace/editdetails

        [HttpGet]
        public ActionResult EditDetails()
        {
            var userContactDetailsDto = _userService.GetContactDetails(CurrentUser.UserId);

            Mapper.CreateMap<UserContactDetailsDto, EditDetailsModel>();
            var myDetailsModel = Mapper.Map<UserContactDetailsDto, EditDetailsModel>(userContactDetailsDto);

            return View(myDetailsModel);
        }

        //
        // POST: /secure/myspace/editdetails

        [HttpPost]
        public ActionResult EditDetails(EditDetailsModel editDetailsModel)
        {
            editDetailsModel.UserId = CurrentUser.UserId;

            if (ModelState.IsValid)
            {
                Mapper.CreateMap<EditDetailsModel, UserContactDetailsDto>();
                var userContactDetailsDto = Mapper.Map<EditDetailsModel, UserContactDetailsDto>(editDetailsModel);

                IList<string> errorMessages = new List<string>();

                if (!_userService.UpdateContactDetails(
                    userContactDetailsDto,
                    CurrentUser.UserId,
                    ref errorMessages))
                {
                    ModelState.AddModelErrors(errorMessages);
                }
                else
                {
                    return RedirectToAction("EditDetailsSuccessful");
                }
            }

            // Reaching this point means something went wrong.
            return View(editDetailsModel);
        }

        //
        // GET: /secure/myspace/editdetailssuccessful

        [HttpGet]
        public ActionResult EditDetailsSuccessful()
        {
            return View();
        }

        #endregion

        #region Edit Password

        //
        // GET: /secure/myspace/editpassword

        [HttpGet]
        public ActionResult EditPassword()
        {
            return View();
        }

        //
        // POST: /secure/myspace/editpassword

        [HttpPost]
        public ActionResult EditPassword(EditPasswordModel editPasswordModel)
        {
            editPasswordModel.UserId = CurrentUser.UserId;

            if (ModelState.IsValid)
            {
                Mapper.CreateMap<EditPasswordModel, UserPasswordDto>();
                var userPasswordDto = Mapper.Map<EditPasswordModel, UserPasswordDto>(editPasswordModel);

                IList<string> errorMessages = new List<string>();

                if (!_userService.UpdatePassword(
                    userPasswordDto,
                    CurrentUser.UserId,
                    ref errorMessages))
                {
                    ModelState.AddModelErrors(errorMessages);
                }
                else
                {
                    return RedirectToAction("EditPasswordSuccessful");
                }
            }

            // Reaching this point means something went wrong.
            return View(editPasswordModel);
        }

        //
        // GET: /secure/myspace/editpasswordsuccessful

        [HttpGet]
        public ActionResult EditPasswordSuccessful()
        {
            return View();
        }

        #endregion
    }
}
