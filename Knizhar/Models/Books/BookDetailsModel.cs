using System.Collections.Generic;

namespace Knizhar.Services.Books.Models
{
    public class BookDetailsModel:IBookModel
    {
        public const int BooksPerPage = 5;
        public int CurrentPage { get; init; } = 1;

        public int TotalBooks { get; set; }
        public int Id { get; init; }
        public string Name { get; init; }

        public string ImagePath { get; init; }
        public string AuthorName { get; init; }
        public int AuthorId { get; init; }

        public string Isbn { get; init; }

        public string LanguageName { get; init; }
        public int LanguageId { get; set; }

        public string GenreName { get; init; }
        public int GenreId { get; set; }
        public string Description { get; set; }
        public string ConditionName { get; set; }
        public int ConditioId { get; set; }
        public string Comment { get; set; }

        public string TownName { get; init; }
        public string TheBookIsFor { get; set; }

        public decimal Price { get; set; }

        public int KnizharId { get; init; }

        public string KnizharName { get; init; }

        public string UserId { get; init; }

        public double AverageVote { get; set; }

        public int TotalVotes { get; set; }

        public bool isArchived { get; set; }

        public bool isFavouriteBook { get; set; }
        public IEnumerable<BookServiceModel> Books { get; set; }
    }

}
