using Knizhar.Models.Books;
using System.Collections.Generic;

namespace Knizhar.Services.Books
{
    public class BookSearchServiceModel
    {
        public const int BooksPerPage = 5;

        public int CurrentPage { get; init; } = 1;

        public int TotalBooks { get; set; }

        public IEnumerable<BookServiceModel> Books { get; set; }
    }
}
