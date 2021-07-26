namespace Knizhar.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Condition;

    public class Condition
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ConditionNameMaxLength)]
        public string ConditionName { get; init; }

        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
