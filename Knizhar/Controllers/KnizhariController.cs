﻿namespace Knizhar.Controllers
{
    using Knizhar.Data;
    using Knizhar.Infrastructure.Extensions;
    using Knizhar.Models.Knizhari;
    using Knizhar.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
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

            //var town = data.Towns.FirstOrDefault(t => t.Name == knizhar.Town);

            //if (town == null)
            //{
            //    town = new Town { Name = knizhar.Town };

            //    this.data.Towns.Add(town);

            //    this.data.SaveChanges();
            //}

            var knizharData = new Knizhar
            {
                UserName = knizhar.UserName,
                TownId = knizhar.TownId,
                UserId = userId,
            };

            this.data.Knizhari.Add(knizharData);

            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "Thank you for becoming a Knizhar!";

            return RedirectToAction(nameof(BooksController.All), "Books");
        }
    }
}