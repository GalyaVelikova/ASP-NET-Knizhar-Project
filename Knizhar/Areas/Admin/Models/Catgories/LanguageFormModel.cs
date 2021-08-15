namespace Knizhar.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Language;
    public class LanguageFormModel
    {
        [Required]
        [StringLength(
            LanguageNameMaxLength,
            MinimumLength = LanguageNameMinLength,
            ErrorMessage = "The name of the language should be between {2} and {1} symbols long.")]
        public string Name { get; set; }
    }
}
