namespace Knizhar.Data.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int KnizharId { get; init; }

        public Knizhar Knizhar { get; init; }

        public string UserId { get; init; }

        public User User { get; init; }

        public byte VoteValue { get; set; }
    }
}
