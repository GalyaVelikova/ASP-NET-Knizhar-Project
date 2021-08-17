namespace Knizhar.Infrastructure.Seeding
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class GenresSeeder : ISeeder
    {
        public void Seed(IServiceProvider services)
        {
            var data = services.GetRequiredService<KnizharDbContext>();

            if (data.Genres.Any())
            {
                return;
            }

            data.Genres.AddRange(new[]
            {
                new Genre {Name = "Fiction"},
                new Genre {Name = "Biographical"},
                new Genre {Name = "Comics"},
                new Genre {Name = "Crime"},
                new Genre {Name = "Fantasy"},
                new Genre {Name = "Historical"},
                new Genre {Name = "Horror"},
                new Genre {Name = "Humorous"},
                new Genre {Name = "Legal"},
                new Genre {Name = "Medical"},
                new Genre {Name = "Political"},
                new Genre {Name = "Psychological"},
                new Genre {Name = "Romance"},
                new Genre {Name = "Science Fiction"},
                new Genre {Name = "CookingFood"},
                new Genre {Name = "Health And DailyLiving"},
                new Genre {Name = "School And Education"},
                new Genre {Name = "Science And Technology"},
                new Genre {Name = "Art"},
                new Genre {Name = "Computer"},
                new Genre {Name = "Health And Fitness"},
            });

            data.SaveChanges();
        }
    }
}
