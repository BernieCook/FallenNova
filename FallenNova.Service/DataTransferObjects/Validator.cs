using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FallenNova.Service
{
    public class Validator<TEntity> where TEntity : class
    {
        private bool? _isValid;
        private IList<string> _errorMessages;

        public bool IsValid
        {
            get
            {
                if (!_isValid.HasValue)
                {
                    Validate();
                }

                return ((_isValid != null) &&
                    (_isValid.Value));
            }
        }

        public IEnumerable<string> ErrorMessages
        {
            get
            {
                if (!_isValid.HasValue)
                {
                    Validate();
                }

                return _errorMessages;
            }
        }

        private void Validate()
        {
            _errorMessages = new List<string>();
            _isValid = false;

            IList<ValidationResult> validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this, null, null);

            if (!Validator.TryValidateObject(this, validationContext, validationResults))
            {
                foreach (var validationResult in validationResults)
                {
                    _errorMessages.Add(validationResult.ErrorMessage);
                }

                return;
            }

            _isValid = true;
        }
    }
}
