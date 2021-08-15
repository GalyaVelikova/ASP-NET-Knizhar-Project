namespace Knizhar.Areas.Admin.Services
{
    using Knizhar.Data.Models;
    using Knizhar.Models.Books;
    using Knizhar.Services.Books.Models;
    using Knizhar.Services.Knizhari;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    public interface ICategoriesService
    {
        public bool AddCondition(string conditionName);

        public bool DeleteCondition(string conditionName);
        public bool AddGenre(string genreName);

        public bool DeleteGenre(string genreName);
        public bool AddLanguage(string languageName);

        public bool DeleteLanguage(string languageName);
        public bool AddTown(string townName);

        public bool DeleteTown(string townName);

    }
}
