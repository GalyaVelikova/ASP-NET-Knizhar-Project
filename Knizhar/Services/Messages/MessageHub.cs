namespace Knizhar.Services.Messages
{
    using Ganss.XSS;
    using Knizhar.Data.Models;
    using Knizhar.Services.Messages.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;
    using System.Linq;
    using System.Threading.Tasks;

    public class MessageHub : Hub
    {
        private readonly UserManager<User> userManager;

        public MessageHub(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task Send(SendMessageFormModel inputModel)
        {
            var sanitizer = new HtmlSanitizer();
            var message = sanitizer.Sanitize(inputModel.Message);

            if (string.IsNullOrEmpty(message) ||
                string.IsNullOrWhiteSpace(message) ||
                string.IsNullOrEmpty(message))
            {
                return;
            }

            var caller = this.userManager
                .Users
                .First(x => x.Id == inputModel.CallerId);

            await this.Clients
                .User(inputModel.UserId)
                .SendAsync("RecieveMessage", message, caller);

            await this.Clients
                .Caller
                .SendAsync("SendMessage", message);
        }
    }
}
