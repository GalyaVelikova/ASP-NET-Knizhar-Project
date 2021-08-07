using Knizhar.Services.Books.Models;

namespace Knizhar.Infrastructure
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IBookModel book)
            => book.Name + "-" + book.AuthorName;
    }
}
