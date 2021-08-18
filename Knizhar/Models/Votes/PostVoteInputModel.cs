namespace Knizhar.Models.Votes
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Vote;
    public class PostVoteInputModel
    {
        public int KnizharId { get; init; }

        [Range(VoteMinValue, VoteMaxValue)]
        public byte Value { get; set; }
    }
}
