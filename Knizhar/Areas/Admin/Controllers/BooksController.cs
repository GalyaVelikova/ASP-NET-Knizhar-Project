namespace Knizhar.Areas.Admin.Controllers
{
    using Knizhar.Areas.Admin.Models;
    using Knizhar.Areas.Admin.Services;
    using Knizhar.Data;
    using Knizhar.Models.Books;
    using Knizhar.Services.Books;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    public class BooksController : AdminController
    {
        private readonly KnizharDbContext data;
        private readonly IBookService books;
        private readonly ICategoriesService categories;
        private readonly IWebHostEnvironment environment;

        public BooksController(
            KnizharDbContext data,
            IBookService books,
            ICategoriesService categories,
            IWebHostEnvironment environment)
        {
            this.data = data;
            this.books = books;
            this.categories = categories;
            this.environment = environment;
        }

        public IActionResult All(BookSearchViewModel search)
        {
            var books = this.books.All(
                search.ImagePath = $"{this.environment.WebRootPath}/images",
                search.Genre,
                search.Town,
                search.Language,
                search.SearchTerm,
                search.Knizhar,
                search.Sorting,
                search.CurrentPage,
                BookSearchViewModel.BooksPerPage,
                 publicOnly: false);

            search.TotalBooks = books.TotalBooks;
            search.Books = books.Books;

            return View(search);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.books.ChnageVisiblity(id);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Condition() => View();

        [HttpPost]
        public IActionResult AddCondition(ConditionFormModel condition)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Condition));
            }

            var result = this.categories.AddCondition(condition.Name);

            if (!result)
            {
                TempData[GlobalMessageKey] = "This condition is already in the list with conditions. You can not add a condition name twice.";
            }
            else
            {
                TempData[GlobalMessageKey] = "The condition was added succesfully.";
            }
            return RedirectToAction(nameof(Condition));
        }

        [HttpPost]
        public IActionResult DeleteCondition(ConditionFormModel condition)
        {
            var result = this.categories.DeleteCondition(condition.Name);

            if (result)
            {
                TempData[GlobalMessageKey] = $"The condition name was deleted.";
            }
            else
            {
                TempData[GlobalMessageKey] = $"The condition name was not deleted.";
            }

            return RedirectToAction(nameof(Condition));
        }

        public IActionResult Genre() => View();

        [HttpPost]
        public IActionResult AddGenre(GenreFormModel genre)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Genre));
            }

            var result = this.categories.AddGenre(genre.Name);

            if (!result)
            {
                TempData[GlobalMessageKey] = "This genre is already in the list with conditions. You can not add a genre name twice.";
            }
            else
            {
                TempData[GlobalMessageKey] = "The genre was added succesfully.";
            }
            return RedirectToAction(nameof(Genre));
        }

        [HttpPost]
        public IActionResult DeleteGenre(GenreFormModel genre)
        {
            var result = this.categories.DeleteGenre(genre.Name);

            if (result)
            {
                TempData[GlobalMessageKey] = $"The genre name was deleted.";
            }
            else
            {
                TempData[GlobalMessageKey] = $"The genre name was not deleted.";
            }

            return RedirectToAction(nameof(Genre));
        }

        public IActionResult Language() => View();

        [HttpPost]
        public IActionResult AddLanguage(LanguageFormModel language)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Language));
            }

            var result = this.categories.AddLanguage(language.Name);

            if (!result)
            {
                TempData[GlobalMessageKey] = "This language is already in the list with conditions. You can not add a language name twice.";
            }
            else
            {
                TempData[GlobalMessageKey] = "The language was added succesfully.";
            }
            return RedirectToAction(nameof(Language));
        }

        [HttpPost]
        public IActionResult DeleteLanguage(LanguageFormModel language)
        {
            var result = this.categories.DeleteLanguage(language.Name);

            if (result)
            {
                TempData[GlobalMessageKey] = $"The language name was deleted.";
            }
            else
            {
                TempData[GlobalMessageKey] = $"The language name was not deleted.";
            }

            return RedirectToAction(nameof(Language));
        }

        public IActionResult Town() => View();

        [HttpPost]
        public IActionResult AddTown(TownFormModel town)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Town));
            }

            var result = this.categories.AddTown(town.Name);

            if (!result)
            {
                TempData[GlobalMessageKey] = "This town is already in the list with conditions. You can not add a town name twice.";
            }
            else
            {
                TempData[GlobalMessageKey] = "The town was added succesfully.";
            }
            return RedirectToAction(nameof(Town));
        }

        [HttpPost]
        public IActionResult DeleteTown(TownFormModel town)
        {
            var result = this.categories.DeleteTown(town.Name);

            if (result)
            {
                TempData[GlobalMessageKey] = $"The town name was deleted.";
            }
            else
            {
                TempData[GlobalMessageKey] = $"The town name was not deleted.";
            }

            return RedirectToAction(nameof(Town));
        }
    }
}