namespace Knizhar.Test.Controller
{
    using Knizhar.Controllers;
    using Knizhar.Data.Models;
    using Knizhar.Models.Books;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;
    using static Data.Books;
    public class BooksControllerTest
    {
        [Theory]
        [InlineData(1)]
        public void AddGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUsersAndShouldReturnView(int knizharId)
            => MyController<BooksController>
                .Instance()
                .WithUser(TestUser.Identifier)
                .WithData(GetKnizhar("userName", 1, 1, TestUser.Identifier, knizharId))
                .Calling(c => c.Add(With.Empty<BookFormModel>()))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Knizhar>(knizhari =>
                    {
                        knizhari.Any(k => k.Id == knizharId);
                    }))
                .ActionAttributes(attrs => attrs
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void AddPostShouldHaveRestrictionsForAuthorizedUsers()
           => MyController<BooksController>
               .Instance()
                .WithUser(user => user.WithIdentifier("1"))
               .Calling(c => c.Add(With.Empty<BookFormModel>()))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForHttpMethod(HttpMethod.Post)
                   .RestrictingForAuthorizedRequests());
        
        [Fact]
        public void DeleteShouldHaveRestrictionsForAuthorizedUsers()
           => MyController<BooksController>
               .Instance()
               .WithUser(TestUser.Identifier)
               .WithData(GetKnizhar("userName", 1, 1, TestUser.Identifier, 1))
               .Calling(c => c.Delete(With.Empty<int>()))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForAuthorizedRequests());

        [Fact]
        public void DeleteShouldReturnNotFoundWhenInvalidId()
            => MyController<BooksController>
               .Instance()
               .WithUser(TestUser.Identifier)
               .WithData(GetKnizhar("userName", 1, 1, TestUser.Identifier, 1))
                .Calling(c => c.Delete(With.Any<int>()))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void ArchiveShouldHaveRestrictionsForAuthorizedUsers()
         => MyController<BooksController>
             .Instance()
             .WithUser(TestUser.Identifier)
             .WithData(GetKnizhar("userName", 1, 1, TestUser.Identifier, 1))
             .Calling(c => c.Archive(With.Empty<int>()))
             .ShouldHave()
             .ActionAttributes(attrs => attrs
                 .RestrictingForAuthorizedRequests());


        [Fact]
        public void ArchiveShouldReturnNotFoundWhenInvalidId()
            => MyController<BooksController>
               .Instance()
               .WithUser(TestUser.Identifier)
               .WithData(GetKnizhar("userName", 1, 1, TestUser.Identifier, 1))
                .Calling(c => c.Archive(With.Any<int>()))
                .ShouldReturn()
                .BadRequest();
    }
}
