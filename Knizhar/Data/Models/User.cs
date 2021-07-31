using Microsoft.AspNetCore.Identity;

namespace Knizhar.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.User;
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }
    }
}
