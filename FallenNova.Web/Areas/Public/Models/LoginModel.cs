using FallenNova.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace FallenNova.Web.Areas.Public.Models
{
    public class LoginModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter a value for the \"{0}\" field.")]
        [StringLength(StringLength.EmailAddress, ErrorMessage="The value in the \"{0}\" field is too long.")]
        [EmailAddressAttribute(ErrorMessage = "Please enter valid email address for the \"{0}\" field.")]
        public string EmailAddress { get; set; }

        [Display(Name="Password")]
        [Required(ErrorMessage = "Please enter a value for the \"{0}\" field.")]
        [StringLength(StringLength.Password, ErrorMessage="The value in the \"{0}\" field is too long.")]
        public string Password { get; set; }

        [Display(Name="Keep me logged in")]
        public bool RememberMe { get; set; }
    }
}
