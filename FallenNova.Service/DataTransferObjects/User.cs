using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FallenNova.Service
{
    public class UserContactDetailsDto : Validator<UserContactDetailsDto>
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A value must be supplied for the user's first name.", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A value must be supplied for the user's surname.", MinimumLength = 1)]
        public string Surname { get; set; }

        [StringLength(200, ErrorMessage = "A value must be supplied for the user's email address.", MinimumLength = 1)]
        [DataType(DataType.EmailAddress, ErrorMessage = "The email address isn't formatted correctly, is it missing an '@'.")]
        public string EmailAddress { get; set; }
    }

    public class UserPasswordDto : Validator<UserPasswordDto>
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The new password must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }

    public class UserStatusDetailsDto
    {
        public int UserId { get; set; }
        public int UserStatusId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }

    public class UserAuthenticationDetailsDto
    {
        public int UserId { get; set; }
        public int UserStatusId { get; set; }
        public string EmailAddress { get; set; }
        public string HashedPassword { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserDetailsDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public IEnumerable<CharacterDetailsDto> Characters { get; set; }
        public IEnumerable<RoleDetailsDto> Roles { get; set; }
    }

    public class UserIdentityDetailsDto
    {
        public int UserId { get; set; }
        public int UserStatusId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public IEnumerable<RoleDetailsDto> Roles { get; set; }
    }

    public class UserStatusDto
    {
        public int UserId { get; set; }
        public int UserStatusId { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserRolesDto
    {
        public int UserId { get; set; }
        public IList<int> RoleIds { get; set; }
    }
}
