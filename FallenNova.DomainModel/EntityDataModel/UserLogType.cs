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
    
    public partial class UserLogType
    {
        public UserLogType()
        {
            this.UserLogs = new HashSet<UserLog>();
        }
    
        public int UserLogTypeId { get; set; }
        public string Title { get; set; }
    
        public virtual ICollection<UserLog> UserLogs { get; set; }
    }
}
