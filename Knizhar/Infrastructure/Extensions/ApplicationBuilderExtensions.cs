namespace Knizhar.Infrastructure.Extensions
{
    using Knizhar.Data;
    using Knizhar.Infrastructure.Seeding;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;
            var dbSeeder = new KnizharDbContextSeeder();

            MigrateDatabase(services);
            dbSeeder.Seed(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<KnizharDbContext>();

            data.Database.Migrate();

        }

    }
}