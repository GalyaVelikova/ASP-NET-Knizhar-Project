namespace Knizhar.Models.Messages
{
    using System;

    public class LatestChatViewModel
    {
        public string RecieverId { get; set; }

        public string RecieverFirstName { get; set; }

        public string Content { get; set; }

        public string SenderId { get; set; }

        public string SenderFirstName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Message
            => this.Content.Substring(0, this.Content.Length > 21 ? 21 : this.Content.Length) + (this.Content.Length > 21 ? "..." : "");
    }
}
