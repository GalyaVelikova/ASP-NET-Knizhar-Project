namespace Knizhar.Test.Routing
{
    using Knizhar.Controllers;
    using Knizhar.Data.Models;
    using Knizhar.Models.Books;
    using Knizhar.Services.Books.Models;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class BooksControllerTest
    {
        [Fact]
        public void GetAddRouteShouldBeMapped()
        => MyRouting
               .Configuration()
               .ShouldMap("/Books/Add")
               .To<BooksController>(c => c.Add());

        [Fact]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Books/All")
                .To<BooksController>(c => c.All(With.Any<BookSearchViewModel>()));

        [Fact]
        public void MyBooksRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Books/MyBooks")
                .To<BooksController>(c => c.MyBooks());

        [Theory]
        [InlineData(1, "BookName-AuthorName")]
        public void GetDetailsShouldBeRoutedCorrectly(
            int bookId,
            string information)
           => MyRouting
               .Configuration()
               .ShouldMap("/Books/Details/1/BookName-AuthorName")
               .To<BooksController>(c => c.Details(bookId, information));

        [Fact]
        public void GetEditShouldBeRoutedCorrectly()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                  .WithLocation("/Books/Edit/1")
                  .WithUser())
              .To<BooksController>(c => c.Edit(1));

        //[Theory]
        //[InlineData(10, "test comment")]
        //public void PostEditShouldBeRoutedCorrectly(decimal price, string comment)
        //    => MyRouting
        //        .Configuration()
        //        .ShouldMap(request => request
        //            .WithMethod(HttpMethod.Post)
        //            .WithLocation("/Books/Edit/1")
        //            .WithFormFields(new
        //            {
        //                Price = price,
        //                Comment = comment
        //            })
        //            .WithUser()
        //            .WithAntiForgeryToken())
        //        .To<BooksController>(c => c.Edit(1, new BookFormModel
        //        {
        //            Price = price,
        //            Comment= comment
        //        }))
        //        .AndAlso()
        //        .ToValidModelState();

        //[Theory]
        //[InlineData("authorName", "John")]
        //public void GetFilterShouldBeRoutedCorrectly(
        //   string filtername,
        //   string authorName)
        //  => MyRouting
        //      .Configuration()
        //      .ShouldMap("/Books/Filter?authorName=John")
        //      .To<BooksController>(c => c.Filter(filtername, new BookDetailsModel
        //      {
        //          AuthorName = authorName
        //      }));

        //[Theory]
        //[InlineData(1, "BookName", "AuthorName"/*"123456789123", "Book Name", "Athor Name", 1, 1, 1, "Comment for the book.", "wwwroot/images/books/1.jpg", "1", "Description of the book", false, 10*/)]
        //public void PostAddShouldBeMapped(
        //    int bookId,
        //    string bookName,
        //    string authorName
        //string isbn,
        //string bookName,
        //string authorName,
        //int genreId,
        //int languageId,
        //int conditionId,
        //string comment,
        //string imagePath,
        //string imageId,
        //string description,
        //bool isForGiveAway,
        //decimal price

        //=> MyRouting
        //    .Configuration()
        //    .ShouldMap(request => request
        //        .WithPath("/Books/Details/1/AuthorName-BookName")
        //        .WithMethod(HttpMethod.Post)
        //         .WithFormFields(new
        //         {
        //             Id = bookId,
        //             Name = bookName,
        //             AuthorName = authorName
        //             //Isbn = isbn,
        //             //Name = bookName,
        //             //AuthorName = authorName,
        //             //GenreId = genreId,
        //             //LanguageId = languageId,
        //             //ConditionId = conditionId,
        //             //Comment = comment,
        //             //ImagePath = imagePath,
        //             //ImageId = imageId,
        //             //Description = description,
        //             //IsForGiveAway = isForGiveAway,
        //             //Price = price
        //         })
        //         .WithUser()
        //         .WithAntiForgeryToken())
        //     //.To<BooksController>(c => c.Details(bookId, $"{bookName}-{authorName}"));
        //{
        //Isbn = isbn,
        //Name = bookName,
        //AuthorName = authorName,
        //GenreId = genreId,
        //LanguageId = languageId,
        //ConditionId = conditionId,
        //Comment = comment,
        //ImagePath = imagePath,
        //ImageId = imageId,
        //Description = description,
        //IsForGiveAway = isForGiveAway,
        //Price = price,
        //}))
        //.AndAlso()
        //.ToValidModelState()

    }
}
