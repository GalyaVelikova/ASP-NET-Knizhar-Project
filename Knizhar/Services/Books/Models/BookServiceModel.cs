namespace Knizhar.Services.Books.Models
{
    public class BookServiceModel
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public string ImageUrl { get; init; }
        public string Author { get; init; }
        public string TheBookIsFor { get; set; } 
        public decimal? Price { get; set; }

        //public bool IsFavourite { get; set; }

        //public string UserImage { get; set; }

        //public string UserName { get; set; }
    }
}
