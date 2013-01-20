using AutoMapper;
using FallenNova.Service;
using FallenNova.Web.Areas.Secure.Models.HomeModel;
using FallenNova.Web.Areas.Secure.Models.UsersModel;
using FallenNova.Web.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FallenNova.Web.Areas.Secure.Controllers
{
    [AuthenticateAndAuthorizeAttribute(Roles = Roles.Member)]
    public class HomeController : BaseController
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        #region Index

        //
        // GET: /secure

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var latestUsers = _userService.GetLatestUsersAsync(5);
            var latestShips = _userService.GetLatestUsersAsync(10);
            var latestItems = _userService.GetLatestUsersAsync(15);

            await Task.WhenAll(
                latestUsers,
                latestShips,
                latestItems);

            Mapper.CreateMap<UserDetailsDto, UsersModel>();
            var latestUsersModels = Mapper.Map<IEnumerable<UserDetailsDto>, IEnumerable<UsersModel>>(latestUsers.Result);

            Mapper.CreateMap<UserDetailsDto, UsersModel>();
            var latestShipsModels = Mapper.Map<IEnumerable<UserDetailsDto>, IEnumerable<UsersModel>>(latestShips.Result);

            Mapper.CreateMap<UserDetailsDto, UsersModel>();
            var latestItemsModels = Mapper.Map<IEnumerable<UserDetailsDto>, IEnumerable<UsersModel>>(latestItems.Result);

            // TODO: Get this querying most popular ships and other useful information.
            var homeModel = new HomeModel
            {
                LatestUsers = latestUsersModels,
                LatestShips = latestShipsModels,
                LatestItems = latestItemsModels
            };

            return View(homeModel);
        }

        #endregion
    }
}
