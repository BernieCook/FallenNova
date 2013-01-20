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
    
    public partial class Character
    {
        public Character()
        {
            this.KillLogs = new HashSet<KillLog>();
            this.CharacterEveOnlineSkills = new HashSet<CharacterEveOnlineSkill>();
        }
    
        public int CharacterId { get; set; }
        public int UserId { get; set; }
        public int CorporationId { get; set; }
        public int KeyId { get; set; }
        public string VCode { get; set; }
        public string CharacterName { get; set; }
        public System.DateTime AddedDateTime { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
        public byte[] Timestamp { get; set; }
    
        public virtual Corporation Corporation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<KillLog> KillLogs { get; set; }
        public virtual ICollection<CharacterEveOnlineSkill> CharacterEveOnlineSkills { get; set; }
    }
}
