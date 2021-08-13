namespace Knizhar.Areas.Admin.Controllers
{
    using Knizhar.Data;
    using Knizhar.Models.Books;
    using Knizhar.Services.Books;
    using Knizhar.Services.Books.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class BooksController : AdminController
    {
        private readonly KnizharDbContext data;
        private readonly IBookService books;
        private readonly IWebHostEnvironment environment;
        public BooksController(
            KnizharDbContext data,
            IBookService books,
            IWebHostEnvironment environment)
        {
            this.data = data;
            this.books = books;
            this.environment = environment;
        }

        public IActionResult All(BookSearchViewModel search)
        {
            var books = this.books.All(
                 search.ImagePath = $"{this.environment.WebRootPath}/images",
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
    }
}
