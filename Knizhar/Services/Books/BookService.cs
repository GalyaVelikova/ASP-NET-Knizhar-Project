namespace Knizhar.Services.Books
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Knizhar.Models.Books;
    using System.Collections.Generic;
    using System.Linq;

    public class BookService : IBookService
    {
        private readonly KnizharDbContext data;

        public BookService(KnizharDbContext data)
        {
            this.data = data;
        }

        public BookSearchServiceModel All(
            string genre,
            string town,
            string language,
            string searchTerm,
            BookSorting sorting,
            int currentPage,
            int booksPerPage)
        {
            var booksQuery = this.data.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(genre))
            {
                booksQuery = booksQuery.Where(b => b.Genre.Name == genre);
            }

            if (!string.IsNullOrWhiteSpace(town))
            {
                booksQuery = booksQuery.Where(b => b.Knizhar.Town.Name == town);
            }

            if (!string.IsNullOrWhiteSpace(language))
            {
                booksQuery = booksQuery.Where(b => b.Language.LanguageName == language);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                booksQuery = booksQuery.Where(b =>

                    b.Isbn.ToLower().Contains(searchTerm.ToLower()) ||
                    b.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    b.Author.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            booksQuery = sorting switch
            {
                BookSorting.ByBookName => booksQuery.OrderBy(b => b.Name),
                BookSorting.ByAuthor => booksQuery.OrderBy(b => b.Author.Name),
                BookSorting.Cheapest => booksQuery
                    .Where(b => b.Price != 0)
                    .OrderBy(b => b.Price),
                BookSorting.MostExpensive => booksQuery
                    .Where(b => b.Price != 0)
                    .OrderByDescending(b => b.Price),
                BookSorting.ForGiveAway => booksQuery.Where(b => b.IsForGiveAway == true),
                BookSorting.Newest or _ => booksQuery.OrderByDescending(b => b.AddedOn)
            };

            var totalBooks = booksQuery.Count();

            var books = booksQuery
                .Skip((currentPage - 1) * booksPerPage)
                .Take(booksPerPage)
                .Select(b => new BookServiceModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                    Author = b.Author.Name,
                    TheBookIsFor = b.IsForGiveAway ? "Give away" :"Exchange",
                    Price = (decimal)b.Price,
                })
                .ToList();

            return new BookSearchServiceModel
            {
                TotalBooks = totalBooks,
                CurrentPage = currentPage,
                Books = books,
            };
        }
        public IEnumerable<string> AllBookGenres()
             => this.data
                .Books
                .Select(b => b.Genre.Name)
                .Distinct()
                .OrderBy(g => g)
                .ToList();

        public IEnumerable<string> AllBookLanguages()
             => this.data
                .Books
                .Select(b => b.Language.LanguageName)
                .Distinct()
                .OrderBy(l => l)
                .ToList();
        public IEnumerable<string> AllBookTowns() 
            => this.data
                .Knizhari
                .Select(k => k.Town.Name)
                .Distinct()
                .OrderBy(t => t)
                .ToList();
    }


}
