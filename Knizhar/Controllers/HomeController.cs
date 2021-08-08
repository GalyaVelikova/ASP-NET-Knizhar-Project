namespace Knizhar.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Knizhar.Data;
    using Knizhar.Models;
    using Knizhar.Models.Home;
    using Knizhar.Services;
    using Knizhar.Services.Books.Models;
    using Knizhar.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly KnizharDbContext data;
        private readonly IConfigurationProvider mapper;
        private readonly IMemoryCache cache;

        public HomeController(
            IStatisticsService statistics,
            KnizharDbContext data,
            IMapper mapper,
            IMemoryCache cache)
        {
            this.statistics = statistics;
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.cache = cache;
        }

        public IActionResult Index(BookServiceModel bookModel)
        {
            const string latestBooksCacheKey = "LatestBooksCacheKey";
            const string totalStatisticsCacheKey = "TotalStatisticsCacheKey";

            var latestBooks = this.cache.Get<List<BookServiceModel>>(latestBooksCacheKey);

            var booksOrderedByDateAdded = this.data.Books.Where(b => b.isPublic).OrderByDescending(b => b.AddedOn).AsQueryable();

            if (latestBooks == null)
            {
                latestBooks = booksOrderedByDateAdded
                .ProjectTo<BookServiceModel>(this.mapper)
                .Take(12)
                .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestBooksCacheKey, latestBooks, cacheOptions);
            }

            var totalStatistics = this.cache.Get<StatisticsServiceModel>(totalStatisticsCacheKey);
            
            if (totalStatistics == null)
            {
                totalStatistics = this.statistics.Total();

                var cacheOptions = new MemoryCacheEntryOptions()
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(totalStatisticsCacheKey, totalStatistics, cacheOptions);
            }
            return View(new IndexViewModel
            {
                TotalBooks = totalStatistics.TotalBooks,
                TotalKnizhari = totalStatistics.TotalKnizhari,
                RecentlyAddedBooks = latestBooks
            } );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    }
}
