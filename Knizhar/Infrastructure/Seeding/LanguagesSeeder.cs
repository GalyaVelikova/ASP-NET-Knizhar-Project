namespace Knizhar.Infrastructure.Seeding
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    public class LanguagesSeeder : ISeeder
    {
        public void Seed(IServiceProvider services)
        {
            var data = services.GetRequiredService<KnizharDbContext>();

            if (data.Languages.Any())
            {
                return;
            }

            data.Languages.AddRange(new[]
            {
                new Language {LanguageName = "Bulgarian"},
                new Language {LanguageName = "English"},

            });

            data.SaveChanges();
        }
    }
}
