using FallenNova.Shared.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FallenNova.Service
{
    public class SkillTreeDetailsDto : Validator<SkillTreeDetailsDto>
    {
        [Required]
        [ValidXml]
        public string Xml { get; set; }
    }

    public class ItemDatabaseDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class ConstellationDetailsDto
    {
        public string LastName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public int RegionId { get; set; }
        public string RegionName { get; set; }

        public IEnumerable<SolarSystem> SolarSystems { get; set; }

        public class SolarSystem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Security { get; set; }
        }
    }

    public class ItemDetailsDto
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public int? IconId { get; set; }
        public int? RaceId { get; set; }
        public int? MarketGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class RegionDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Constellation> Constellations { get; set; }

        public class Constellation
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public IEnumerable<SolarSystem> SolarSystems { get; set; }

            public class SolarSystem
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public double Security { get; set; }
            }
        }
    }

    public class SolarSystemDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Security { get; set; }

        public int ConstellationId { get; set; }
        public string ConstellationName { get; set; }

        public int RegionId { get; set; }
        public string RegionName { get; set; }
    }

    public class SkillGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Skill> Skills { get; set; }

        public class Skill
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }

    public class SkillDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public string PrimaryEveOnlineAttributeName { get; set; }
        public string SecondaryEveOnlineAttributeName { get; set; }

        public int EveOnlineSkillGroupId { get; set; }
        public string EveOnlineSkillGroupName { get; set; }

        public IEnumerable<Skill> RequiredSkills { get; set; }

        public class Skill
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Level { get; set; }

            public IEnumerable<Skill> RequiredSkills { get; set; }
        }
    }

    public class SkillGroupDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Skill> Skills { get; set; }

        public class Skill
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
