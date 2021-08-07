﻿namespace Knizhar.Controllers
{
    using AutoMapper;
    using Knizhar.Infrastructure;
    using Knizhar.Models.Books;
    using Knizhar.Services.Books;
    using Knizhar.Services.Books.Models;
    using Knizhar.Services.Knizhari;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    public class BooksController : Controller
    {
        private readonly IBookService books;
        private readonly IKnizharService knizhari;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment environment;

        public BooksController(
            IKnizharService knizhari,
            IBookService books,
            IMapper mapper,
            IWebHostEnvironment environment)
        {
            this.books = books;
            this.knizhari = knizhari;
            this.mapper = mapper;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.knizhari.IsKnizhar(this.User.Id()))
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
            var knizharId = this.knizhari.IdByUser(this.User.Id());

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

            var authorId = this.books.GetAuthor(book.Author).Id;

            this.books.Create(
                book.Isbn,
                book.Name,
                book.GenreId,
                book.LanguageId,
                book.ConditionId,
                book.Image,
                book.Description,
                authorId,
                book.Comment,
                book.IsForGiveAway,
                book.Price = book.Price == 0m ? 00.00m : book.Price,
                knizharId,
                book.ImagePath = $"{this.environment.WebRootPath}/images");

            return RedirectToAction(nameof(All));
        }
        public IActionResult All([FromQuery] BookSearchViewModel search, string imagePath)
        {

            var searchResult = this.books.All(
                search.Genre,
                search.Language,
                search.Town,
                search.SearchTerm,
                search.Sorting,
                search.CurrentPage,
                BookSearchViewModel.BooksPerPage,
                search.ImagePath = $"{this.environment.WebRootPath}/images");

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
        public IActionResult MyBooks(string imagePath)
        {
            imagePath = $"{this.environment.WebRootPath}/images";

            var myBooks = this.books.ByUser(this.User.Id(), imagePath);

            return View(myBooks);
        }

        public IActionResult Details(int id, string information)
        {

            var book = this.books.Details(id);

            if (information != book.GetInformation())
            {
                return BadRequest();
            }
            return View(book);
        }

        public IActionResult Filter([FromQuery] string filterName, string imagePath, BookDetailsModel book)
        {
            imagePath = $"{this.environment.WebRootPath}/images";
            var currentPage = book.CurrentPage;
            var booksPerPage = BookDetailsModel.BooksPerPage;

            var filterResult = this.books.Filter(
                filterName,
                currentPage,
                booksPerPage,
                imagePath);

            return View(filterResult);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.knizhari.IsKnizhar(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(KnizhariController.Create), "Knizhari");
            }

            var book = this.books.Details(id);

            if (book.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var bookForm = this.mapper.Map<BookFormModel>(book);

            bookForm.Genres = this.books.AllGenres();
            bookForm.Languages = this.books.AllLanguages();
            bookForm.Conditions = this.books.AllConditions();
            bookForm.Author = book.AuthorName;

            return View(bookForm);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, BookFormModel book)
        {
            var knizharId = this.knizhari.IdByUser(this.User.Id());

            if (knizharId == 0 && !User.IsAdmin())
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

            if (!this.books.IsByKnizhar(id, knizharId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            var authorId = this.books.GetAuthor(book.Author).Id;

            this.books.Edit(
                id,
                book.Isbn,
                book.Name,
                book.GenreId,
                book.LanguageId,
                book.ConditionId,
                book.Description,
                authorId,
                book.Comment,
                book.IsForGiveAway,
                (decimal)book.Price);

            return RedirectToAction(nameof(All));
        }
    }
}