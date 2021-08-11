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

        public bool isPublic { get; init; }

        public bool isArchived { get; init; }
        //public bool IsFavourite { get; set; }
    }
}
