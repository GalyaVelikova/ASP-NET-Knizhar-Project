namespace Knizhar.Infrastructure.Extensions
{
    using Knizhar.Services.Books.Models;

    public static class ModelExtensions
    {
        public static string GetInformation(this IBookModel book)
            => book.Name + "-" + book.AuthorName;
    }
}
