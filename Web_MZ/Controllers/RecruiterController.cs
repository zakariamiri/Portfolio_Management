using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_MZ.Models;
using Web_MZ.Services;

namespace Web_MZ.Controllers
{
    public class RecruiterController : Controller
    {

        private readonly IRecruiterService _recruiterService;

        public RecruiterController(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        [HttpGet]
        public IActionResult AddProfil()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProfil(RecruiterViewModel model)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

            if (userId == 0)
            {
                return RedirectToAction("login", "Login");
            }

            var recruiter = _recruiterService.CreateRecruiter(userId, model);

            HttpContext.Session.SetInt32("RecruiterId", recruiter.Id);

            return RedirectToAction("Welcom", "DashboardRecruiter");
        }

    }
}
