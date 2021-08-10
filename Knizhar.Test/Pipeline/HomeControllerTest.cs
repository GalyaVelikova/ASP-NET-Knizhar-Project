namespace Knizhar.Test.Controllers.Pipeline
{
    using Knizhar.Controllers;
    using Knizhar.Services.Books.Models;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Xunit;
    using static Data.Books;
    using static WebConstants.Cache;

    public class HomeControllerTest
    {
        //[Fact]
        //public void IndexShouldReturnViewWithCorrectModelAndData()
        // => MyMvc
        //    .Pipeline()
        //    .ShouldMap("/")
        //    .To<HomeController>(c => c.Index(bookmodel))
        //    .Which(controller => controller
        //    .WithData(TenPublicBooks()))
        //    .ShouldHave()
        //    .MemoryCache(cache => cache
        //        .ContainingEntryWithKey(LatestBooksCacheKey)
        //        .ContainingEntryWithKey(TotalStatisticsCacheKey))
        //    .AndAlso()
        //    .ShouldReturn()
        //    .View(view => view
        //        .WithModelOfType<IEnumerable<BookServiceModel>>()
        //        .Passing(m => m.Should().HaveCount(3)));

        [Fact]
        public void ErrorShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error())
                .Which()
                .ShouldReturn()
                .View();
    }
}
