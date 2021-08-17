namespace Knizhar.Infrastructure.Seeding
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    public class AuthorsSeeder : ISeeder
    {
        public void Seed(IServiceProvider services)
        {
            var data = services.GetRequiredService<KnizharDbContext>();

            if (data.Authors.Any())
            {
                return;
            }

            data.Authors.AddRange(new[]
            {
                new Author {Name = "John Steinbeck"},
                new Author {Name = "Emily Brontë"},
                new Author {Name = "Jane Austen"},
                new Author {Name = "Stephen King"},
                new Author {Name = "Terry Pratchett"},
                new Author {Name = "Ivan Vazov"},

            });

            data.SaveChanges();
        }
    }
}
