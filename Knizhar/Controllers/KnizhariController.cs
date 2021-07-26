namespace Knizhar.Controllers
{
    using Knizhar.Data;
    using Knizhar.Infrastructure;
    using Knizhar.Models.Knizhari;
    using Knizhar.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Collections.Generic;

    public class KnizhariController : Controller
    {
        private readonly KnizharDbContext data;

        public KnizhariController(KnizharDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Create() => View(new BecomeKnizharFormModel
        {
            Towns = this.GetTowns(),
        });

        [HttpPost]
        [Authorize]

        public IActionResult Create(BecomeKnizharFormModel knizhar)
        {
            var userId = this.User.GetId();

            var userIsKnizhar = this.data
                .Knizhari
                .Any(k => k.UserId == userId);

            if (userIsKnizhar)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(knizhar);
            }

            var town = data.Towns.FirstOrDefault(t => t.Name == knizhar.Town);

            if (town == null)
            {
                town = new Town { Name = knizhar.Town };
                
                this.data.Towns.Add(town);

                this.data.SaveChanges();
            }

            var knizharData = new Knizhar
            {
                UserName = knizhar.UserName,
                TownId = town.Id,
                UserId = userId,
            };

            this.data.Knizhari.Add(knizharData);

            this.data.SaveChanges();

            return RedirectToAction("All", "Books");
        }

        private IEnumerable<TownViewModel> GetTowns()
         => this.data
                .Towns
                .Select(b => new TownViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToList();
    }
}
