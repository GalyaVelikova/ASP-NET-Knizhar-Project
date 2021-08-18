namespace Knizhar.Models.Books
{
    using Knizhar.Services.Books.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class BookSearchViewModel
    {
        public const int BooksPerPage = 5;

        public int CurrentPage { get; init; } = 1;

        public int TotalBooks { get; set; }

        public string Knizhar { get; set; }

        public string Genre { get; init; }
        public IEnumerable<string> Genres { get; set; }

        public string Town { get; init; }
        public IEnumerable<string> Towns { get; set; }

        public string Language { get; init; }
        public IEnumerable<string> Languages { get; set; }

        [Display(Name="Search")]
        public string SearchTerm { get; set; }

        public BookSorting Sorting { get; init; }

        public string ImagePath { get; set; }

        public string Filter { get; set; }

        public string Author { get; set; }

        public IEnumerable<BookServiceModel> Books { get; set; }
    }
}
