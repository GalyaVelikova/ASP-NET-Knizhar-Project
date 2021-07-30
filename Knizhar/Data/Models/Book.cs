namespace Knizhar.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Book;
    public class Book
    {
        public int Id { get; init; }

        [Required]
        public int KnizharId { get; init; }

        public Knizhar Knizhar { get; init; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        [MaxLength(BookNameMaxLength)]
        public string Name { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; init; }

        public int LanguageId { get; set; }

        public Language Language { get; init; }

        public int ConditionId { get; set; }
        public  Condition Condition { get; init; }

        [MaxLength(CommentMaxLength)]
        public string Comment { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsForGiveAway { get; set; }

        public decimal? Price { get; set; }

        public DateTime AddedOn { get; set; }

        public bool Favourite { get; set; }

        public bool IsArchived { get; set; }

       //public IEnumerable<ApplicationUser> UsersFavouriteBooks { get; init; } = new List<ApplicationUser>();

        //public ICollection<ApplicationUser> Users { get; init; } = new List<ApplicationUser>();
    }
}
