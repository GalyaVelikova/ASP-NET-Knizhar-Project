namespace Knizhar.Test.Data
{
    using Knizhar.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public static class Books
    {

        public static List<Knizhar> GetKnizhar(string userName, int townId, int count, string userId, int knizharId)
            => Enumerable
                .Range(1, count)
                .Select(i => new Knizhar
                {
                    UserName = userName,
                    TownId = townId,
                    UserId = userId,
                    Id = knizharId
                })
                .ToList();

        public static Image GetImage(string id, int addedByKnizharId, int bookId, string extension, int count)
           => new Image
           {
               Id = id,
               AddedByKnizharId = addedByKnizharId,
               BookId = bookId,
               Extension = extension,
           };

        public static Book GetBook(int authorId, string authorName, int addedByKnizharId, int bookId, string extension, int count, string isbn, string bookName, int genreId, int languageId, int conditionId, string imageId, string description, string comment, bool isForGiveAway, decimal price, int knizharId)
        => new Book
        {
            Isbn = isbn,
            Name = bookName,
            GenreId = genreId,
            LanguageId = languageId,
            ConditionId = conditionId,
            Description = description,
            Author = GetAuthor(authorId, authorName),
            AuthorId = authorId,
            Comment = comment,
            Image = GetImage(imageId, addedByKnizharId, bookId, extension, count),
            ImageId = imageId,
            IsForGiveAway = isForGiveAway,
            Price = price,
            KnizharId = knizharId,
            IsPublic = true,
            Id = bookId
        };

        public static Author GetAuthor(int id, string name)
            => new Author
            {
                Name = name,
                Id = id
            };

        public static IFormFile CreateTestFormFile(string p_Name, string p_Content)
        {
            byte[] s_Bytes = Encoding.UTF8.GetBytes(p_Content);

            return new FormFile(
                baseStream: new MemoryStream(s_Bytes),
                baseStreamOffset: 0,
                length: s_Bytes.Length,
                name: "Data",
                fileName: p_Name
            );
        }
    }
}
