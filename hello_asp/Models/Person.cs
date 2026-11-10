using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace hello_asp.Models;

public class Person
{
    [Required]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}