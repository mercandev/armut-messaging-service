using System;
using System.ComponentModel.DataAnnotations;

namespace Armut.MS.Infrastructure.Validation;

public class CustomArmutValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int)
        {
            if ((int)value == default)
            {
                throw new NullReferenceException($"{validationContext.DisplayName} cannot be default!");
            }

            return ValidationResult.Success;
        }

        if (value.ToString() == ValidationControlWorldsString().FirstOrDefault())
        {
            throw new NullReferenceException($"{validationContext.DisplayName} cannot be empty or null!");
        }

        if (!string.IsNullOrWhiteSpace(Convert.ToString(value)))
        {
            return ValidationResult.Success;
        }

        throw new NullReferenceException($"{validationContext.DisplayName} cannot be empty or null!");
    }

    private IEnumerable<string> ValidationControlWorldsString()
    {
        yield return "string";
        yield return "0";
        yield return "";
        yield return null;
    }

}