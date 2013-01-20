using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using FallenNova.Web.Areas.Shared.Models;
using FallenNova.Web.Areas.Secure.Models.ItemDatabaseModel;
using FallenNova.Web.Constants;
using FallenNova.Service;

namespace FallenNova.Web.Areas.Secure.Controllers
{
    [AuthenticateAndAuthorizeAttribute(Roles = Roles.Member)]
    public class ItemDatabaseController : BaseController
    {
        private const string ConstSortByName = "Name";
        private const string ConstSortByType = "Type";

        private readonly IEveOnlineService _eveOnlineService;

        public ItemDatabaseController(IEveOnlineService eveOnlineService)
        {
            _eveOnlineService = eveOnlineService;
        }

        #region Index

        //
        // GET: /secure/itemdatabase

        [HttpGet]
        public ActionResult Index(
            int pageIndex = 1, 
            int pageSize = 10,
            string sortBy = ConstSortByName,
            bool sortAscending = true,
            string keywords = null,
            bool includeItems = false,
            bool includeGalacticObjects = false)
        {
            var isKeywordsNull = (keywords == null);

            var itemDatabaseModel = new ItemDatabaseModel
            {
                Keywords = keywords,
                IncludeItems = isKeywordsNull || includeItems,
                IncludeGalacticObjects = (isKeywordsNull) || includeGalacticObjects
            };

            IPagingInfo pagingInfo = new PagingInfo
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                SortBy = sortBy,
                SortAscending = sortAscending,
                RouteName = RouteNames.Secure.Data,
                RouteUrl = RouteUrls.Secure.Data
            };

            ViewBag.ShowSearchResults = !isKeywordsNull;
            ViewBag.PagingInfo = pagingInfo;

            if (ViewBag.ShowSearchResults)
            {
                int totalResults;

                var itemDatabaseDetailsDtos =
                    _eveOnlineService.GetItems(
                    pageIndex,
                    pageSize,
                    sortBy,
                    sortAscending,
                    out totalResults,
                    keywords,
                    includeItems,
                    includeGalacticObjects);

                pagingInfo.TotalResults = totalResults;
            
                ViewBag.NameSortAscending = pagingInfo.IsSortAscending(ConstSortByName);
                ViewBag.TypeSortAscending = pagingInfo.IsSortAscending(ConstSortByType);

                Mapper.CreateMap<ItemDatabaseDetailsDto, ItemDatabaseModel.Item>();
                itemDatabaseModel.Items = Mapper.Map<IEnumerable<ItemDatabaseDetailsDto>, IEnumerable<ItemDatabaseModel.Item>>(itemDatabaseDetailsDtos);
            }

            return View(itemDatabaseModel);
        }

        #endregion

        #region Constellation

        //
        // GET: /secure/itemdatabase/constellation/117

        [HttpGet]
        public ActionResult Constellation(int id)
        {
            var constellationDetailsDto = _eveOnlineService.GetConstellationDetails(id);

            Mapper.CreateMap<ConstellationDetailsDto, ConstellationModel>();
            Mapper.CreateMap<ConstellationDetailsDto.SolarSystem, ConstellationModel.SolarSystem>();
            var constellationModel = Mapper.Map<ConstellationDetailsDto, ConstellationModel>(constellationDetailsDto);

            return View(constellationModel);
        }

        #endregion

        #region Item

        //
        // GET: /secure/itemdatabase/item/117

        [HttpGet]
        public ActionResult Item(int id)
        {
            var itemDetailsDto = _eveOnlineService.GetItemDetails(id);

            Mapper.CreateMap<ItemDetailsDto, ItemModel>();
            var itemModel = Mapper.Map<ItemDetailsDto, ItemModel>(itemDetailsDto);

            return View(itemModel);
        }

        #endregion

        #region Region

        //
        // GET: /secure/itemdatabase/region/117

        [HttpGet]
        public ActionResult Region(int id)
        {
            var regionDetailsDto = _eveOnlineService.GetRegionDetails(id);

            Mapper.CreateMap<RegionDetailsDto, RegionModel>();
            Mapper.CreateMap<RegionDetailsDto.Constellation, RegionModel.Constellation>();
            Mapper.CreateMap<RegionDetailsDto.Constellation.SolarSystem, RegionModel.Constellation.SolarSystem>();
            var regionModel = Mapper.Map<RegionDetailsDto, RegionModel>(regionDetailsDto);

            return View(regionModel);
        }

        #endregion

        #region Solar System

        //
        // GET: /secure/itemdatabase/solarsystem/117

        [HttpGet]
        public ActionResult SolarSystem(int id)
        {
            var solarSystemDetailsDto = _eveOnlineService.GetSolarSystemDetails(id);

            Mapper.CreateMap<SolarSystemDetailsDto, SolarSystemModel>();
            var solarSystemModel = Mapper.Map<SolarSystemDetailsDto, SolarSystemModel>(solarSystemDetailsDto);

            return View(solarSystemModel);
        }

        #endregion
    }
}
