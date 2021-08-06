namespace Knizhar.Services.Books
{
    using Knizhar.Data.Models;
    using Knizhar.Models.Books;
    using Knizhar.Models.Books.Models;
    using Knizhar.Services.Books.Models;
    using Knizhar.Services.Knizhari;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public interface IBookService
    {

        int Create(
                string isbn,
                string name,
                int genreId,
                int languageId,
                int conditionId,
                IFormFile image,
                string description,
                int author,
                string comment,
                bool isForGiveAway,
                decimal price,
                int knizharId,
                string imagePath);

        bool Edit(
                int bookId,
                string isbn,
                string name,
                int genreId,
                int languageId,
                int conditionId,
                string description,
                int author,
                string comment,
                bool isForGiveAway,
                decimal price);

        Author GetAuthor(string authorName);
        BookSearchServiceModel All(
            string genre,
            string town,
            string language,
            string searchTerm,
            BookSorting sorting,
            int currentPage,
            int booksPerPage,
            string imagePath);
        BookDetailsServiceModel Details(int bookId);
        IEnumerable<BookServiceModel> ByUser(string userId, string imagePath);
        IEnumerable<BookGenreServiceModel> AllGenres();
        IEnumerable<BookLanguageServiceModel> AllLanguages();
        IEnumerable<BookConditionServiceModel> AllConditions();
        IEnumerable<TownServiceModel> AllTowns();

        bool IsByKnizhar(int bookId, int knizharId);
        bool GenreExists(int genreId);

        bool LanguageExists(int languageId);

        bool ConditionExists(int conditionId);
        string TheBookIsFor(BookServiceModel bookModel, Book book);
    }
}
