using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using FallenNova.Service;
using FallenNova.Web.Constants;
using FallenNova.Web.Areas.Shared.Models;
using FallenNova.Web.Areas.Secure.Models.CorpModel;

namespace FallenNova.Web.Areas.Secure.Controllers
{
    [AuthenticateAndAuthorizeAttribute(Roles = Roles.Member)]
    public class CorpController : BaseController
    {
        private const string ConstSortByMember = "Member";
        private const string ConstSortByCorporation = "Corporation";

        private readonly ICharacterService _characterService;

        public CorpController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        #region Index

        //
        // get: /secure/corp

        [HttpGet]
        public ActionResult Index(
            int pageIndex = 1, 
            int pageSize = 10,
            string sortBy = ConstSortByMember,
            bool sortAscending = true)
        {
            int totalResults;

            var characterDetailsDtos =
                _characterService.GetCharacters(
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
            ViewBag.MemberSortAscending = pagingInfo.IsSortAscending(ConstSortByMember);
            ViewBag.CorporationSortAscending = pagingInfo.IsSortAscending(ConstSortByCorporation);

            ViewBag.IsAdministrator = CurrentUser.IsAdministrator;

            Mapper.CreateMap<CharacterDetailsDto, MembersModel>();
            var membersModels = Mapper.Map<IEnumerable<CharacterDetailsDto>, IEnumerable<MembersModel>>(characterDetailsDtos);
            
            return View(membersModels);
        }

        #endregion

        #region Add

        //
        // GET: /secure/add

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        //
        // GET: /secure/add

        [HttpPost]
        public ActionResult Add(AddMemberModel addMemberModel)
        {
            return RedirectToAction("AddSuccessful");
        }

        //
        // GET: /secure/add/successful

        [HttpGet]
        public ActionResult AddSuccessful()
        {
            return View();
        }

        #endregion

        #region Details

        //
        // GET: /secure/corp/details/117

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        #endregion
    }
}
