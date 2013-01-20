using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using FallenNova.DomainModel;
using FallenNova.Repository;
using FallenNova.Shared.ExtensionMethods;

namespace FallenNova.Service
{
    public class EveOnlineService : BaseService, IEveOnlineService
    {
        #region Constants

        private const string Const = "type";
        private const string ConstItem = "Item";
        private const string ConstConstellation = "Constellation";
        private const string ConstRegion = "Region";
        private const string ConstSolarSystem = "Solar System";

        private const string ConstDescription = "description";
        private const string ConstGroupId = "groupID";
        private const string ConstGroupName = "groupName";
        private const string ConstPrimaryEveOnlineAttributeName = "primaryAttribute";
        private const string ConstRank = "rank";
        private const string ConstRequiredAttributes = "requiredAttributes";
        private const string ConstRow = "row";
        private const string ConstRowSet = "rowset";
        private const string ConstSecondaryEveOnlineAttributeName = "secondaryAttribute";
        private const string ConstSkillLevel = "skillLevel";
        private const string ConstTypeId = "typeID";
        private const string ConstTypeName = "typeName";

        private const string ConstFakeSkill = "fake skill";

        // NOTE: At the time of writing (Nov 2012) there were 16 skill groups and 429 skills (not including the fake ones). 
        private const int ConstMinimumTotalSkillGroups = 16;
        private const int ConstMinimumTotalSkills = 429;

        #endregion

        private readonly IEveOnlineConstellationRepository _eveOnlineConstellationRepository;
        private readonly IEveOnlineTypeRepository _eveOnlineTypeRepository;
        private readonly IEveOnlineRegionRepository _eveOnlineRegionRepository;
        private readonly IEveOnlineSolarSystemRepository _eveOnlineSolarSystemRepository;
        private readonly IGenericRepository<EveOnlineSkill> _eveOnlineSkillRepository;
        private readonly IGenericRepository<EveOnlineSkillGroup> _eveOnlineSkillGroupRepository;
        private readonly IGenericRepository<EveOnlineSkillTree> _eveOnlineSkillTreeRepository;
        private readonly IEveOnlineRequiredSkillRepository _eveOnlineRequiredSkillRepository;
        private readonly IGenericRepository<EveOnlineAttribute> _eveOnlineAttributeRepository;
        private readonly IGenericRepository<UserLog> _userLogRepository;
        private readonly IRoleRepository _roleRepository;

        public EveOnlineService(
            IUnitOfWork unitOfWork,
            IEveOnlineConstellationRepository eveOnlineConstellationRepository,
            IEveOnlineTypeRepository eveOnlineTypeRepository,
            IEveOnlineRegionRepository eveOnlineRegionRepository,
            IEveOnlineSolarSystemRepository eveOnlineSolarSystemRepository,
            IGenericRepository<EveOnlineSkill> eveOnlineSkillRepository,
            IGenericRepository<EveOnlineSkillGroup> eveOnlineSkillGroupRepository,
            IGenericRepository<EveOnlineSkillTree> eveOnlineSkillTreeRepository,
            IEveOnlineRequiredSkillRepository eveOnlineRequiredSkillRepository,
            IGenericRepository<EveOnlineAttribute> eveOnlineAttributeRepository,
            IGenericRepository<UserLog> userLogRepository,
            IRoleRepository roleRepository)
        {
            UnitOfWork = unitOfWork;
            _eveOnlineConstellationRepository = eveOnlineConstellationRepository;
            _eveOnlineTypeRepository = eveOnlineTypeRepository;
            _eveOnlineRegionRepository = eveOnlineRegionRepository;
            _eveOnlineSolarSystemRepository = eveOnlineSolarSystemRepository;
            _eveOnlineSkillRepository = eveOnlineSkillRepository;
            _eveOnlineSkillGroupRepository = eveOnlineSkillGroupRepository;
            _eveOnlineSkillTreeRepository = eveOnlineSkillTreeRepository;
            _eveOnlineRequiredSkillRepository = eveOnlineRequiredSkillRepository;
            _eveOnlineAttributeRepository = eveOnlineAttributeRepository;
            _userLogRepository = userLogRepository;
            _roleRepository = roleRepository;
        }

        #region Get Methods

        #region Get Constellations

