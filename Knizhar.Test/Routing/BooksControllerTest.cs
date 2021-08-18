namespace Knizhar.Test.Routing
{
    using Knizhar.Controllers;
    using Knizhar.Models.Books;
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


        [Fact]
        public void GetDeleteShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Books/Delete/1")
                .To<BooksController>(c => c.Delete(1));

        [Fact]
        public void GetArchiveShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Books/Archive/1")
               .To<BooksController>(c => c.Archive(1));

    }
}
