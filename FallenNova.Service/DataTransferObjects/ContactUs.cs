using System.ComponentModel.DataAnnotations;

namespace FallenNova.Service
{
    public class ContactUsDetailsDto : Validator<ContactUsDetailsDto>
    {
        public string Name { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "The email address isn't formatted correctly, is it missing an '@'.")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A value must be supplied for the message.", MinimumLength = 1)]
        public string Message { get; set; }
    }
}
