namespace Knizhar.Services.Messages.Models
{
    using System.ComponentModel.DataAnnotations;
    public class SendMessageFormModel
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string UserId { get; set; }

        public string CallerId { get; set; }
    }
}
