namespace Knizhar.Services.Messages.Models
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Knizhar.Models.Messages;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MessageService : IMessageService
    {
        private readonly KnizharDbContext data;

        public MessageService(KnizharDbContext data)
        {
            this.data = data;
        }

        public async Task<Message> Create(string senderId, string receiverId, string message)
        {
            var newMessage = new Message()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = message,
            };

            await this.data.AddAsync(newMessage);
            await this.data.SaveChangesAsync();

            return newMessage;
        }

        public async Task<Message> Delete(string messageId)
        {
           var message = this.data
                .Messages
                .FirstOrDefault(m => m.Id == messageId);

            this.data.Messages.Remove(message);
            await this.data.SaveChangesAsync();
            return message;
        }

        public IEnumerable<LatestChatViewModel> GetLatestChatsAsync(string userId)
        {
            var messages = this.data
                .Messages
                .Where(x => x.ReceiverId == userId || x.SenderId == userId)
                .Select(x => new LatestChatViewModel
                {
                    Content = x.Content,
                    RecieverFirstName = x.Receiver.FullName,
                    RecieverId = x.Receiver.Id,
                    SenderId = x.SenderId,
                    CreatedOn = x.CreatedOn,
                    SenderFirstName = x.Sender.FullName,
                })
                .OrderByDescending(x => x.CreatedOn)
                .Distinct()
                .Take(5)
                .ToList();

            var result = new List<LatestChatViewModel>();
            foreach (var message in messages)
            {
                if (this.data.Messages
                    .Any(y => (y.CreatedOn > message.CreatedOn) && (y.ReceiverId == message.RecieverId || y.SenderId == message.RecieverId)) == false)
                {
                    result.Add(message);
                }
            }

            return result;
        }
        public async Task<IEnumerable<MessageViewModel>> GetMessagesAsync(string senderId, string recieverId)
        {
            return await Task.Run(() => GetMessage(senderId, recieverId));
        }

        private IEnumerable<MessageViewModel> GetMessage(string senderId, string recieverId)
            => this.data
                .Messages
                .Where(x => (x.SenderId == senderId && x.ReceiverId == recieverId) ||
                (x.SenderId == recieverId && x.ReceiverId == senderId))
                .Select(m => new MessageViewModel
                {
                    Content = m.Content,
                    CreatedOn = m.CreatedOn,
                    SenderId = m.SenderId,
                    RecieverId = m.ReceiverId,
                })
                .OrderBy(x => x.CreatedOn)
                .ToList();
    }
}
