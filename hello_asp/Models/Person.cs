using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using hello_asp.CustomValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace hello_asp.Models;

public class Person : IValidatableObject
{
    [BindNever]
    public Guid Id { get; set; }
    
    [Required]
    [Display(Name = "Person Name")]//{0} default Name(function name) if this validation not specified
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters")]
    [RegularExpression("^[A-Za-z .]$", ErrorMessage = "Only alphanumeric characters or dot are allowed")]
    public string? Name { get; set; }
    
    [MinimumYearValidator(2026)]
    public DateTime? DateOfBirth { get; set; }
    
    [DateRangeValidator("DateOfBirth", ErrorMessage = "Date of death shouldn't be older than DateOfBirth")]
    public DateTime? DateOfDeath { get; set; }

    // Tags[0],Tags[1],etc. will be collected
    public List<string?> Tags { get; set; } = new List<string?>();
    
    public enum Genders { Male, Female }
    
    public Genders Gender { get; set; } = Genders.Male;
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }

    // 不可复用在其他类的快速验证
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!DateOfBirth.HasValue && !DateOfDeath.HasValue)
        {
            yield return new ValidationResult("", [nameof(DateOfDeath)]);// 多个yield组合成IEnumerable
        }
    }
}