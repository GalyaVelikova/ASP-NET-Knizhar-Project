namespace Knizhar.Services.Votes
{
    public interface IVoteService
    {
        void SetVote(int knizharId, string userId, byte vote);

        double GetAverageVotes(int knizharId);
    }
}
