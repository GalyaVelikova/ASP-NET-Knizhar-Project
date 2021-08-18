namespace Knizhar.Test.Controllers.Pipeline
{
    using Knizhar.Data.Models;
    using Knizhar.Controllers;
    using Knizhar.Models.Knizhari;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using System.Linq;

    using static WebConstants;
    using Knizhar.Models.Books;

    public class KnizharControllerTest
    {
        [Fact]
        public void GetKnizharShouldBeForAuthorizedUsersAndShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Knizhari/Create")
                    .WithUser())
                .To<KnizhariController>(c => c.Create())
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
    }
}
