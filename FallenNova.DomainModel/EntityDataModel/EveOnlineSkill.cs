//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FallenNova.DomainModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class EveOnlineSkill
    {
        public EveOnlineSkill()
        {
            this.CharacterEveOnlineSkills = new HashSet<CharacterEveOnlineSkill>();
            this.EveOnlineRequiredSkills = new HashSet<EveOnlineRequiredSkill>();
            this.EveOnlineRequiredSkills1 = new HashSet<EveOnlineRequiredSkill>();
        }
    
        public int EveOnlineSkillId { get; set; }
        public int EveOnlineSkillGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public int PrimaryEveOnlineAttributeId { get; set; }
        public int SecondaryEveOnlineAttributeId { get; set; }
    
        public virtual ICollection<CharacterEveOnlineSkill> CharacterEveOnlineSkills { get; set; }
        public virtual EveOnlineAttribute PrimaryEveOnlineAttribute { get; set; }
        public virtual EveOnlineAttribute SecondaryEveOnlineAttribute { get; set; }
        public virtual ICollection<EveOnlineRequiredSkill> EveOnlineRequiredSkills { get; set; }
        public virtual ICollection<EveOnlineRequiredSkill> EveOnlineRequiredSkills1 { get; set; }
        public virtual EveOnlineSkillGroup EveOnlineSkillGroup { get; set; }
    }
}