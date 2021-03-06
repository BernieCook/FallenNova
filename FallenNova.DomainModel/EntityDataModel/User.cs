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
    
    public partial class User
    {
        public User()
        {
            this.Characters = new HashSet<Character>();
            this.ContactUsLogs = new HashSet<ContactUsLog>();
            this.User1 = new HashSet<User>();
            this.User11 = new HashSet<User>();
            this.UserLogs = new HashSet<UserLog>();
            this.UserRoles = new HashSet<UserRole>();
            this.UserLogs1 = new HashSet<UserLog>();
            this.EveOnlineSkillTrees = new HashSet<EveOnlineSkillTree>();
        }
    
        public int UserId { get; set; }
        public int UserStatusId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string HashedPassword { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDateTime { get; set; }
        public int ModifiedByUserId { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
        public int UnsuccessfulLoginAttempts { get; set; }
        public Nullable<System.DateTime> LastSuccessfulLoginDateTime { get; set; }
        public Nullable<System.DateTime> LastSuccessfulAuthenticationDateTime { get; set; }
    
        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<ContactUsLog> ContactUsLogs { get; set; }
        public virtual ICollection<User> User1 { get; set; }
        public virtual User User2 { get; set; }
        public virtual ICollection<User> User11 { get; set; }
        public virtual User User3 { get; set; }
        public virtual UserStatus UserStatus { get; set; }
        public virtual ICollection<UserLog> UserLogs { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserLog> UserLogs1 { get; set; }
        public virtual ICollection<EveOnlineSkillTree> EveOnlineSkillTrees { get; set; }
    }
}
