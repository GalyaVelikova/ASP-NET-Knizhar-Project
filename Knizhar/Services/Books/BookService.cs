namespace Knizhar.Services.Books
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Knizhar.Models.Books;
    using Knizhar.Services.Books.Models;
    using Knizhar.Services.Knizhari;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class BookService : IBookService
    {
        private readonly KnizharDbContext data;
        private readonly IConfigurationProvider mapper;

        public BookService(
            KnizharDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public BookSearchServiceModel All(
            string imagePath,
            string genre = null,
            string town = null,
            string language = null,
            string searchTerm = null,
            BookSorting sorting = BookSorting.Newest,
            int currentPage = 1,
            int booksPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var booksQuery = this.data.Books
                .Where(b => (!publicOnly || b.IsPublic && !b.IsArchived))
                .AsQueryable();

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
                .Skip((currentPage - 1) * booksPerPage)
                .Take(booksPerPage), imagePath);

            return new BookSearchServiceModel
            {
                TotalBooks = totalBooks,
                CurrentPage = currentPage,
                Books = books,
            };
        }

        public IEnumerable<BookServiceModel> ByUser(string userId, string imagePath)
        {

            var booksByUser = GetBooks(this.data
                .Books
                .Where(b => b.Knizhar.UserId == userId), imagePath);

            return booksByUser;
        }
        public bool IsByKnizhar(int bookId, int knizharId)
            => this.data
                .Books
                .Any(b => b.Id == bookId && b.KnizharId == knizharId);
        private IEnumerable<BookServiceModel> GetBooks(IQueryable<Book> bookQuery, string imagePath)
            => bookQuery
                .ProjectTo<BookServiceModel>(this.mapper)
                .ToList();

        public IEnumerable<BookGenreServiceModel> AllGenres()
            => this.data
                    .Genres
                    .ProjectTo<BookGenreServiceModel>(this.mapper)
                    .OrderBy(g => g.Name)
                    .ToList();

        public IEnumerable<BookLanguageServiceModel> AllLanguages()
             => this.data
                    .Languages
                    .ProjectTo<BookLanguageServiceModel>(this.mapper)
                    .OrderBy(l => l.Name)
                    .ToList();

        public IEnumerable<BookConditionServiceModel> AllConditions()
            => this.data
                    .Conditions
                    .ProjectTo<BookConditionServiceModel>(this.mapper)
                    .OrderBy(bc => bc.Name)
                    .ToList();

        public IEnumerable<TownServiceModel> AllTowns()
        => this.data
                    .Towns
                    .ProjectTo<TownServiceModel>(this.mapper)
                    .OrderBy(b => b.Name)
                    .ToList();

        public BookDetailsModel Details(int id)
            =>this.data
                  .Books
                  .Where(b => b.Id == id)
                  .ProjectTo<BookDetailsModel>(this.mapper)
                  //.Select(b => new BookDetailsServiceModel
                  //{
                  //    Name = b.Name,
                  //    ImageUrl = b.ImageUrl,
                  //    AuthorName = b.Author.Name,
                  //    Isbn = b.Isbn,
                  //    Language = b.Language.LanguageName,
                  //    Genre = b.Genre.Name,
                  //    Description = b.Description,
                  //    Condition = b.Condition.ConditionName,
                  //    Comment = b.Comment,
                  //    IsForGiveAway = b.IsForGiveAway,
                  //    Price = b.Price == 0 ? 00.00m : b.Price,
                  //    KnizharId = b.KnizharId,
                  //    KnizharName = b.Knizhar.UserName,
                  //    UserId = b.Knizhar.UserId

                  //})
                  .FirstOrDefault();

        public BookSearchServiceModel Filter(
             BookDetailsModel book,
             int currentPage,
             int booksPerPage,
             string imagePath)
        {
            var booksQuery = this.data.Books.Where(b => !b.IsArchived).AsQueryable();

            if (this.data.Genres.Any(g => g.Name == book.GenreName))
            {
                booksQuery = booksQuery.Where(b => b.Genre.Name == book.GenreName);
            }

            if (this.data.Towns.Any(t => t.Name == book.TownName))
            {
                booksQuery = booksQuery.Where(b => b.Knizhar.Town.Name == book.TownName);
            }

            if (this.data.Languages.Any(l => l.LanguageName == book.LanguageName))
            {
                booksQuery = booksQuery.Where(b => b.Language.LanguageName == book.LanguageName);
            }

            if (this.data.Authors.Any(a => a.Name == book.AuthorName))
            {
                booksQuery = booksQuery.Where(b => b.Author.Name == book.AuthorName);
            }

            var totalBooks = booksQuery.Count();

            var books = GetBooks(booksQuery
                .Skip((currentPage - 1) * booksPerPage)
                .Take(booksPerPage), imagePath);

            return new BookSearchServiceModel
            {
                TotalBooks = totalBooks,
                CurrentPage = currentPage,
                Books = books,
            };
        }
        public bool GenreExists(int genreId)
            => this.data.Genres.Any(g => g.Id == genreId);
        public bool LanguageExists(int languageId)
            => this.data.Languages.Any(l => l.Id == languageId);

        public bool ConditionExists(int conditionId)
            => this.data.Conditions.Any(c => c.Id == conditionId);

        public Author GetAuthor(string author)
        {
            var authorData = data.Authors.FirstOrDefault(a => a.Name == author);
            if (authorData == null)
            {
                authorData = new Author { Name = author };

                this.data.Authors.Add(authorData);
                this.data.SaveChanges();
            }

            return authorData;
        }

        public int Create(string isbn, string name, int genreId, int languageId, int conditionId, IFormFile image, string description, int author, string comment, bool isForGiveAway, decimal price, int knizharId, string imagePath)
        {

            Directory.CreateDirectory($"{imagePath}/books/");

            var imageData = new Image
            {
                AddedByKnizharId = knizharId,
                Extension = Path.GetExtension(image.FileName).TrimStart('.')
            };

            var physicalPath = $"{imagePath}/books/{imageData.Id}.{imageData.Extension}";

            using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            this.data.Images.Add(imageData);
            this.data.SaveChanges();

            var bookData = new Book
            {
                Isbn = isbn,
                Name = name,
                GenreId = genreId,
                LanguageId = languageId,
                ConditionId = conditionId,
                Description = description,
                AuthorId = author,
                Comment = comment,
                ImageId = imageData.Id,
                IsForGiveAway = isForGiveAway,
                Price = price,
                AddedOn = DateTime.UtcNow,
                Favourite = false,
                IsArchived = false,
                IsPublic = false,
                KnizharId = knizharId,
            };

            this.data.Books.Add(bookData);
            this.data.SaveChanges();

            return bookData.Id;
        }

        public bool Edit(
            int id, 
            string isbn, 
            string name, 
            int genreId, 
            int languageId, 
            int conditionId, 
            string description, 
            int author, 
            string comment, 
            bool isForGiveAway, 
            decimal price,
            bool isPublic)
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
            bookData.Description = description;
            bookData.AuthorId = author;
            bookData.Comment = comment;
            bookData.IsForGiveAway = isForGiveAway;
            bookData.Price = price == 0m ? 00.00m : price;
            bookData.IsPublic = false;

            this.data.SaveChanges();

            return true;
        }

        public string TheBookIsFor(BookServiceModel bookModel, Book book)
        {
            if (book.IsForGiveAway)
            {
                return "Give Away";
            }

            return "Exchange";
        }

        public void ChnageVisiblity(int carId)
        {
            var book = this.data.Books.Find(carId);

            book.IsPublic = !book.IsPublic;

            this.data.SaveChanges();
        }

        public List<BookServiceModel> Latest()
            =>this.data
                .Books
                .Where(b => b.IsPublic && !b.IsArchived)
                .OrderByDescending(b => b.AddedOn)
                .ProjectTo<BookServiceModel>(this.mapper)
                .Take(12)
                .ToList();

        public bool Delete(int bookId)
        {
            var book = this.data.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                this.data.Books.Remove(book);
                this.data.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Archive(int bookId)
        {
            var book = this.data.Books.FirstOrDefault(b => b.Id == bookId);

            if (book != null)
            {
                book.IsArchived = true;

                this.data.SaveChanges();
                return true;
            }

            return false;

        }
    }
}