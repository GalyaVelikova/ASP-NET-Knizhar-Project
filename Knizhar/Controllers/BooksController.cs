namespace Knizhar.Controllers
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Knizhar.Infrastructure;
    using Knizhar.Models.Books;
    using Knizhar.Services.Books;
    using Knizhar.Services.Knizhari;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class BooksController : Controller
    {
        private readonly IBookService books;
        private readonly IKnizharService knizhari;
        private readonly KnizharDbContext data;

        public BooksController(
            KnizharDbContext data,
            IKnizharService knizhari,
            IBookService books)
        {
            this.data = data;
            this.books = books;
            this.knizhari = knizhari;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.knizhari.IsKnizhar(this.User.GetId()))
            {
                return RedirectToAction(nameof(KnizhariController.Create), "Knizhari");
            }

            return View(new BookFormModel
            {
                Genres = this.books.AllGenres(),
                Languages = this.books.AllLanguages(),
                Conditions = this.books.AllConditions(),
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(BookFormModel book)
        {
            var knizharId = this.knizhari.IdByUser(this.User.GetId());

            if (knizharId == 0)
            {
                return RedirectToAction(nameof(KnizhariController.Create), "Knizhari");
            }

            if (!this.books.GenreExists(book.GenreId))
            {
                this.ModelState.AddModelError(nameof(book.GenreId), "Genre does not exist");
            }

            if (!this.books.LanguageExists(book.LanguageId))
            {
                this.ModelState.AddModelError(nameof(book.LanguageId), "Language does not exist");
            }

            if (!this.books.ConditionExists(book.ConditionId))
            {
                this.ModelState.AddModelError(nameof(book.ConditionId), "Pleaese choose condition from the drop down menu.");
            }

            if (!ModelState.IsValid)
            {
                book.Genres = this.books.AllGenres();
                book.Languages = this.books.AllLanguages();
                book.Conditions = this.books.AllConditions();

                return View(book);
            }

            var author = this.books.GetAuthor(book.Author);

            this.books.Create(
                book.Isbn,
                book.Name,
                book.GenreId,
                book.LanguageId,
                book.ConditionId,
                book.Comment,
                book.ImageUrl,
                book.Description,
                book.Author,
                book.IsForGiveAway,
                (decimal)book.Price,
                knizharId);
            

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

            var bookGenres = this.books.AllGenres();
            var bookTowns = this.books.AllTowns();
            var bookLanguages = this.books.AllLanguages();

            search.Genres = bookGenres.Select(g => g.Name);
            search.Languages = bookLanguages.Select(l => l.Name);
            search.Towns = bookTowns.Select(t => t.Name);
            search.TotalBooks = searchResult.TotalBooks;
            search.Books = searchResult.Books;

            return View(search);
        }

        [Authorize]
        public IActionResult MyBooks()
        {
            var myBooks = this.books.ByUser(this.User.GetId());

            return View(myBooks);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.knizhari.IsKnizhar(this.User.GetId()))
            {
                return RedirectToAction(nameof(KnizhariController.Create), "Knizhari");
            }

            var book = this.books.Details(id);

            if (book.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new BookFormModel
            {
                Isbn = book.Isbn,
                Name = book.Name,
                Author = book.Author,
                GenreId = book.GenreId,
                LanguageId = book.LanguageId,
                ConditionId = book.ConditioId,
                Comment = book.Comment,
                ImageUrl = book.ImageUrl,
                Description = book.Description,
                IsForGiveAway = book.IsForGiveAway,
                Price = book.Price,
                Genres = this.books.AllGenres(),
                Languages = this.books.AllLanguages(),
                Conditions = this.books.AllConditions(),
            });
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, BookFormModel book)
        {
            var knizharId = this.knizhari.IdByUser(this.User.GetId());

            if (knizharId == 0)
            {
                return RedirectToAction(nameof(KnizhariController.Create), "Knizhari");
            }

            if (!this.books.GenreExists(book.GenreId))
            {
                this.ModelState.AddModelError(nameof(book.GenreId), "Genre does not exist");
            }

            if (!this.books.LanguageExists(book.LanguageId))
            {
                this.ModelState.AddModelError(nameof(book.LanguageId), "Language does not exist");
            }

            if (!this.books.ConditionExists(book.ConditionId))
            {
                this.ModelState.AddModelError(nameof(book.ConditionId), "Pleaese choose condition from the drop down menu.");
            }

            if (!ModelState.IsValid)
            {
                book.Genres = this.books.AllGenres();
                book.Languages = this.books.AllLanguages();
                book.Conditions = this.books.AllConditions();

                return View(book);
            }

            if (!this.books.IsByKnizhar(id, knizharId))
            {
                return BadRequest();
            }
            var author = this.books.GetAuthor(book.Author);

            this.books.Edit(
                id,
                book.Isbn,
                book.Name,
                book.GenreId,
                book.LanguageId,
                book.ConditionId,
                book.ImageUrl,
                book.Description,
                book.Author,
                book.Comment,
                book.IsForGiveAway,
                (decimal)book.Price);

            return RedirectToAction(nameof(All));
        }
    }
}
