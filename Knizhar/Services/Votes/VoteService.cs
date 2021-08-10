namespace Knizhar.Services.Votes
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using System.Linq;

    public class VoteService : IVoteService
    {
        private readonly KnizharDbContext data;

        public VoteService(KnizharDbContext data)
        {
            this.data = data;
        }

        public void SetVote(int knizharId, string userId, byte voteValue)
        {
            var vote = this.data.Votes
                    .FirstOrDefault(v => v.KnizharId == knizharId && v.UserId == userId);

            if (vote == null)
            {
                vote = new Vote
                {
                    KnizharId = knizharId,
                    UserId = userId
                };

                this.data.Votes.Add(vote);
            }

            vote.VoteValue = voteValue;

            this.data.SaveChanges();
        }
        public double GetAverageVotes(int knizharId)
            => this.data
                    .Votes
                    .Where(v => v.KnizharId == knizharId)
                    .Average(v => v.VoteValue);
    }
}
