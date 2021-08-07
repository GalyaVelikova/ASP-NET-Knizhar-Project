using System.Collections.Generic;

namespace Knizhar.Services.Books.Models
{
    public class BookServiceModel : IBookModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string ImagePath { get; set; }
        public string AuthorName { get; init; }
        public string TheBookIsFor { get; set; }
        public decimal? Price { get; set; }

        //public bool IsFavourite { get; set; }
    }
}
