namespace Knizhar.Services.Knizhari
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    public class KnizharService : IKnizharService
    {
        private readonly KnizharDbContext data;

        public KnizharService(KnizharDbContext data)
            => this.data = data;

        public IEnumerable<TownServiceModel> AllTowns()
                => this.data
                    .Towns
                    .Select(b => new TownServiceModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                    })
                    .ToList();

        public void Create(string userName, int townId, string userId)
        {
            //var town = data.Towns.FirstOrDefault(t => t.Name == knizhar.Town);

            //if (town == null)
            //{
            //    town = new Town { Name = knizhar.Town };

            //    this.data.Towns.Add(town);

            //    this.data.SaveChanges();
            //}

            var knizharData = new Knizhar
            {
                UserName = userName,
                TownId = townId,
                UserId = userId,
            };

            this.data.Knizhari.Add(knizharData);

            this.data.SaveChanges();
        }

        public int IdByUser(string userId)
            => this.data
                .Knizhari
                .Where(k => k.UserId == userId)
                .Select(k => k.Id)
                .FirstOrDefault();

        public bool IsKnizhar(string userId)
            => this.data
                .Knizhari
                .Any(k => k.UserId == userId);

        
    }
}
