namespace Knizhar.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Knizhar.Models;
    using Knizhar.Models.Home;
    using Knizhar.Services;
    using Knizhar.Services.Books.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly KnizharDbContext data;
        private readonly IMapper mapper;

        public HomeController(
            IStatisticsService statistics,
            KnizharDbContext data,
            IMapper mapper)
        {
            this.statistics = statistics;
            this.data = data;
            this.mapper = mapper;
        }

        public IActionResult Index(BookServiceModel bookModel)
        {
            var booksOrderedByDateAdded = this.data.Books.OrderByDescending(b => b.AddedOn).AsQueryable();
            
                var newlyAddedBooks = booksOrderedByDateAdded
                .ProjectTo<BookServiceModel>(this.mapper.ConfigurationProvider)
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