        public ConstellationDetailsDto GetConstellationDetails(int id)
        {
            var eveOnlineConstellation = _eveOnlineConstellationRepository.Get(
                eoc => eoc.EveOnlineConstellationId == id,
                eoc => eoc.EveOnlineSolarSystems,
                eoc => eoc.EveOnlineRegion);

            return new ConstellationDetailsDto
            {
                Id = eveOnlineConstellation.EveOnlineConstellationId,
                Name = eveOnlineConstellation.Name,
                RegionId = eveOnlineConstellation.EveOnlineRegionId,
                RegionName = eveOnlineConstellation.EveOnlineRegion.Name,
                SolarSystems = eveOnlineConstellation.EveOnlineSolarSystems.Select(ss =>
                new ConstellationDetailsDto.SolarSystem
                {
                    Id = ss.EveOnlineSolarSystemId,
                    Name = ss.Name,
                    Security = ss.Security
                }).ToList()
            };
        }

        #endregion

        #region Get Item

        public ItemDetailsDto GetItemDetails(int id)
        {
            var eveOnlineType = _eveOnlineTypeRepository.Get(
                eot => eot.EveOnlineTypeId == id,
                eot => eot.EveOnlineGroup,
                eot => eot.EveOnlineIcon,
                eot => eot.EveOnlineMarketGroup,
                eot => eot.EveOnlineRace);

            return new ItemDetailsDto
            {
                Id = eveOnlineType.EveOnlineTypeId,
                GroupId = eveOnlineType.EveOnlineGroupId,
                IconId = eveOnlineType.EveOnlineIconId,
                RaceId = eveOnlineType.EveOnlineRaceId,
                MarketGroupId = eveOnlineType.EveOnlineMarketGroupId,
                Name = eveOnlineType.Name,
                Description = eveOnlineType.Description,
            };
        }

        #endregion

        #region Get Items

        public IEnumerable<ItemDatabaseDetailsDto> GetItems(
            int pageIndex,
            int pageSize,
            string sortBy,
            bool sortAscending,
            out int totalUserCount,
            string keywords,
            bool includeItems,
            bool includeGalacticObjects)
        {
            var items = Enumerable.Empty<ItemDatabaseDetailsDto>();

            if (includeItems)
            {
                // Eve Online refer to "items" as "types". So for clarity I've translated it to "items" within the service layer.
                var types =
                    from t in _eveOnlineTypeRepository.GetByName(keywords)
                    select new ItemDatabaseDetailsDto
                    {
                        Id = t.EveOnlineTypeId,
                        Name = t.Name,
                        Type = ConstItem
                    };

                items = types;
            }

            if (includeGalacticObjects)
            {
                var constellations =
                    from c in _eveOnlineConstellationRepository.GetByName(keywords)
                    select new ItemDatabaseDetailsDto
                    {
                        Id = c.EveOnlineConstellationId,
                        Name = c.Name,
                        Type = ConstConstellation
                    };

                var regions =
                    from r in _eveOnlineRegionRepository.GetByName(keywords)
                    select new ItemDatabaseDetailsDto
                    {
                        Id = r.EveOnlineRegionId,
                        Name = r.Name,
                        Type = ConstRegion
                    };

                var solarSystems =
                    from ss in _eveOnlineSolarSystemRepository.GetByName(keywords)
                    select new ItemDatabaseDetailsDto
                    {
                        Id = ss.EveOnlineSolarSystemId,
                        Name = ss.Name,
                        Type = ConstSolarSystem
                    };

                items = (items.Equals(Enumerable.Empty<ItemDatabaseDetailsDto>()))
                    ? constellations
                    : items.Union(constellations);

                items = items.Union(regions);
                items = items.Union(solarSystems);
            }

            // Retrieve all items to avoid additional repository requests (and in turn database queries).
            // Calling .ToList() avoids re-executing each repository query when we call .Skip().Take() below as the data is now in memory.
            items = items.ToList();

            totalUserCount = items.Count();

            #region Sorting

            // More than one orderBy so that we can chain our ordering.
            Func<ItemDatabaseDetailsDto, string> orderBy = q => q.Name;
            Func<ItemDatabaseDetailsDto, string> thenBy = q => q.Type;

            if (sortBy.Trim().ToLower().Equals(Const))
            {
                orderBy = q => q.Type;
                thenBy = q => q.Name;
            }

            items = (sortAscending)
                ? items.OrderBy(orderBy).ThenBy(thenBy)
                : items.OrderByDescending(orderBy).ThenByDescending(thenBy);

            #endregion

            return items
                .Skip(((pageIndex - 1) * pageSize))
                .Take(pageSize);
        }

        #endregion

        #region Get Regions

