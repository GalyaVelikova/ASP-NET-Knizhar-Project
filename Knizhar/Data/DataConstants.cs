namespace Knizhar.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int FullNameMaxLength = 40;
            public const int FullNameMinLength = 2;

            public const int PasswordMaxLength = 100;
            public const int PasswordMinLength = 6;
        }
        public class Book
        {
            public const string IsbnRegularExpression13Digits = @"[1-9]{13}";
            public const string IsbnRegularExpression10Digits = @"[1-9]{10}";
            public const int BookNameMaxLength = 50;
            public const int BookNameMinLength = 2;
            public const int BookDescriptionMinLength = 10;
            public const int BookDescriptionMaxLength = 10000;
            public const int CommentMaxLength = 200;
            public const int CommentMinLength = 3;
        }

        public class Condition 
        {
            public const int ConditionNameMaxLength = 25;
            public const int ConditionNameMinLength = 3;

        }
        public class Genre
        {
            public const int GenreNameMaxLength = 40;
            public const int GenreNameMinLength = 3;
        }
        public class Language
        {
            public const int LanguageNameMaxLength = 20;
            public const int LanguageNameMinLength = 5;
        }
        public class Author
        {
            public const int AuthorNameMinLength = 2;
            public const int AuthorNameMaxLength = 30;
        }
        public class Knizhar
        {
            public const int UserNameMaxLength = 25;
            public const int UserNameMinLength = 3;
        }

        public class Town
        {
            public const int TownNameMinLength = 3;
            public const int TownNameMaxLength = 30;
        }
    };

}
