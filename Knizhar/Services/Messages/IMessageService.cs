namespace Knizhar.Services.Messages
{
    using Knizhar.Data.Models;
    using Knizhar.Models.Messages;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IMessageService
    {
        public Task<Message> Create(string senderId, string recieverId, string message);

        public Task<Message> Delete(string messageId);

        public Task<IEnumerable<MessageViewModel>> GetMessagesAsync(string senderId, string recieverId);

        public IEnumerable<LatestChatViewModel> GetLatestChatsAsync(string userId);
    }
}
