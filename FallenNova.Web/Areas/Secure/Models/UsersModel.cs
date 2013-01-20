using FallenNova.Shared.ExtensionMethods;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FallenNova.Web.Areas.Secure.Models.UsersModel
{
    public class UsersModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName 
        { 
            get { return string.Concat(FirstName, " ", Surname); } 
        }
    }

    public class UserDetailsModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }

        public class Role
        {
            public string Title { get; set; }
        }

        public class Character
        {
            public int CharacterId { get; set; }
            public string CharacterName { get; set; }
            public string CorporationName { get; set; }
        }
         
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Character> Characters { get; set; }

        public string RoleTitles
        {
            get
            {
                var roleTitles = Roles.Aggregate(string.Empty, (current, role) => current + role.Title + ", ");

                return roleTitles.Substring(0, (roleTitles.Length - 2));
            }
        }
    }

    public class AddUserModel
    {
        [Required]
        [Display(Name = "First name")]
        [StringLength(100, ErrorMessage = "A value must be supplied for the user's first name.", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        [StringLength(100, ErrorMessage = "A value must be supplied for the user's surname.", MinimumLength = 1)]
        public string Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Required]
        public int RoleId { get; set; }
    }

    public class AddUserSuccessfulModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
        
        public string FirstNameWithApostrope
        {
            get { return FirstName.WithApostrophe(); }
        }
    }

    public class EditDetailsUserModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(100, ErrorMessage = "A value must be supplied for the user's first name.", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        [StringLength(100, ErrorMessage = "A value must be supplied for the user's surname.", MinimumLength = 1)]
        public string Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Required]
        public int RoleId { get; set; }
    }

    public class EditStatusUserModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        [Required]
        public int UserStatusId { get; set; }
    }

    public class EditUserSuccessfulModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string FirstNameWithApostrope
        {
            get { return FirstName.WithApostrophe(); }
        }
    }

}