namespace Knizhar.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Language;

    public class Language
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(LanguageNameMaxLength)]
        public string LanguageName { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
