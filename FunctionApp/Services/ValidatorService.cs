using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureFunctionApp.Services
{
    public struct ValidationResults
    {
        public bool IsValid { get; set; }
        public List<ValidationResult> Results { get; set; }
    }

    public interface IValidatorService
    {
        ValidationResults TryValidateObject<T>(T obj);
    }

    public class ValidatorService : IValidatorService
    {
        public ValidationResults TryValidateObject<T>(T obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return new ValidationResults
            {
                IsValid = validationResults.Count == 0,
                Results = validationResults
            };
        }
    }
}
