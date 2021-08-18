namespace Knizhar.Test.Controller
{
    using FluentAssertions;
    using Knizhar.Controllers;
    using Knizhar.Services.Books.Models;
    using MyTested.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using Xunit;

    using static Data.Books;
    using static WebConstants.Cache;
    public class HomeControllerTests
    {
      
        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();

    }
}
