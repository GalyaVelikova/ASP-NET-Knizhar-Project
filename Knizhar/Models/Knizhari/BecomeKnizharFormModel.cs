namespace Knizhar.Models.Knizhari
{
    using Knizhar.Services.Knizhari;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Knizhar;
    using static Data.DataConstants.Town;
    public class BecomeKnizharFormModel
    {
        [Required]
        [StringLength(
            UserNameMaxLength,
            MinimumLength = UserNameMinLength,
            ErrorMessage = "The username should be between {2} and {1} symbols long.")]
        public string UserName { get; init; }

        //[Required] TO ADD FUNCTIONALITY TO ADD NEW TOWNS TO THE TOWNS TABLE
        //[StringLength(
        //   TownNameMaxLength,
        //   MinimumLength = TownNameMinLength,
        //   ErrorMessage = "The name of the town should be between {2} and {1} symbols long.")]
        //public string Town { get; init; }

        [Required]
        [Display(Name = "Town")]
        public int TownId { get; set; }

        //Field for uploading image of the Knizhar

        public IEnumerable<TownServiceModel> Towns { get; set; }
    }
}
