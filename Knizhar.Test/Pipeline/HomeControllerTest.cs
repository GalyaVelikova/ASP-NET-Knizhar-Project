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
