using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FallenNova.Shared.DataAnnotations;

namespace FallenNova.Web.Areas.Secure.Models.AdministrationModel
{
    public class UpdateSkillTreeModel
    {
        [AllowHtml]
        [Required]
        [ValidXml]
        [Display(Name = "Skill Tree XML")]
        public string Xml { get; set; }
    }

    public class SkillTreeModel
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

    public class SkillModel
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

    public class SkillGroupModel
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

    public class NinjectTestResultsModel
    {
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}