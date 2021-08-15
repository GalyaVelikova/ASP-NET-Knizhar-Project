using Microsoft.AspNetCore.Identity;

namespace Knizhar.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.User;
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        public IList<Vote> Votes { get; set; } = new List<Vote>();

        //public int FavouriteBookId { get; set; }

        //public FavouriteBook FavouriteBook { get; set; }
        public IList<FavouriteBook> FavouriteBooks { get; set; } = new List<FavouriteBook>();
    }
}
