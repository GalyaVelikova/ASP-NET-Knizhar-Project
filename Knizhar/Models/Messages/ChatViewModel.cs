namespace Knizhar.Models.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ChatViewModel
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string SenderId { get; set; }

        public string SenderUserName { get; set; }
        public string RecieverId { get; set; }

        public string ReceiverUserName { get; set; }
        public IEnumerable<MessageViewModel> Messages { get; set; }
        public int Count
            => this.Messages.Count();
    }
}
