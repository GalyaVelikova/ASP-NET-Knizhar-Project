namespace Knizhar.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Condition;
    public class ConditionFormModel
    {
        [StringLength(
            ConditionNameMaxLength,
            MinimumLength = ConditionNameMinLength,
            ErrorMessage = "The name of the condition should be between {2} and {1} symbols long.")]
        public string Name { get; set; }
    }
}
