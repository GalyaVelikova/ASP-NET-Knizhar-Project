namespace Knizhar.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Town;
    public class Town
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TownNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Knizhar> Knizhari { get; set; } = new List<Knizhar>();
    }
}
