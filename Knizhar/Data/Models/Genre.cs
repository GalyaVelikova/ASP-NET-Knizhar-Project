namespace Knizhar.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Genre;
    public class Genre
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(GenreNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
