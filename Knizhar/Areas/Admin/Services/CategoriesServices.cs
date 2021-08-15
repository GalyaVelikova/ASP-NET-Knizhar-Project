namespace Knizhar.Areas.Admin.Services
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using System.Linq;

    public class CategoriesServices : ICategoriesService
    {

        private readonly KnizharDbContext data;

        public CategoriesServices(KnizharDbContext data)
        {
            this.data = data;
        }

        public bool AddCondition(string conditionName)
        {
            var conditions = this.data.Conditions.AsQueryable();

            if (conditions.Any(c => c.ConditionName == conditionName))
            {
                return false;
            }
            var conditionData = new Condition { ConditionName = conditionName };

            this.data.Conditions.Add(conditionData);
            this.data.SaveChanges();

            return true;
        }

        public bool AddGenre(string genreName)
        {
            var genres = this.data.Genres.AsQueryable();

            if (genres.Any(g => g.Name == genreName))
            {
                return false;
            }
            var genreData = new Genre { Name = genreName };

            this.data.Genres.Add(genreData);
            this.data.SaveChanges();

            return true;
        }

        public bool AddLanguage(string languageName)
        {
            var languages = this.data.Languages.AsQueryable();

            if (languages.Any(l => l.LanguageName == languageName))
            {
                return false;
            }
            var languageData = new Language { LanguageName = languageName };

            this.data.Languages.Add(languageData);
            this.data.SaveChanges();

            return true;
        }

        public bool AddTown(string townName)
        {
            var towns = this.data.Towns.AsQueryable();

            if (towns.Any(t => t.Name == townName))
            {
                return false;
            }
            var townData = new Town { Name = townName };

            this.data.Towns.Add(townData);
            this.data.SaveChanges();

            return true;
        }

        public bool DeleteGenre(string genreName)
        {
            var genre = this.data.Genres.FirstOrDefault(g => g.Name == genreName);

            if (genre != null)
            {
                this.data.Genres.Remove(genre);
                this.data.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteLanguage(string languageName)
        {
            var language = this.data.Languages.FirstOrDefault(l => l.LanguageName == languageName);

            if (language != null)
            {
                this.data.Languages.Remove(language);
                this.data.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteTown(string townName)
        {
            var town = this.data.Towns.FirstOrDefault(t => t.Name == townName);

            if (town != null)
            {
                this.data.Towns.Remove(town);
                this.data.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteCondition(string conditionName)
        {
            var condition = this.data.Conditions.FirstOrDefault(c => c.ConditionName == conditionName);

            if (condition != null)
            {
                this.data.Conditions.Remove(condition);
                this.data.SaveChanges();
                return true;
            }

            return false;
        }

        
    }
}
