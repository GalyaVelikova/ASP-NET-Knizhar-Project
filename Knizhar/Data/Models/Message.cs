namespace Knizhar.Data.Models
{
    using System;
    public class Message
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string SenderId { get; set; }

        public User Sender { get; set; }

        public string ReceiverId { get; set; }

        public User Receiver { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
