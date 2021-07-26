namespace Knizhar.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using static Data.DataConstants.Book;
    public class IsbnValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!Regex.IsMatch((string)value, IsbnRegularExpression10Digits) && !Regex.IsMatch((string)value, IsbnRegularExpression13Digits))
            {
                return new ValidationResult("The ISBN is ten digits long if assigned before 2007, and thirteen digits long if assigned on or after 1 January 2007. Please enter the ISBN number in the correct format.");
               
            };
            return ValidationResult.Success;
        }
    }
}
