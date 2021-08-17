namespace Knizhar.Infrastructure.Seeding
{
    using Knizhar.Data;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;

    public class KnizharDbContextSeeder : ISeeder
    {
        public void Seed(IServiceProvider services)
        {
            var data = services.GetRequiredService<KnizharDbContext>();

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var seeders = new List<ISeeder>()
            {
                new GenresSeeder(),
                new LanguagesSeeder(),
                new ConditionsSeeder(),
                new TownsSeeder(),
                new AdministratorSeeder(),
                new AuthorsSeeder(),
            };

            foreach (var seeder in seeders)
            {
                seeder.Seed(services);
                data.SaveChanges();
            }

        }
    }
}
