using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace backend.Attributes
{
    public class StringRangeAttribute : ValidationAttribute
    {
        private readonly string[] validValues;
        
        public StringRangeAttribute(params string[] ValidValues)
        {
            validValues = ValidValues;
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not string strVal) return new ValidationResult("Invalid type");
            if (!validValues.Contains(strVal))
            {
                var errorMsg = $"Valid value should be one of the following: {string.Join(", ", validValues)}";
                return new ValidationResult(errorMsg);
            }
;            
            return ValidationResult.Success;
        }
    }
}