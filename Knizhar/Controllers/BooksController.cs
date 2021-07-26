namespace Knizhar.Controllers
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Knizhar.Infrastructure;
    using Knizhar.Models.Books;
    using Knizhar.Services.Books;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class BooksController : Controller
    {
        private readonly IBookService books;
        private readonly KnizharDbContext data;

        public BooksController(
            KnizharDbContext data,
            IBookService books)
        {
            this.data = data;
            this.books = books;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsKnizhar())
            {
                return RedirectToAction(nameof(KnizhariController.Create), "Knizhari");
            }

            return View(new AddBookFormModel
            {
                Genres = this.GetBookGenres(),
                Languages = this.GetBookLanguages(),
                Conditions = this.GetBookConditions(),
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddBookFormModel book)
        {
            var knizharId = this.data
                .Knizhari
                .Where(k => k.UserId == this.User.GetId())
                .Select(k => k.Id)
                .FirstOrDefault();

            if (knizharId == 0)
            {
                return RedirectToAction(nameof(KnizhariController.Create), "Knizhari");
            }

            if (!this.data.Genres.Any(g => g.Id == book.GenreId))
            {
                this.ModelState.AddModelError(nameof(book.GenreId), "Genre does not exist");
            }

            if (!this.data.Languages.Any(l => l.Id == book.LanguageId))
            {
                this.ModelState.AddModelError(nameof(book.LanguageId), "Language does not exist");
            }

            if (!this.data.Conditions.Any(c => c.Id == book.ConditionId))
            {
                this.ModelState.AddModelError(nameof(book.ConditionId), "Pleaese choose condition from the drop down menu.");
            }

            if (!ModelState.IsValid)
            {
                book.Genres = this.GetBookGenres();
                book.Languages = this.GetBookLanguages();
                book.Conditions = this.GetBookConditions();

                return View(book);
            }

            var author = data.Authors.FirstOrDefault(a => a.Name == book.Author);
            if (author == null)
            {
                author = new Author { Name = book.Author };
                this.data.Authors.Add(author);

                this.data.SaveChanges();
            }

            var bookData = new Book
            {
                Isbn = book.Isbn,
                Name = book.Name,
                GenreId = book.GenreId,
                LanguageId = book.LanguageId,
                ConditionId = book.ConditionId,
                ImageUrl = book.ImageUrl,
                Description = book.Description,
                Author = author,
                IsForGiveAway = book.IsForGiveAway,
                Price = book.Price == null ? 00.00m : book.Price,
                AddedOn = DateTime.UtcNow,
                Favourite = false,
                IsArchived = false,
                KnizharId = knizharId,
            };

            this.data.Books.Add(bookData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery]BookSearchViewModel search)
        {

            var searchResult = this.books.All(
                search.Genre,
                search.Language,
                search.Town,
                search.SearchTerm,
                search.Sorting,
                search.CurrentPage,
                BookSearchViewModel.BooksPerPage);

            var bookGenres = this.books.AllBookGenres();
            var bookTowns = this.books.AllBookTowns();
            var bookLanguages = this.books.AllBookLanguages();

            search.Genres = bookGenres;
            search.Languages = bookLanguages;
            search.Towns = bookTowns;
            search.TotalBooks = searchResult.TotalBooks;
            search.Books = searchResult.Books;

            return View(search);
        }
        private IEnumerable<BookGenreViewModel> GetBookGenres()
         => this.data
                .Genres
                .Select(b => new BookGenreViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToList();

        private IEnumerable<BookLanguageViewModel> GetBookLanguages()
            => this.data
                .Languages
                .Select(l => new BookLanguageViewModel
                {
                    Id = l.Id,
                    Name = l.LanguageName,
                })
                .ToList();

        private IEnumerable<BookConditionViewModel> GetBookConditions()
         => this.data
                .Conditions
                .Select(b => new BookConditionViewModel
                {
                    Id = b.Id,
                    Name = b.ConditionName,
                })
                .ToList();

        private bool UserIsKnizhar()
            =>this.data
                .Knizhari
                .Any(k => k.UserId == this.User.GetId());
      

    }
}
