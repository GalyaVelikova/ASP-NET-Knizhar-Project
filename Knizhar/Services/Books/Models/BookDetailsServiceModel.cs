namespace Knizhar.Models.Books.Models
{
    using Knizhar.Services.Books.Models;
    using System.Collections.Generic;
    public class BookDetailsServiceModel
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public string ImageUrl { get; init; }
        public string AuthorName { get; init; }
        public int AuthorId { get; init; }

        public string Isbn { get; init; }

        public string Language { get; init; }
        public int LanguageId { get; set; }

        public string Genre { get; init; }
        public int GenreId { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }
        public int ConditioId { get; set; }
        public string Comment { get; set; }

        //public string BookLocation { get; init; }
        public bool IsForGiveAway { get; set; }

        public decimal Price { get; set; }

        public int KnizharId { get; init; }

        public string KnizharName { get; init; }

        public string UserId { get; init; }

        public IEnumerable<BookGenreServiceModel> Genres { get; set; }

        public IEnumerable<BookLanguageServiceModel> Languages { get; set; }

        public IEnumerable<BookConditionServiceModel> Conditions { get; set; }
    }
}
