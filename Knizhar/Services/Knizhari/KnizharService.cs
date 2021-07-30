using Knizhar.Data;
using System.Collections.Generic;
using System.Linq;

namespace Knizhar.Services.Knizhari
{
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
