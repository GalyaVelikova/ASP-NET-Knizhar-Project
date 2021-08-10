namespace Knizhar.Test.Controller
{
    using Knizhar.Data.Models;
    using Knizhar.Controllers;
    using Knizhar.Models.Knizhari;
    using Knizhar.Models.Books;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using System.Linq;

    using static WebConstants;
    public class KnizhariControllerTest
    {
        [Fact]
        public void GetCreateShouldBeForAuthorizedUsersAndReturnView()
            => MyController<KnizhariController>
                .Instance()
                .Calling(c => c.Create())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void GetCtreateShouldReturnView()
            => MyController<KnizhariController>
                .Instance()
                .Calling(c => c.Create())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<BecomeKnizharFormModel>());

        [Theory]
        [InlineData("Knizhar", 1)]
        public void PostCreateShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string userName,
            int townId)
            => MyController<KnizhariController>
                .Instance(instance => instance
                    .WithUser())
                .Calling(c => c.Create(new BecomeKnizharFormModel
                {
                    UserName = userName,
                    TownId = townId
                }))
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
