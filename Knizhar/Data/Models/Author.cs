namespace Knizhar.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Author;

    public class Author
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(AuthorNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; init; } = new List<Book>();
    }
}
