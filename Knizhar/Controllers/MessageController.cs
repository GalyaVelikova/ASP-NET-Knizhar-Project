namespace Knizhar.Controllers
{
    using Ganss.XSS;
    using Knizhar.Models.Messages;
    using Knizhar.Services.Messages;
    using Knizhar.Services.Messages.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    //[Produces("application/json")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messagesService;

        public MessageController(IMessageService messagesService)
        {
            this.messagesService = messagesService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SendMessageFormModel messageModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NoContent();
            }

            var sanitizer = new HtmlSanitizer();
            var message = sanitizer.Sanitize(messageModel.Message);
            var senderId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var model = await this.messagesService.Create(senderId, messageModel.UserId, message);
            var result = new JsonResult(model);

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string receiverId, string senderId)
        {
            var model = new ChatViewModel();
            model.Messages = await this.messagesService.GetMessagesAsync(receiverId, senderId);
            var result = new JsonResult(model);

            return result;
        }
    }
}

