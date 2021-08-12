namespace Knizhar.Test.Controller
{
    using Knizhar.Controllers;
    using Knizhar.Data.Models;
    using Knizhar.Models.Books;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;
    using static WebConstants;
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

        [Theory]
        [InlineData(1)]
        public void AddPostShouldReturnViewWithSameModelWhenInvalidModelState(int knizharId)
           => MyController<BooksController>
               .Instance()
               .WithUser(TestUser.Identifier)
               .WithData(GetKnizhar("userName", 1, 1, TestUser.Identifier, knizharId))
               .Calling(c => c.Add(With.Default<BookFormModel>()))
               .ShouldHave()
               .Data(data => data
                    .WithSet<Knizhar>(knizhari =>
                    {
                        knizhari.Any(k => k.Id == knizharId);
                    }))
               .InvalidModelState()
               .AndAlso()
               .ShouldReturn()
               .View(result => result
                    .WithModelOfType<BookFormModel>());
                   // .Passing(book => book == null);

        //[Theory]
        //[InlineData(1, "AuthorName", 1, 1, ".jpg", 1, "1234567891", "Book Name", 1, 1, 1, "1", "Description for the book.", "Comment for the book", true,  0, 1, "wwwroot/images/books")]
        //public void AddPostShouldSaveArticleSetModelStateMessageAndRedirectWhenValidModelState(
        //    int authorId, 
        //    string authorName, 
        //    int addedByKnizharId, 
        //    int bookId, 
        //    string extension, 
        //    int count, 
        //    string isbn, 
        //    string bookName, 
        //    int genreId, 
        //    int languageId, 
        //    int conditionId, 
        //    string imageId, 
        //    string description, 
        //    string comment, 
        //    bool isForGiveAway, 
        //    decimal price, 
        //    int knizharId,
        //    string imagePath)
        //   => MyController<BooksController>
        //       .Instance()
        //       .WithUser(TestUser.Identifier)
        //       .WithData(GetKnizhar("userName", 1, 1, TestUser.Identifier, 1))
        //       .WithData(GetImage("1",1,1,".jpg",1))
        //       .WithData(GetAuthor(1, "AuthorName"))
        //        .Calling(c => c.Add(new BookFormModel
        //        {
        //            Isbn = isbn,
        //            Name = bookName,
        //            AuthorName = authorName,
        //            GenreId = genreId,
        //            LanguageId = languageId,
        //            ConditionId = conditionId,
        //            Comment = comment,
        //            Image = CreateTestFormFile("1.jpg", "Hello"),
        //            ImagePath = imagePath,
        //            ImageId = imageId,
        //            Description = description,
        //            IsForGiveAway = isForGiveAway,
        //            Price = price
        //        }))
        //       .ShouldHave()
        //       .Data(data => data
        //            .WithSet<Knizhar>(knizhari =>
        //            {
        //                knizhari.Any(k => k.Id == knizharId);
        //            }))
        //        .Data(data => data
        //            .WithSet<Image>(image =>
        //            {
        //                image.Any(i => i.Id == imageId &&
        //                               i.Extension == extension &&
        //                               i.BookId == bookId);
        //            }))
        //        .Data(data => data
        //                   .WithSet<Author>(author =>
        //                   {
        //                       author.Any(a => a.Name == authorName);
        //                   }))
        //       .ValidModelState()
        //       .AndAlso()
        //       .ShouldHave()
        //       .Data(data => data
        //            .WithSet<Book>(books =>
        //            {
        //                books.Any(b =>
        //                    b.Isbn == isbn &&
        //                    b.Name == bookName &&
        //                    b.Author.Name == authorName &&
        //                    b.GenreId == genreId &&
        //                    b.LanguageId == languageId &&
        //                    b.ConditionId == conditionId &&
        //                    b.Comment == comment &&
        //                    b.ImageId == imageId &&
        //                    b.Description == description &&
        //                    b.IsForGiveAway == isForGiveAway &&
        //                    b.Price == price );
                                                
        //           }))
        //       .AndAlso()
        //       .ShouldHave()
        //       .TempData(tempdata => tempdata
        //            .ContainingEntryWithKey(GlobalMessageKey))
        //       .AndAlso()
        //       .ShouldReturn()
        //       .Redirect(redirect => redirect
        //           .To<BooksController>(c => c.Details(bookId, $"{bookName}-{authorName}")));

        //[Theory]
        //[InlineData(8, 1, 8)]
        //[InlineData(18, 1, ServicesConstants.ArticlesPerPage)]
        //[InlineData(18, 2, 6)]
        //public void AllShouldReturnDefaultViewWithCorrectModel(int total, int page, int expectedCount)
        //   => MyController<BooksController>
        //       .Instance()
        //       .WithData(ArticleTestData.GetArticles(total))
        //       .Calling(c => c.All(page))
        //       .ShouldReturn()
        //       .View(view => view
        //           .WithModelOfType<ArticleListingViewModel>()
        //           .Passing(articleListing =>
        //           {
        //               articleListing.Articles.Count().ShouldBe(expectedCount);
        //               articleListing.Total.ShouldBe(total);
        //               articleListing.Page.ShouldBe(page);
        //           }));

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

        //[Fact]
        //public void DeleteShouldReturnNotFoundWhenNonAuthorUser()
        //    => MyController<BooksController>
        //        .Instance(instance => instance
        //            .WithUser(user => user.WithIdentifier("NonAuthor"))
        //            .WithData(GetBook(1)))
        //        .Calling(c => c.Delete(1))
        //        .ShouldReturn()
        //        .NotFound();

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
