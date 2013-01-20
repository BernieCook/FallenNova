using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Linq;

namespace FallenNova.Shared.DataAnnotations
{
    public class ValidXml : ValidationAttribute
    {
        /// <summary>
        /// Validates the string to determine whether or not it's an Xml document.
        /// </summary>
        /// <param name="value">Value to validate.</param>
        /// <param name="validationContext">Validation context.</param>
        /// <returns>Validation result.</returns>
        /// <remarks>Best used in conjunction with a [Required] data annotation.</remarks>
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value != null)
            {
                // Catching the exception is faster than a straight up Xml parse.
                try
                {
                    XDocument.Parse(value.ToString());
                }
                catch (XmlException)
                {
                    return new ValidationResult(
                            FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }
}
