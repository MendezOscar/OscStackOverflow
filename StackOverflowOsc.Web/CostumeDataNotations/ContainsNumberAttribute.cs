using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverflowOsc.Web.CostumeDataNotations
{
    public class ContainsNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var text = value.ToString();
                var hasNumber = text.Any(char.IsDigit);
                return hasNumber ? ValidationResult.Success : new ValidationResult("Must contain a number");
            }
            return new ValidationResult("required fiels");
        }
    }
}