        public RegionDetailsDto GetRegionDetails(int id)
        {
            var eveOnlineRegion = _eveOnlineRegionRepository.Get(
                eor => eor.EveOnlineRegionId == id,
                eor => eor.EveOnlineConstellations,
                eor => eor.EveOnlineConstellations.Select(eoc => eoc.EveOnlineSolarSystems));

            return new RegionDetailsDto
            {
                Id = eveOnlineRegion.EveOnlineRegionId,
                Name = eveOnlineRegion.Name,
                Constellations = eveOnlineRegion.EveOnlineConstellations.Select(c =>
                new RegionDetailsDto.Constellation
                {
                    Id = c.EveOnlineConstellationId,
                    Name = c.Name,
                    SolarSystems = c.EveOnlineSolarSystems.Select(ss =>
                    new RegionDetailsDto.Constellation.SolarSystem
                    {
                        Id = ss.EveOnlineSolarSystemId,
                        Name = ss.Name,
                        Security = ss.Security
                    })
                })
            };
        }

        #endregion

        #region Get Solar Systems

        public SolarSystemDetailsDto GetSolarSystemDetails(int id)
        {
            var eveOnlineSolarSystem = _eveOnlineSolarSystemRepository.Get(
                eoss => eoss.EveOnlineSolarSystemId == id,
                eoss => eoss.EveOnlineConstellation,
                eoss => eoss.EveOnlineConstellation.EveOnlineRegion);

            return new SolarSystemDetailsDto
            {
                Id = eveOnlineSolarSystem.EveOnlineSolarSystemId,
                Name = eveOnlineSolarSystem.Name,
                Security = eveOnlineSolarSystem.Security,
                ConstellationId = eveOnlineSolarSystem.EveOnlineConstellationId,
                ConstellationName = eveOnlineSolarSystem.EveOnlineConstellation.Name,
                RegionId = eveOnlineSolarSystem.EveOnlineConstellation.EveOnlineRegionId,
                RegionName = eveOnlineSolarSystem.EveOnlineConstellation.EveOnlineRegion.Name
            };
        }

        #endregion

        #region Get Skill Tree

        public IEnumerable<SkillGroupDto> GetSkillTree(
            out int totalSkillGroupCount)
        {
            IEnumerable<SkillGroupDto> skillGroups =
                from sg in _eveOnlineSkillGroupRepository.GetAll()
                orderby sg.Name ascending
                select new SkillGroupDto
                {
                    Id = sg.EveOnlineSkillGroupId,
                    Name = sg.Name,
                    Skills = sg.EveOnlineSkills.Select(s =>
                    new SkillGroupDto.Skill
                    {
                        Id = s.EveOnlineSkillId,
                        Name = s.Name,
                        Description = s.Description
                    }).OrderBy(s => s.Name)
                };

            totalSkillGroupCount = skillGroups.Count();

            return skillGroups;
        }

        #endregion

        #region Get Skill

        public SkillDetailsDto GetSkillDetails(int id)
        {
            var eveOnlineSkill = _eveOnlineSkillRepository.Get(
                eos => eos.EveOnlineSkillId == id,
                eos => eos.PrimaryEveOnlineAttribute,
                eos => eos.SecondaryEveOnlineAttribute,
                eos => eos.EveOnlineSkillGroup);

            var requiredSkills = GetEveOnlineRequiredSkills(eveOnlineSkill.EveOnlineSkillId);

            return new SkillDetailsDto
            {
                Id = eveOnlineSkill.EveOnlineSkillId,
                Name = eveOnlineSkill.Name,
                Description = eveOnlineSkill.Description,
                Rank = eveOnlineSkill.Rank,
                PrimaryEveOnlineAttributeName = eveOnlineSkill.PrimaryEveOnlineAttribute.Name,
                SecondaryEveOnlineAttributeName = eveOnlineSkill.SecondaryEveOnlineAttribute.Name,
                EveOnlineSkillGroupId = eveOnlineSkill.EveOnlineSkillGroupId,
                EveOnlineSkillGroupName = eveOnlineSkill.EveOnlineSkillGroup.Name,
                RequiredSkills = GetEveOnlineRequiredSkills(eveOnlineSkill.EveOnlineSkillId)
            };
        }

        // TODO: Try using yield to see if it provides a performance gain.
        /*
            public IEnumerable<Forum> GetHierachy(Forum forum)
            {
                yield forum;

                foreach (var x in forum.SubForums.SelectMany(s => GetHierarchy(s)))
                {
                    yield return x;
                }
            }
        */

