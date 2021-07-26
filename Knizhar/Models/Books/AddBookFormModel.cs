﻿namespace Knizhar.Models.Books
{
    using Knizhar.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using static Data.DataConstants.Book;
    using static Data.DataConstants.Author;
    using static Data.DataConstants.Condition;
    public class AddBookFormModel
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
            ErrorMessage = "The name of the book should be between {2} and {1} symbols long.")]
        public string Author { get; init; }

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
        [Display(Name = "Image")]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(
            10000, 
            MinimumLength=BookDescriptionMinLength, 
            ErrorMessage = "The description of the book should be at least {2} symbols long.")]
        public string Description { get; init; }
        public bool IsForGiveAway { get; set; }
        public decimal? Price { get; init; }
        public bool IsArchived { get; set; }

        public IEnumerable<BookGenreViewModel> Genres { get; set; }

        public IEnumerable<BookLanguageViewModel> Languages { get; set; }

        public IEnumerable<BookConditionViewModel> Conditions { get; set; }

    }
}
