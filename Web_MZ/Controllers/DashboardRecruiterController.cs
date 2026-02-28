using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_MZ.Data;
using Web_MZ.Entities;
using Web_MZ.Models;
using Web_MZ.Pdf;
using Web_MZ.Services;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Web_MZ.Controllers
{
    public class DashboardRecruiterController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IRecruiterService _recruiterService;

        public DashboardRecruiterController(IRecruiterService recruiterService, MyDbContext context)
        {
            _recruiterService = recruiterService;
            _context = context;
        }
        public IActionResult DashRecruiter(int page = 1, int pageSize = 8)
        {
            // --- Profil du recruiter ---
            ViewBag.FirstName = HttpContext.Session.GetString("UserFirstName");
            ViewBag.LastName = HttpContext.Session.GetString("UserLastName");
            int? recruiterId = HttpContext.Session.GetInt32("RecruiterId");

            var recruiter = _recruiterService.GetRecruiterByUserId(recruiterId.Value);
            if (recruiter == null)
                return RedirectToAction("AddProfil", "DashboardRecruiter");

            ViewBag.Bio = recruiter.Bio;
            ViewBag.ProfileImage = recruiter.ProfileImage;
            ViewBag.CompanyName = recruiter.CompanyName;
            ViewBag.Position = recruiter.Position;

            // --- Liste des creators avec pagination ---
            var query = _context.Creators
                .Include(c => c.User)
                .Where(c => c.User.Role == "Creator");

            int totalCreators = query.Count();

            var creators = query
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CreatorCardVM
                {
                    CreatorId = c.Id,
                    FullName = c.User.FirstName + " " + c.User.LastName,
                    Bio = c.Bio,
                    Country = c.User.Country,
                    ProfileImage = c.ProfileImage,
                    GitHub = c.GitHub,
                    LinkedIn = c.LinkedIn,
                    Email = c.User.Email
                })
                .ToList();

            var model = new CreatorPaginationVM
            {
                Creators = creators,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalCreators / pageSize)
            };

            return View(model);
        }


        public IActionResult AddProfil()
        {
            return View();
        }

        public IActionResult Welcom()
        {
            // --- Profil du recruiter ---
            ViewBag.FirstName = HttpContext.Session.GetString("UserFirstName");
            ViewBag.LastName = HttpContext.Session.GetString("UserLastName");
            int? recruiterId = HttpContext.Session.GetInt32("RecruiterId");

            var recruiter = _recruiterService.GetRecruiterByUserId(recruiterId.Value);
            if (recruiter == null)
                return RedirectToAction("AddProfil", "DashboardRecruiter");

            ViewBag.Bio = recruiter.Bio;
            ViewBag.ProfileImage = recruiter.ProfileImage;
            ViewBag.CompanyName = recruiter.CompanyName;
            ViewBag.Position = recruiter.Position;
            return View();
        }

        public IActionResult ConsulterPortfolio(int id)
        {
            ViewBag.FirstName = HttpContext.Session.GetString("UserFirstName");
            ViewBag.LastName = HttpContext.Session.GetString("UserLastName");
            int? recruiterId = HttpContext.Session.GetInt32("RecruiterId");

            if (!recruiterId.HasValue)
            {
                // Session expirée ou utilisateur non connecté
                return RedirectToAction("login", "Login");
            }

            var recruiter = _recruiterService.GetRecruiterByUserId(recruiterId.Value);

            if (recruiter == null)
            {
                return RedirectToAction("AddProfil");
            }


            ViewBag.Bio = recruiter.Bio;
            ViewBag.ProfileImage = recruiter.ProfileImage;
            ViewBag.CompanyName = recruiter.CompanyName;
            ViewBag.Position = recruiter.Position;

            // ====== CREATOR ======
            var creator = _context.Creators
                .Include(c => c.User)
                .FirstOrDefault(c => c.Id == id);

            if (creator == null)
                return NotFound();

            ViewBag.Country = creator.User.Country;

            // ====== VIEWMODEL ======
            var model = new ConsulterPortfolioVM
            {
                Creator = creator,
                Experiences = _context.Experiences
                    .Where(e => e.CreatorId == id)
                    .OrderByDescending(e => e.StartDate)
                    .ToList(),

                Projects = _context.Projects
                    .Where(p => p.CreatorId == id)
                    .ToList(),

                Competences = _context.Competences
                    .Where(c => c.CreatorId == id)
                    .ToList(),

                Langues = _context.Langue
                    .Where(l => l.CreatorId == id)
                    .ToList(),

                Certifications = _context.Certifications
                    .Where(c => c.CreatorId == id)
                    .ToList()
            };

            return View(model);
        }



        [HttpPost]
        public IActionResult SendContact(Contact model)
        {
            int? recruiterId = HttpContext.Session.GetInt32("RecruiterId");
            if (recruiterId == null)
                return RedirectToAction("AddProfil");

            // Associer le message au recruiter
            model.RecruiterId = recruiterId.Value;
            model.DateSent = DateTime.Now;

            _context.Contacts.Add(model);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Message sent successfully!";
            return RedirectToAction("ConsulterPortfolio", new { id = model.CreatorId });
        }


        public IActionResult DownloadCV(int id)
        {
            var creator = _context.Creators
        .Include(c => c.User)
        .FirstOrDefault(c => c.Id == id);

            if (creator == null)
                return NotFound();

            var model = new ConsulterPortfolioVM
            {
                Creator = creator,
                Experiences = _context.Experiences.Where(e => e.CreatorId == id).ToList(),
                Projects = _context.Projects.Where(p => p.CreatorId == id).ToList(),
                Competences = _context.Competences.Where(c => c.CreatorId == id).ToList(),
                Langues = _context.Langue.Where(l => l.CreatorId == id).ToList(),
                Certifications = _context.Certifications.Where(c => c.CreatorId == id).ToList()
            };

            var document = new CreatorCvDocument(model);

            byte[] pdf = document.GeneratePdf();

            return File(pdf, "application/pdf",
                $"CV_{creator.User.FirstName}_{creator.User.LastName}.pdf");
        }

    }
}
