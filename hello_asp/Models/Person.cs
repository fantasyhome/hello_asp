using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using hello_asp.CustomValidators;

namespace hello_asp.Models;

public class Person
{
    public Guid Id { get; set; }
    
    [Required]
    [Display(Name = "Person Name")]//{0} default Name(function name) if this validation not specified
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters")]
    [RegularExpression("^[A-Za-z .]$", ErrorMessage = "Only alphanumeric characters or dot are allowed")]
    public string? Name { get; set; }
    
    [MinimumYearValidator(2026)]
    public DateTime? DateOfBirth { get; set; }
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}