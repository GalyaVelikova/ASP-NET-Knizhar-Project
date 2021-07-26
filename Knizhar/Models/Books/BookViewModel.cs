namespace Knizhar.Models.Books
{
    public class BookViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public string ImageUrl { get; init; }
        public string Author { get; init; }

        public string Isbn { get; init; }

        public string Language { get; init; }

        public string Genre { get; init; }

        public string Condition { get; set; }

        public string Comment { get; set; }

        public string BookLocation { get; init; }

        public string TheBookIsFor { get; init; }
    }
}
