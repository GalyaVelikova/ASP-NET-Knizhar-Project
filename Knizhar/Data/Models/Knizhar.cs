namespace Knizhar.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Knizhar;
    public class Knizhar
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(UserNameMaxLength)]
        public string UserName { get; init; }
       
        [Required]
        public int TownId { get; set; }

        public Town Town { get; init; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Book> Books { get; init; } = new List<Book>();
        public IList<Vote> Votes { get; init; } = new List<Vote>();
    }
}