        private IEnumerable<SkillDetailsDto.Skill> GetEveOnlineRequiredSkills(int eveOnlineSkillId)
        {
            IList<SkillDetailsDto.Skill> skills = new List<SkillDetailsDto.Skill>();

            var eveOnlineRequiredSkills = _eveOnlineRequiredSkillRepository.GetAll(
                eors => eors.EveOnlineSkillId == eveOnlineSkillId,
                eors => eors.RequiredEveOnlineSkill);

            foreach (var eveOnlineRequiredSkill in eveOnlineRequiredSkills)
            {
                skills.Add(new SkillDetailsDto.Skill
                {
                    Id = eveOnlineRequiredSkill.RequiredEveOnlineSkillId,
                    Name = eveOnlineRequiredSkill.RequiredEveOnlineSkill.Name,
                    Level = eveOnlineRequiredSkill.RequiredSkillLevel,
                    RequiredSkills = GetEveOnlineRequiredSkills(eveOnlineRequiredSkill.RequiredEveOnlineSkillId)
                });
            }

            return skills;
        }

        #endregion

        #region Get Skill Group

        public SkillGroupDetailsDto GetSkillGroupDetails(int id)
        {
            var eveOnlineSkillGroup = _eveOnlineSkillGroupRepository.Get(
                eosg => eosg.EveOnlineSkillGroupId == id,
                eosg => eosg.EveOnlineSkills);

            return new SkillGroupDetailsDto
            {
                Id = eveOnlineSkillGroup.EveOnlineSkillGroupId,
                Name = eveOnlineSkillGroup.Name,
                Skills = eveOnlineSkillGroup.EveOnlineSkills.Select(s =>
                new SkillGroupDetailsDto.Skill
                {
                    Id = s.EveOnlineSkillId,
                    Name = s.Name,
                }).OrderBy(s => s.Name)
            };
        }

        #endregion

        #endregion

        #region Update Methods

