namespace Knizhar.Services.Knizhari
{
    using Knizhar.Models.Knizhari;
    using System.Collections.Generic;
    public interface IKnizharService
    {
        public bool IsKnizhar(string userId);

        public int IdByUser(string userId);
        IEnumerable<TownServiceModel> AllTowns();

        void Create(string userName, int townId, string userId);
    }
}
