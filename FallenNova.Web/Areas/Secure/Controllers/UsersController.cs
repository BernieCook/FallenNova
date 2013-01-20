using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using FallenNova.Service;
using FallenNova.Web.Areas.Shared.Models;
using FallenNova.Web.Areas.Secure.Models.UsersModel;
using FallenNova.Web.Constants;
using FallenNova.Web.Controllers;

namespace FallenNova.Web.Areas.Secure.Controllers
{
    [AuthenticateAndAuthorizeAttribute(Roles = Roles.Administrator)]
    public class UsersController : BaseController
    {
        private const string ConstSortByFirstName = "FirstName";
        private const string ConstSortBySurname = "Surname";

        private const int ConstDefaultRoleId = 1; // 1 = Member

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #region Index

        //
        // GET: /secure/users

        [HttpGet]
        public ActionResult Index(
            int pageIndex = 1, 
            int pageSize = 10,
            string sortBy = ConstSortByFirstName,
            bool sortAscending = true)
        {
            int totalResults;

            var userDetailsDtos =
                _userService.GetUsers(
                pageIndex, 
                pageSize, 
                sortBy,
                sortAscending,
                out totalResults);

            IPagingInfo pagingInfo = new PagingInfo
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalResults = totalResults,
                SortBy = sortBy,
                SortAscending = sortAscending,
                RouteName = RouteNames.Secure.Data,
                RouteUrl = RouteUrls.Secure.Data
            };

            ViewBag.PagingInfo = pagingInfo;
            ViewBag.FirstNameSortAscending = pagingInfo.IsSortAscending(ConstSortByFirstName);
            ViewBag.SurnameSortAscending = pagingInfo.IsSortAscending(ConstSortBySurname);

            Mapper.CreateMap<UserDetailsDto, UsersModel>();
            var usersModels = Mapper.Map<IEnumerable<UserDetailsDto>, IEnumerable<UsersModel>>(userDetailsDtos);
            
            return View(usersModels);
        }

        #endregion

        #region Add

        //
        // GET: /secure/users/add

        [HttpGet]
        public ActionResult Add()
        {
            var addUsersModel = new AddUserModel
            {
                RoleId = ConstDefaultRoleId
            };

            return View(addUsersModel);
        }

        //
        // POST: /secure/users/add

        [HttpPost]
        public ActionResult Add(AddUserModel addUsersModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<AddUserModel, UserContactDetailsDto>();
                var userInsertDetailsDto = Mapper.Map<AddUserModel, UserContactDetailsDto>(addUsersModel);

                IList<string> errorMessages = new List<string>();

                if (!_userService.Insert(
                    userInsertDetailsDto,
                    CurrentUser.UserId,
                    ref errorMessages))
                {
                    ModelState.AddModelErrors(errorMessages);
                }
                else
                {
                    return RedirectToAction("AddSuccessful", new { id = userInsertDetailsDto.UserId });
                }
            }

            // Reaching this point means something went wrong.
            return View(addUsersModel);
        }

        //
        // GET: /secure/users/addsuccessful/117

        [HttpGet]
        public ActionResult AddSuccessful(int id)
        {
            var userDetailsDto = _userService.GetDetails(id);

            Mapper.CreateMap<UserDetailsDto, AddUserSuccessfulModel>();
            var addUserSuccessfulModel = Mapper.Map<UserDetailsDto, AddUserSuccessfulModel>(userDetailsDto);

            return View(addUserSuccessfulModel);
        }

        #endregion

        #region Details

        //
        // GET: /secure/users/details/117

        [HttpGet]
        public ActionResult Details(int id)
        {
            var userDetailsDto = _userService.GetDetails(id, true);

            Mapper.CreateMap<UserDetailsDto, UserDetailsModel>();
            Mapper.CreateMap<RoleDetailsDto, UserDetailsModel.Role>();
            Mapper.CreateMap<CharacterDetailsDto, UserDetailsModel.Character>();
            var userDetailsModel = Mapper.Map<UserDetailsDto, UserDetailsModel>(userDetailsDto);

            return View(userDetailsModel);
        }

        #endregion

        #region Edit

        //
        // GET: /secure/users/editdetails/117

        [HttpGet]
        public ActionResult EditDetails(int id)
        {
            var userContactDetailsDto = _userService.GetContactDetails(id);

            Mapper.CreateMap<UserContactDetailsDto, EditDetailsUserModel>();
            var editDetailsUserModel = Mapper.Map<UserContactDetailsDto, EditDetailsUserModel>(userContactDetailsDto);

            return View(editDetailsUserModel);
        }

        //
        // POST: /secure/users/editdetails/117

        [HttpPost]
        public ActionResult EditDetails(EditDetailsUserModel editDetailsUsersModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<EditDetailsUserModel, UserContactDetailsDto>();
                var userContactDetailsDto = Mapper.Map<EditDetailsUserModel, UserContactDetailsDto>(editDetailsUsersModel);

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
                    return RedirectToAction("EditSuccessful", new { id = userContactDetailsDto.UserId });
                }
            }

            // Reaching this point means something went wrong.
            return View(editDetailsUsersModel);
        }

        //
        // GET: /secure/users/editstatus/117

        [HttpGet]
        public ActionResult EditStatus(int id)
        {
            var userStatusDetailsDto = _userService.GetStatusDetails(id);

            Mapper.CreateMap<UserStatusDetailsDto, EditStatusUserModel>();
            var editStatusUserModel = Mapper.Map<UserStatusDetailsDto, EditStatusUserModel>(userStatusDetailsDto);

            return View(editStatusUserModel);
        }

        //
        // POST: /secure/users/editstatus/117

        [HttpPost]
        public ActionResult EditStatus(EditStatusUserModel editStatusUserModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<EditStatusUserModel, UserStatusDetailsDto>();
                var userStatusDetailsDto = Mapper.Map<EditStatusUserModel, UserStatusDetailsDto>(editStatusUserModel);

                IList<string> errorMessages = new List<string>();

                if (!_userService.UpdateStatus(
                    userStatusDetailsDto,
                    CurrentUser.UserId,
                    ref errorMessages))
                {
                    ModelState.AddModelErrors(errorMessages);
                }
                else
                {
                    return RedirectToAction("EditSuccessful", new { id = userStatusDetailsDto.UserId });
                }
            }

            // Reaching this point means something went wrong.
            return View(editStatusUserModel);
        }

        //
        // GET: /secure/users/editsuccessful/117

        [HttpGet]
        public ActionResult EditSuccessful(int id)
        {
            var userDetailsDto = _userService.GetDetails(id);

            Mapper.CreateMap<UserDetailsDto, EditUserSuccessfulModel>();
            var editUserSuccessfulModel = Mapper.Map<UserDetailsDto, EditUserSuccessfulModel>(userDetailsDto);

            return View(editUserSuccessfulModel);
        }

        #endregion
    }
}
