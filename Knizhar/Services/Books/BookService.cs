namespace Knizhar.Services.Books
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Knizhar.Models.Books;
    using Knizhar.Services.Knizhari;
    using System;
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

            var books = GetBooks(booksQuery
                .Skip((currentPage-1) * booksPerPage)
                .Take(booksPerPage));

            return new BookSearchServiceModel
            {
                TotalBooks = totalBooks,
                CurrentPage = currentPage,
                Books = books,
            };
        }

        public IEnumerable<BookServiceModel> ByUser(string userId)
            => GetBooks(this.data
                .Books
                .Where(b => b.Knizhar.UserId == userId));
        public bool IsByKnizhar(int bookId, int knizharId)
            => this.data
                .Books
                .Any(b => b.Id == bookId && b.KnizharId == knizharId);
        private static IEnumerable<BookServiceModel> GetBooks(IQueryable<Book> bookQuery)
            => bookQuery
                .Select(b => new BookServiceModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                    Author = b.Author.Name,
                    TheBookIsFor = b.IsForGiveAway ? "Give away" : "Exchange",
                    Price = (decimal)b.Price,
                })
                .ToList();

        public IEnumerable<BookGenreServiceModel> AllGenres()
            => this.data
                    .Genres
                    .Select(b => new BookGenreServiceModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                    })
                    .ToList();

        public IEnumerable<BookLanguageServiceModel> AllLanguages()
             => this.data
                    .Languages
                    .Select(l => new BookLanguageServiceModel
                    {
                        Id = l.Id,
                        Name = l.LanguageName,
                    })
                    .ToList();

        public IEnumerable<BookConditionServiceModel> AllConditions()
            => this.data
                    .Conditions
                    .Select(b => new BookConditionServiceModel
                    {
                        Id = b.Id,
                        Name = b.ConditionName,
                    })
                    .ToList();

        public IEnumerable<TownServiceModel> AllTowns()
        => this.data
                    .Towns
                    .Select(b => new TownServiceModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                    })
                    .ToList();

        public BookDetailsServiceModel Details(int id)
            => this.data
                .Books
                .Where(b => b.Id == id)
                .Select(b => new BookDetailsServiceModel
                {
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                    Author = b.Author.Name,
                    Isbn = b.Isbn,
                    Language = b.Language.LanguageName,
                    Genre = b.Genre.Name,
                    Description = b.Description,
                    Condition = b.Condition.ConditionName,
                    Comment = b.Comment,
                    IsForGiveAway = b.IsForGiveAway,
                    Price = b.Price == 0 ? 00.00m : b.Price,
                    KnizharId = b.KnizharId,
                    KnizharName = b.Knizhar.UserName,
                    UserId = b.Knizhar.UserId

                })
                .FirstOrDefault();

        public bool GenreExists(int genreId)
            => this.data.Genres.Any(g => g.Id == genreId);
        public bool LanguageExists(int languageId)
            => this.data.Languages.Any(l => l.Id == languageId);

        public bool ConditionExists(int conditionId)
            => this.data.Conditions.Any(c => c.Id == conditionId);

        public Author GetAuthor(string authorName)
        {
            var author = data.Authors.FirstOrDefault(a => a.Name == authorName);
            if (author == null)
            {
                author = new Author { Name = authorName };

                this.data.Authors.Add(author);
                this.data.SaveChanges();
            }

            return author;
        }

        public int Create(string isbn, string name, int genreId, int languageId, int conditionId, string imageUrl, string description, string author, string comment, bool isForGiveAway, decimal price, int knizharId)
        {
            var bookData = new Book
            {
                Isbn = isbn,
                Name = name,
                GenreId = genreId,
                LanguageId = languageId,
                ConditionId = conditionId,
                Comment = comment,
                ImageUrl = imageUrl,
                Description = description,
                Author = GetAuthor(author),
                IsForGiveAway = isForGiveAway,
                Price = price == 0m ? 00.00m : price,
                AddedOn = DateTime.UtcNow,
                Favourite = false,
                IsArchived = false,
                KnizharId = knizharId,
            };

            this.data.Books.Add(bookData);
            this.data.SaveChanges();

            return bookData.Id;
        }

        public bool Edit(int id, string isbn, string name, int genreId, int languageId, int conditionId, string imageUrl, string description, string author, string comment, bool isForGiveAway, decimal price)
        {
            var bookData = this.data.Books.Find(id);

            if (bookData == null)
            {
                return false;
            }

            bookData.Isbn = isbn;
            bookData.Name = name;
            bookData.GenreId = genreId;
            bookData.LanguageId = languageId;
            bookData.ConditionId = conditionId;
            bookData.ImageUrl = imageUrl;
            bookData.Description = description;
            bookData.Author = GetAuthor(author);
            bookData.Comment = comment;
            bookData.IsForGiveAway = isForGiveAway;
            bookData.Price = price == 0m ? 00.00m : price;

            this.data.SaveChanges();

            return true;
        }

    }


}
