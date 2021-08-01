namespace Knizhar.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class CarsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
