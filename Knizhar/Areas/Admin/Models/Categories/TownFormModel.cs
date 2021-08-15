namespace Knizhar.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Town;
    public class TownFormModel
    {
        [Required]
        [StringLength(
            TownNameMaxLength,
            MinimumLength = TownNameMinLength,
            ErrorMessage = "The name of the town should be between {2} and {1} symbols long.")]
        public string Name { get; set; }
    }
}