        public bool UpdateSkillTree(
            SkillTreeDetailsDto skillTreeDetailsDto,
            int addedByUserId,
            ref IList<string> errorMessages)
        {
            var skillTree = new XDocument();

            #region Validation

            if (!skillTreeDetailsDto.IsValid)
            {
                errorMessages = skillTreeDetailsDto.ErrorMessages.ToList();
            }

            if (_roleRepository.GetByUserId(addedByUserId).All(r => r.RoleId != (int)Role.Roles.Administrator))
            {
                errorMessages.Add("Wait a sec, you're attempting to update the skill tree and you're not an administrator. What gives?");
            }

            // Parse the Xml to ensure it meets some basic requirements, i.e. enough skills and skill groups.
            if (errorMessages.Count == 0)
            {
                // Xml validation is handled in the models so don't need to be checked here.
                skillTree = XDocument.Parse(skillTreeDetailsDto.Xml);

                // The "groupName" attribute is unique to skill group elements.
                var skillGroupCount =
                    (
                        from row in skillTree.Descendants(ConstRow)
                        where row.Attribute(ConstGroupName) != null
                        select row.Attribute(ConstGroupName).Value
                    ).Distinct().Count();

                // The "typeName" attribute is unique to skill elements.
                var skillCount =
                    (
                        from row in skillTree.Descendants(ConstRow)
                        where row.Attribute(ConstTypeName) != null
                        select row.Attribute(ConstTypeName).Value
                    ).Distinct().Count();

                if ((skillGroupCount < ConstMinimumTotalSkillGroups) ||
                    (skillCount < ConstMinimumTotalSkills))
                {
                    errorMessages.Add("This skill tree doesn't look valid. Are you sure it's correct? It should be around 400KB in size and contain a lot of XML.");
                }
            }

            if (errorMessages.Count > 0)
            {
                return false;
            }

            #endregion

            #region Process Skill Tree

            // Take care of the skill groups first.
            var skillGroups =
                (
                    skillTree.Descendants(ConstRow)
                        .Where(row => row.Attribute(ConstGroupName) != null)
                        .Where(row => !row.Attribute(ConstGroupName).Value.ToLower().Contains(ConstFakeSkill))
                        .Select(row => new
                        {
                            Id = (int)row.Attribute(ConstGroupId),
                            Name = (string)row.Attribute(ConstGroupName)
                        })
                ).Distinct();

            foreach (var skillGroup in skillGroups)
            {
                var eveOnlineSkillGroup = _eveOnlineSkillGroupRepository.GetById(skillGroup.Id);

                if (eveOnlineSkillGroup == null)
                {
                    _eveOnlineSkillGroupRepository.Insert(
                        new EveOnlineSkillGroup
                        {
                            EveOnlineSkillGroupId = skillGroup.Id,
                            Name = skillGroup.Name,
                        });
                }
                else
                {
                    eveOnlineSkillGroup.Name = skillGroup.Name;

                    _eveOnlineSkillGroupRepository.Update(eveOnlineSkillGroup);
                }
            }

            // Address skills next.
            var attributes = _eveOnlineAttributeRepository.GetAll().ToDictionary(a => a.Name.ToLower(), a => a.EveOnlineAttributeId);

            var skills =
                skillTree.Descendants(ConstRow)
                    .Where(row => row.Attribute(ConstTypeName) != null)
                    .Where(row => !row.Attribute(ConstTypeName).Value.ToLower().Contains(ConstFakeSkill))
                    .Select(row => new
                    {
                        Id = (int)row.Attribute(ConstTypeId),
                        GroupId = (int)row.Attribute(ConstGroupId),
                        Name = (string)row.Attribute(ConstTypeName),
                        Description = (string)row.Element(ConstDescription),
                        Rank = (int)row.Element(ConstRank),
                        PrimaryEveOnlineAttributeId = attributes[(string)row.Element(ConstRequiredAttributes).Element(ConstPrimaryEveOnlineAttributeName)],
                        SecondaryEveOnlineAttributeId = attributes[(string)row.Element(ConstRequiredAttributes).Element(ConstSecondaryEveOnlineAttributeName)],
                        RequiredSkills = row.Element(ConstRowSet).Elements(ConstRow)
                            .Where(rs => rs.Attribute(ConstTypeId) != null)
                                   .Select(rs => new
                                   {
                                       Id = (int)rs.Attribute(ConstTypeId),
                                       Level = (int)rs.Attribute(ConstSkillLevel)
                                   })
                    });

            // NOTE: This is the most efficient approach otherwise it's a lot of lookups, updates, inserts, deletes etc. 
            // Better to just delete everything given the foreign key relationship type and insert everything from scratch.
            _eveOnlineRequiredSkillRepository.DeleteAll();

            foreach (var skill in skills)
            {
                var eveOnlineSkill = _eveOnlineSkillRepository.GetById(skill.Id);

                if (eveOnlineSkill == null)
                {
                    _eveOnlineSkillRepository.Insert(
                        new EveOnlineSkill
                        {
                            EveOnlineSkillId = skill.Id,
                            EveOnlineSkillGroupId = skill.GroupId,
                            Name = skill.Name,
                            Description = skill.Description,
                            Rank = skill.Rank,
                            PrimaryEveOnlineAttributeId = skill.PrimaryEveOnlineAttributeId,
                            SecondaryEveOnlineAttributeId = skill.SecondaryEveOnlineAttributeId
                        });
                }
                else
                {
                    eveOnlineSkill.Name = skill.Name;
                    eveOnlineSkill.EveOnlineSkillGroupId = skill.GroupId;
                    eveOnlineSkill.Description = skill.Description;
                    eveOnlineSkill.Rank = skill.Rank;
                    eveOnlineSkill.PrimaryEveOnlineAttributeId = skill.PrimaryEveOnlineAttributeId;
                    eveOnlineSkill.SecondaryEveOnlineAttributeId = skill.SecondaryEveOnlineAttributeId;

                    _eveOnlineSkillRepository.Update(eveOnlineSkill);
                }

                foreach (var requiredSkill in skill.RequiredSkills)
                {
                    _eveOnlineRequiredSkillRepository.Insert(
                        new EveOnlineRequiredSkill
                        {
                            EveOnlineSkillId = skill.Id,
                            RequiredEveOnlineSkillId = requiredSkill.Id,
                            RequiredSkillLevel = requiredSkill.Level
                        });
                }
            }

            #endregion

            // Prepare the Eve Online skill tree entity for storage. The system holds onto it for historical purposes.
            var eveOnlineSkillTree = new EveOnlineSkillTree
            {
                Xml = skillTreeDetailsDto.Xml,
                AddedByUserId = addedByUserId,
                AddedDateTime = DateTime.Now.ToGmtDateTime()
            };

            var userLog = new UserLog
            {
                UserId = addedByUserId,
                UserLogTypeId = (int)UserLogType.Types.UpdatedEveOnlineSkillTree,
                AddedDateTime = DateTime.Now.ToGmtDateTime()
            };

            _eveOnlineSkillTreeRepository.Insert(eveOnlineSkillTree);
            _userLogRepository.Insert(userLog);
            UnitOfWork.Commit();

            return true;
        }

        #endregion
    }
}
