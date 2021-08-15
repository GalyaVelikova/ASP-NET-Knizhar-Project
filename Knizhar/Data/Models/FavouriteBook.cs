namespace Knizhar.Data.Models
{
    public class FavouriteBook
    {
        public int Id { get; set; }
        public int BookId { get; init; }

        public Book Book { get; set; }

        public string UserId { get; init; }

        public User User { get; set; }
    }
}
