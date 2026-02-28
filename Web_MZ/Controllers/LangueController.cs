using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_MZ.Models;
using Web_MZ.Services;

namespace Web_MZ.Controllers
{
    public class LangueController : Controller
    {
        private readonly ILangueService _service;

        public LangueController(ILangueService service)
        {
            _service = service;
        }

        public IActionResult Langues()
        {
            return View("ThirdDemon");
        }

        [HttpPost]
        public IActionResult SaveLangues(LangueViewModel vm)
        {
            int? creatorId = HttpContext.Session.GetInt32("CreatorId");
            if (creatorId == null)
            {
                return RedirectToAction("SecondDemon", "Creator");
            }
            _service.SaveLangues(creatorId.Value, vm);

            return RedirectToAction("Dash", "Dashboard");
        }
    }

}
