using AutoMapper;
using FallenNova.Service;
using FallenNova.Shared.ExtensionMethods;
using FallenNova.Web.Areas.Secure.Models.AdministrationModel;
using FallenNova.Web.Constants;
using FallenNova.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FallenNova.Web.Areas.Secure.Controllers
{
    [AuthenticateAndAuthorizeAttribute(Roles = Roles.Administrator)]
    public class AdministrationController : BaseController
    {
        private readonly IEveOnlineService _eveOnlineService;
        private readonly IUserService _userService;
        private readonly IContactUsService _contactUsService;

        public AdministrationController(
            IEveOnlineService eveOnlineService,
            IUserService userService,
            IContactUsService contactUsService)
        {
            _eveOnlineService = eveOnlineService;
            _userService = userService;
            _contactUsService = contactUsService;
        }

        #region Index
 
        //
        // GET: /secure/administration

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Skills

        //
        // GET: /secure/administration/skill/117

        [HttpGet]
        public ActionResult Skill(int id)
        {
            var skillDetailsDto = _eveOnlineService.GetSkillDetails(id);

            Mapper.CreateMap<SkillDetailsDto, SkillModel>();
            Mapper.CreateMap<SkillDetailsDto.Skill, SkillModel.Skill>();
            var skillModel = Mapper.Map<SkillDetailsDto, SkillModel>(skillDetailsDto);

            return View(skillModel);
        }
        
        //
        // GET: /secure/administration/skillgroup/117

        [HttpGet]
        public ActionResult SkillGroup(int id)
        {
            var skillGroupDetailsDto = _eveOnlineService.GetSkillGroupDetails(id);

            Mapper.CreateMap<SkillGroupDetailsDto, SkillGroupModel>();
            Mapper.CreateMap<SkillGroupDetailsDto.Skill, SkillGroupModel.Skill>();
            var skillGroupModel = Mapper.Map<SkillGroupDetailsDto, SkillGroupModel>(skillGroupDetailsDto);

            return View(skillGroupModel);
        }

        //
        // GET: /secure/administration/skilltree

        [HttpGet]
        public ActionResult SkillTree()
        {
            int totalResults;

            var skillGroupDtos = _eveOnlineService.GetSkillTree(out totalResults);

            ViewBag.TotalResults = totalResults;

            Mapper.CreateMap<SkillGroupDto, SkillTreeModel>();
            Mapper.CreateMap<SkillGroupDto.Skill, SkillTreeModel.Skill>();
            var skillTreeModels = Mapper.Map<IEnumerable<SkillGroupDto>, IEnumerable<SkillTreeModel>>(skillGroupDtos);

            return View(skillTreeModels);
        }

        //
        // GET: /secure/administration/updateskilltree

        [HttpGet]
        public ActionResult UpdateSkillTree()
        {
            return View();
        }

        //
        // POST: /secure/administration/updateskilltree

        [HttpPost]
        public ActionResult UpdateSkillTree(UpdateSkillTreeModel updateSkillTreeModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<UpdateSkillTreeModel, SkillTreeDetailsDto>();
                var skillTreeDetailsDto = Mapper.Map<UpdateSkillTreeModel, SkillTreeDetailsDto>(updateSkillTreeModel);

                IList<string> errorMessages = new List<string>();

                if (!_eveOnlineService.UpdateSkillTree(
                    skillTreeDetailsDto,
                    CurrentUser.UserId,
                    ref errorMessages))
                {
                    ModelState.AddModelErrors(errorMessages);
                }
                else
                {
                    return RedirectToAction("UpdateSkillTreeSuccessful");
                }
            }

            // Reaching this point means something went wrong.
            return View(updateSkillTreeModel);
        }

        //
        // GET: /secure/administration/updateskilltreesuccessful

        [HttpGet]
        public ActionResult UpdateSkillTreeSuccessful()
        {
            return View();
        }

        #endregion

        #region Diagnostics

        [HttpGet]
        public ActionResult Ninject()
        {
            // Perform a number of service calls to test the Ninject configuration.
            var ninjectTestResultsModel = new NinjectTestResultsModel
                {
                    StartTime = DateTime.UtcNow,
                    Status = "OK"
                };
            
            try
            {
                IList<string> errorMessages = new List<string>();
                var userStatusDetailsDto = _userService.GetStatusDetails(CurrentUser.UserId);
                var userStatusId = userStatusDetailsDto.UserStatusId;
                
                // Switch the user status identifier.
                userStatusDetailsDto.UserStatusId = (userStatusId == 1) ? 2 : 1;

                if (!_userService.UpdateStatus(
                    userStatusDetailsDto,
                    CurrentUser.UserId,
                    ref errorMessages))
                {
                    throw new Exception(errorMessages[0]);
                }

                // Switch the user's status back to how it was.
                userStatusDetailsDto.UserStatusId = userStatusId;

                if (!_userService.UpdateStatus(
                    userStatusDetailsDto,
                    CurrentUser.UserId,
                    ref errorMessages))
                {
                    throw new Exception(errorMessages[0]);
                }

                var contactUsDetailsDto = new ContactUsDetailsDto
                {
                    Name = "Joe Bloggs",
                    EmailAddress = "joe.bloggs@test.com,",
                    Message = "Testing..."
                };

                if (!_contactUsService.Insert(
                    contactUsDetailsDto,
                    (CurrentUser != null) ? CurrentUser.UserId : (int?)null,
                    ref errorMessages))
                {
                    throw new Exception(errorMessages[0]);
                }
            }
            catch (Exception exception)
            {
                ninjectTestResultsModel.Status = exception.Message;
            }

            ninjectTestResultsModel.EndTime = DateTime.Now.ToGmtDateTime();

            return View(ninjectTestResultsModel);
        }

        #endregion
    }
}
