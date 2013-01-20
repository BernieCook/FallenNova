using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FallenNova.Web.Areas.Public.Models
{
    public class ContactUsModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [EmailAddressAttribute(ErrorMessage = "Please enter valid email address for the \"{0}\" field.")]
        public string EmailAddress { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}