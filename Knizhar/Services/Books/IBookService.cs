namespace Knizhar.Services.Books
{
    using Knizhar.Data.Models;
    using Knizhar.Models.Books;
    using System.Collections.Generic;

    public interface IBookService
    {
        BookSearchServiceModel All(
            string genre,
            string town,
            string language,
            string searchTerm,
            BookSorting sorting,
            int currentPage,
            int booksPerPage);

        IEnumerable<string> AllBookGenres();
        IEnumerable<string> AllBookTowns();
        IEnumerable<string> AllBookLanguages();

    }
}
