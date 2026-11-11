using System.ComponentModel.DataAnnotations;

namespace hello_asp.CustomValidators;

public class MinimumYearValidatorAttribute(int minimumYear = 2000) : ValidationAttribute
{
    public int MinimumYear { get; set; } = minimumYear;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            DateTime date = (DateTime)value;
            if (date.Year < MinimumYear)
            {
                return new ValidationResult($"Minimum year must be greater than or equal to {MinimumYear}");
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        return null;
    }
}