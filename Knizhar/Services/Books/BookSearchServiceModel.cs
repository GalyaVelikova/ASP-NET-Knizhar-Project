namespace Knizhar.Services.Books
{
    using System.Collections.Generic;
    public class BookSearchServiceModel
    {
        public const int BooksPerPage = 5;

        public int CurrentPage { get; init; } = 1;

        public int TotalBooks { get; set; }

        public IEnumerable<BookServiceModel> Books { get; set; }
    }
}
