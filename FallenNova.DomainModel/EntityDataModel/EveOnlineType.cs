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
    
    public partial class EveOnlineType
    {
        public int EveOnlineTypeId { get; set; }
        public Nullable<int> EveOnlineGroupId { get; set; }
        public Nullable<int> EveOnlineIconId { get; set; }
        public Nullable<int> EveOnlineRaceId { get; set; }
        public Nullable<int> EveOnlineMarketGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual EveOnlineGroup EveOnlineGroup { get; set; }
        public virtual EveOnlineIcon EveOnlineIcon { get; set; }
        public virtual EveOnlineMarketGroup EveOnlineMarketGroup { get; set; }
        public virtual EveOnlineRace EveOnlineRace { get; set; }
    }
}