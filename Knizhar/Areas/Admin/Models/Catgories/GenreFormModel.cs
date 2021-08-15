namespace Knizhar.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Genre;
    public class GenreFormModel
    {
        [Required]
        [StringLength(
            GenreNameMaxLength,
            MinimumLength = GenreNameMinLength,
            ErrorMessage = "The name of the genre should be between {2} and {1} symbols long.")]
        public string Name { get; set; }
    }
}
