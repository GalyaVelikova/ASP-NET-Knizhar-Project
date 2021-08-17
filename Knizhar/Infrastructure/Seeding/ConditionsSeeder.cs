namespace Knizhar.Infrastructure.Seeding
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    public class ConditionsSeeder : ISeeder
    {
        public void Seed(IServiceProvider services)
        {
            var data = services.GetRequiredService<KnizharDbContext>();

            if (data.Conditions.Any())
            {
                return;
            }

            data.Conditions.AddRange(new[]
            {
                new Condition {ConditionName = "Excellent"},
                new Condition {ConditionName = "Very Good"},
                new Condition {ConditionName = "Good"},
                new Condition {ConditionName = "Bad"},

            });

            data.SaveChanges();
        }
    }
}
