namespace Knizhar.Controllers
{
    using Knizhar.Models;
    using Knizhar.Models.Home;
    using Knizhar.Services;
    using Knizhar.Services.Books;
    using Knizhar.Services.Books.Models;
    using Knizhar.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using static WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IBookService books;
        private readonly IMemoryCache cache;

        public HomeController(
            IStatisticsService statistics,
            IBookService books,
            IMemoryCache cache)
        {
            this.statistics = statistics;
            this.books = books;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var latestBooks = this.cache.Get<List<BookServiceModel>>(LatestBooksCacheKey);

            if (latestBooks == null)
            {
                latestBooks = this.books.Latest();
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(LatestBooksCacheKey, latestBooks, cacheOptions);
            }

            var totalStatistics = this.cache.Get<StatisticsServiceModel>(TotalStatisticsCacheKey);
            
            if (totalStatistics == null)
            {
                totalStatistics = this.statistics.Total();

                var cacheOptions = new MemoryCacheEntryOptions()
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(TotalStatisticsCacheKey, totalStatistics, cacheOptions);
            }

            return View(new IndexViewModel
            {
                TotalBooks = totalStatistics.TotalBooks,
                TotalKnizhari = totalStatistics.TotalKnizhari,
                RecentlyAddedBooks = latestBooks
            }) ;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    }
}
