namespace Knizhar.Models.Books
{
    using Knizhar.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Book;
    using static Data.DataConstants.Author;
    using Knizhar.Services.Books.Models;
    using Microsoft.AspNetCore.Http;

    public class BookFormModel : IBookModel
    {
        [Required]
        [IsbnValidationAttribute]
        public string Isbn { get; init; }

        [Required]
        [StringLength(
            BookNameMaxLength, 
            MinimumLength =BookNameMinLength, 
            ErrorMessage = "The name of the book should be between {2} and {1} symbols long.")]
        public string Name { get; init; }
        
        [Required]
        [StringLength(
            AuthorNameMaxLength, 
            MinimumLength = AuthorNameMinLength, 
            ErrorMessage = "The name of the author should be between {2} and {1} symbols long.")]
        public string AuthorName { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; init; }

        [Required]
        [Display(Name = "Language")]
        public int LanguageId { get; init; }

        [Required]
        [Display(Name = "Condition")]
        public int ConditionId { get; init; }

        [StringLength(
            CommentMaxLength,
            MinimumLength = CommentMinLength,
            ErrorMessage = "The comment should be between {2} and {1} symbols long.")]
        public string Comment { get; init; }

        [Required]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedImageExtensions(new string[] { ".png", ".jpg", ".gif" })]
        public IFormFile Image { get; init; }

        public string ImagePath { get; set; }

        public string ImageId { get; set; }

        [Required]
        [StringLength(
            BookDescriptionMaxLength, 
            MinimumLength=BookDescriptionMinLength, 
            ErrorMessage = "The description of the book should be at least {2} symbols long.")]
        public string Description { get; init; }
        public bool IsForGiveAway { get; set; }
        public decimal Price { get; set; }
        public bool IsArchived { get; set; }

        public IEnumerable<BookGenreServiceModel> Genres { get; set; }

        public IEnumerable<BookLanguageServiceModel> Languages { get; set; }

        public IEnumerable<BookConditionServiceModel> Conditions { get; set; }

    }
}
