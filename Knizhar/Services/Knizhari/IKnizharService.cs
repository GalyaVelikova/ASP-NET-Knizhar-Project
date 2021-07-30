namespace Knizhar.Services.Knizhari
{
    using System.Collections.Generic;
    public interface IKnizharService
    {
        public bool IsKnizhar(string userId);

        public int IdByUser(string userId);
        IEnumerable<TownServiceModel> AllTowns();
    }
}
