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
    
    public partial class EveOnlineRegion
    {
        public EveOnlineRegion()
        {
            this.EveOnlineConstellations = new HashSet<EveOnlineConstellation>();
        }
    
        public int EveOnlineRegionId { get; set; }
        public Nullable<int> EveOnlineFactionId { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<EveOnlineConstellation> EveOnlineConstellations { get; set; }
        public virtual EveOnlineFaction EveOnlineFaction { get; set; }
    }
}