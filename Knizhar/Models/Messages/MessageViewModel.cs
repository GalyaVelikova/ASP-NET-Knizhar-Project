namespace Knizhar.Models.Messages
{
    using System;
    public class MessageViewModel
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string SenderId { get; set; }

        public string RecieverId { get; set; }

    }
}
