namespace Knizhar.Models.Home
{
    using Knizhar.Data.Models;
    using Knizhar.Services.Books.Models;
    using System.Collections.Generic;
    public class IndexViewModel
    {
        public const int BooksPerSlide = 4;
        public int CurrentSlide { get; init; } = 1;
        public int TotalBooks { get; init; }
        public int TotalKnizhari { get; init; }
        public List<BookServiceModel> RecentlyAddedBooks { get; set; } 
        public IEnumerable<Book> BooksByGenre { get; set; } = new List<Book>();

        public IEnumerable<Book> BooksByLocation { get; set; } = new List<Book>();

    }
}
