using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_MZ.Models;
using Web_MZ.Services;

namespace Web_MZ.Controllers
{
    public class CreatorController : Controller
    {
        private readonly ICreatorService _creatorService;

        public CreatorController(ICreatorService creatorService)
        {
            _creatorService = creatorService;
        }

        [HttpGet]
        public IActionResult SecondDemon()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SecondDemon(CreatorViewModel model)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

            if (userId == 0)
            {
                return RedirectToAction("login", "Login");
            }

            var creator = _creatorService.CreateCreator(userId, model);

            // Stocker l'ID du Creator en session pour l'étape suivante
            HttpContext.Session.SetInt32("CreatorId", creator.Id);

            return RedirectToAction("ThirdDemon", "Demon");
        }

    }
}
