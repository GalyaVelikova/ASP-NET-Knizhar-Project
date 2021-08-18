namespace Knizhar.Controllers
{
    using Knizhar.Data;
    using Knizhar.Infrastructure.Extensions;
    using Knizhar.Models.Knizhari;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Knizhar.Services.Knizhari;

    using static WebConstants;

    public class KnizhariController : Controller
    {
        private readonly KnizharDbContext data;
        private readonly IKnizharService knizhari;

        public KnizhariController(
            IKnizharService knizhari,
            KnizharDbContext data)
        {
            this.knizhari = knizhari;
            this.data = data;
        }

        [Authorize]
        public IActionResult Create() => View(new BecomeKnizharFormModel
        {
            Towns = this.knizhari.AllTowns(),
        });

        [HttpPost]
        [Authorize]

        public IActionResult Create(BecomeKnizharFormModel knizhar)
        {
            var userId = this.User.Id();

            var userIsKnizhar = knizhari.IsKnizhar(userId);
             
            if (userIsKnizhar)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(knizhar);
            }

            knizhari.Create(
                    knizhar.UserName, 
                    knizhar.TownId, 
                    userId);

            TempData[GlobalMessageKey] = "Thank you for becoming a Knizhar!";

            return RedirectToAction(nameof(BooksController.All), "Books");
        }
    }
}