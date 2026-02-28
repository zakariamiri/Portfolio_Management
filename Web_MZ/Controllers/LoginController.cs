using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_MZ.Data;
using Web_MZ.Services;

namespace Web_MZ.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult login()
        {
            return View();
        }

        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost]
        public IActionResult login(string email, string password)
        {
            var user = _loginService.Authenticate(email, password);

            if (user == null)
            {
                ViewBag.Error = "Email ou mot de passe incorrect.";
                return View();
            }

            // Stocker en session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserFirstName", user.FirstName);
            HttpContext.Session.SetString("UserLastName", user.LastName);
            HttpContext.Session.SetInt32("UserPhone", user.Phone);
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserCountry", user.Country);
            HttpContext.Session.SetString("UserDateCreated", user.DateCreated.ToString("yyyy-MM-dd"));
            // --- SI CREATOR ---
            if (user.Role == "Creator")
            {
                using (var db = new MyDbContext(HttpContext.RequestServices.GetService<DbContextOptions<MyDbContext>>()))
                {
                    // 1) Vérifier s'il a un Creator
                    var creator = db.Creators.FirstOrDefault(c => c.UserId == user.Id);

                    if (creator == null)
                    {
                        // → jamais rempli SecondDemon
                        return RedirectToAction("FirstDemon", "Demon");
                    }

                    // Stocker CreatorId en session pour les étapes suivantes
                    HttpContext.Session.SetInt32("CreatorId", creator.Id);

                    // 2) Vérifier profil rempli
                    bool profileDone =
                        !string.IsNullOrEmpty(creator.Bio) &&
                        !string.IsNullOrEmpty(creator.ProfileImage) &&
                        !string.IsNullOrEmpty(creator.LinkedIn) &&
                        !string.IsNullOrEmpty(creator.GitHub);

                    if (!profileDone)
                    {
                        // → continuer flow profil
                        return RedirectToAction("SecondDemon", "Demon");
                    }

                    // 3) Vérifier langues
                    var langues = db.Langue.Where(l => l.CreatorId == creator.Id).ToList();

                    if (langues.Count == 0)
                    {
                        // → aller à page langues
                        return RedirectToAction("ThirdDemon", "Demon");
                    }

                    // 4) Tout est rempli → Dashboard
                    return RedirectToAction("Dash", "Dashboard");
                }
            }

            // --- SI RECRUITER ---
            if (user.Role == "Recruiter")
                using (var db = new MyDbContext(HttpContext.RequestServices.GetService<DbContextOptions<MyDbContext>>()))
                {
                    // Vérifier s'il a un recruiter
                    var recruiter = db.Recruiters.FirstOrDefault(r => r.UserId == user.Id);

                    if (recruiter == null)
                    {
                        return RedirectToAction("AddProfil", "DashboardRecruiter");
                    }

                    // Stocker CreatorId en session pour les étapes suivantes
                    HttpContext.Session.SetInt32("RecruiterId", recruiter.Id);

                    // Tout est rempli → Dashboard
                    return RedirectToAction("Welcom", "DashboardRecruiter");
                }

            return RedirectToAction("Index", "Home");
        }


    }


}
