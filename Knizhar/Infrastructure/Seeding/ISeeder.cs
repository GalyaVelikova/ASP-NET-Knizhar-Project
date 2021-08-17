namespace Knizhar.Infrastructure.Seeding
{
    using System;
    public interface ISeeder
    {
        public void Seed(IServiceProvider services);
    }
}
