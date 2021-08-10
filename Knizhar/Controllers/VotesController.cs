namespace Knizhar.Controllers
{
    using Knizhar.Infrastructure.Extensions;
    using Knizhar.Models.Votes;
    using Knizhar.Services.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
       
        private readonly IVoteService votes;

        public VotesController(IVoteService votes)
        {
            this.votes = votes;;
        }

        [HttpPost]
        [Authorize]
        public PostVoteResponseModel PostVote(PostVoteInputModel voteInput)
        {
            var userId = this.User.Id();

            this.votes.SetVote(voteInput.KnizharId, userId, voteInput.Value);

            var averageVote = this.votes.GetAverageVotes(voteInput.KnizharId);

            return new PostVoteResponseModel{AverageVote = averageVote};
        }
    }
}
