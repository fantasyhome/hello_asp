using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace hello_asp.CustomValidators;

public class DateRangeValidatorAttribute(string otherPropertyName) : ValidationAttribute
{
    public string OtherPropertyName { get; set; } = otherPropertyName;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            DateTime dateOfDeath = Convert.ToDateTime(value.ToString());
            
            PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);
            if (otherProperty != null)
            {
                DateTime dateOfBirth = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance, null));

                if (dateOfBirth > dateOfDeath)
                {
                    if (validationContext.MemberName != null)
                        return new ValidationResult(ErrorMessage,
                            [OtherPropertyName, validationContext.MemberName]); // 一个错误关联到两个属性对应的 ModelState 中,ErrorMessage出现两次
                }
            }
        }
        return null;
    }
}