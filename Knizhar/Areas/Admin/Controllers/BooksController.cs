namespace Knizhar.Areas.Admin.Controllers
{
    using Knizhar.Models.Books;
    using Knizhar.Services.Books;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    public class BooksController : AdminController
    {
        private readonly IBookService books;
        private readonly IWebHostEnvironment environment;
        public BooksController(
            IBookService books,
            IWebHostEnvironment environment)
        {
            this.books = books;
            this.environment = environment;
        }

        public IActionResult All(BookSearchViewModel search)
        {
            var books = this.books.All(
                 search.ImagePath = $"{this.environment.WebRootPath}/images",
                 publicOnly: false).Books;

            return View(books);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.books.ChnageVisiblity(id);

            return RedirectToAction(nameof(All));
        }
    }
}
