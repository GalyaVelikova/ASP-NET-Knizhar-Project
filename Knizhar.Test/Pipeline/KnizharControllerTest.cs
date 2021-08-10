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

        [Theory]
        [InlineData("Knizhar", 1)]
        public void PostCreateShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string userName,
            int townId)
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Knizhari/Create")
                    .WithMethod(HttpMethod.Post)
                    .WithFormFields(new
                    {
                        UserName = userName,
                        TownId = townId
                    })
                    .WithUser()
                    .WithAntiForgeryToken())
                .To<KnizhariController>(c => c.Create(new BecomeKnizharFormModel
                {
                    UserName = userName,
                    TownId = townId
                }))
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Knizhar>(knizhari =>
                    {
                        knizhari.Any(k =>
                            k.UserName == userName &&
                            k.TownId == townId &&
                            k.UserId == TestUser.Identifier);
                    }))
                .TempData(tempdata => tempdata
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<BooksController>(c => c.All(With.Any<BookSearchViewModel>())));

    }
}
