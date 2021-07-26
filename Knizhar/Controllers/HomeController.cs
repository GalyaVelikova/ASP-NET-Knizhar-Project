namespace Knizhar.Controllers
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Knizhar.Models;
    using Knizhar.Models.Books;
    using Knizhar.Models.Home;
    using Knizhar.Services;
    using Knizhar.Services.Books;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly KnizharDbContext data;

        public HomeController(
            IStatisticsService statistics,
            KnizharDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
        }

        public IActionResult Index(BookServiceModel bookModel)
        {
            var booksOrderedByDateAdded = this.data.Books.OrderByDescending(b => b.AddedOn).AsQueryable();
            
                var newlyAddedBooks = booksOrderedByDateAdded
                .Select(b => new BookServiceModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                    Author = b.Author.Name,
                    TheBookIsFor = TheBookIsFor(bookModel, b),
                    Price = (decimal)b.Price,
                })
                .Take(16)
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalBooks = totalStatistics.TotalBooks,
                TotalKnizhari = totalStatistics.TotalKnizhari,
                RecentlyAddedBooks = newlyAddedBooks
            } );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        private static string TheBookIsFor(BookServiceModel bookModel, Book book)
        {

            if (book.IsForGiveAway)
            {
                return "Give Away";
            }

            return "Exchange";
        }

    }
}
