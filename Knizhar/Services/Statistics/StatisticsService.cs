using Knizhar.Data;
using System.Linq;

namespace Knizhar.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly KnizharDbContext data;

        public StatisticsService(KnizharDbContext data) 
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalBooks = this.data.Books.Count(b => b.IsPublic);
            var totalKnizhari = this.data.Knizhari.Count();

            return new StatisticsServiceModel
            {
                TotalBooks = totalBooks,
                TotalKnizhari = totalKnizhari,
            };
        }
    }
}
