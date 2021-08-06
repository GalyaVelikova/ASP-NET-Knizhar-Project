namespace Knizhar.Services.Books.Models
{
    using Microsoft.AspNetCore.Http;
    public class BookServiceModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string ImagePath { get; set; }
        public string Author { get; init; }
        public string TheBookIsFor { get; set; } 
        public decimal? Price { get; set; }

        //public bool IsFavourite { get; set; }
    }
}
