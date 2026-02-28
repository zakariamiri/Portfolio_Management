using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_MZ.Data;
using Web_MZ.Entities;

namespace Web_MZ.Controllers
{
    public class SignUpRecruiterController : Controller
    {
        private readonly MyDbContext _context;

        public SignUpRecruiterController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult recruiter()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult recruiter(User user)
        {
            if (!ModelState.IsValid)
            {
                // Afficher toutes les erreurs dans la vue
                ViewBag.Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return View(user);
            }

            user.Role = "Recruiter";
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login", "Login");
        }

    }
}
