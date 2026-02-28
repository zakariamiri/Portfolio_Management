using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_MZ.Data;
using Web_MZ.Entities;
using Web_MZ.Models;
using Web_MZ.Pdf;
using Web_MZ.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Web_MZ.Controllers
{

    public class DashboardController : Controller
    {
        private readonly ICreatorService _creatorService;
        private readonly ILangueService _langueService;
        private readonly ICertificationService _certifService;
        private readonly IProjectService _ProjetService;
        private readonly IExperienceService _ExperienceService;
        private readonly ICompetenceService _CompetenceService;
        private readonly IModifCreatorService _ModifCreatorService;
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _env;



        public DashboardController(ICreatorService creatorService, ILangueService langueService, ICertificationService certifService, IProjectService projetService, IExperienceService experienceService, ICompetenceService competenceService, IModifCreatorService ModifCreatorService, MyDbContext context, IWebHostEnvironment env)
        {
            _creatorService = creatorService;
            _langueService = langueService;
            _certifService = certifService;
            _ProjetService = projetService;
            _ExperienceService = experienceService;
            _CompetenceService = competenceService;
            _ModifCreatorService = ModifCreatorService;
            _context = context;
            _env = env;
        }
        // GET: DashboardController
        public IActionResult Dash()
        {
            ViewBag.FirstName = HttpContext.Session.GetString("UserFirstName");
            ViewBag.LastName = HttpContext.Session.GetString("UserLastName");
            int? creatorId = HttpContext.Session.GetInt32("CreatorId");

            var creator = _creatorService.GetCreatorByUserId(creatorId.Value);

            if (creator == null)
                return RedirectToAction("SecondDemon", "Demon");

            ViewBag.Bio = creator.Bio;
            ViewBag.ProfileImage = creator.ProfileImage;
            ViewBag.LinkedIn = creator.LinkedIn;
            ViewBag.GitHub = creator.GitHub;

            var prj = _ProjetService.GetMyProjet(creatorId.Value);
            var exp = _ExperienceService.GetMyExperience(creatorId.Value);
            var comp = _CompetenceService.GetMyCompetence(creatorId.Value);
            var certs = _certifService.GetMyCertifs(creatorId.Value);
           
            // Nombre total de certifications
            ViewBag.TotalCertifications = certs.Count;
            // Nombre total de Experiences 
            ViewBag.TotalExperiences = exp.Count;
            // Nombre total de Projets
            ViewBag.TotalProjets = prj.Count;
            // Nombre total de Competences
            ViewBag.TotalCompetences = comp.Count;

            return View();
        }

        public async Task<IActionResult> Profil()
        {
            ViewBag.FirstName = HttpContext.Session.GetString("UserFirstName");
            ViewBag.LastName = HttpContext.Session.GetString("UserLastName");
            ViewBag.Phone = HttpContext.Session.GetInt32("UserPhone");
            ViewBag.Email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Date = HttpContext.Session.GetString("UserDateCreated");
            ViewBag.Country = HttpContext.Session.GetString("UserCountry");


            int? creatorId = HttpContext.Session.GetInt32("CreatorId");
            if (creatorId == null)
                return RedirectToAction("Login", "Account");

            var creator = _creatorService.GetCreatorByUserId(creatorId.Value);

            if (creator == null)
                return RedirectToAction("SecondDemon", "Demon");

      

            var model = new ProfilViewModel
            {
                Creator = creator,
                Experiences = _ExperienceService.GetMyExperience(creatorId.Value) ?? new List<Experience>(),
                Projects = _ProjetService.GetMyProjet(creatorId.Value) ?? new List<Project>(),
                Competences = _CompetenceService.GetMyCompetence(creatorId.Value) ?? new List<Competence>(),
                Certifications = _certifService.GetMyCertifs(creatorId.Value) ?? new List<Certification>(),
                Langues = await _langueService.GetLanguesForCreatorAsync(creatorId.Value) ?? new List<Langue>()
            };

            // Retourne la vue avec le modèle
            return View(model);
        }

        // POST Edit
        [HttpPost]
        public IActionResult EditProfile(ProfilViewModel model, IFormFile ProfileImageFile)
        {
            if (model.Creator == null)
                return BadRequest();

            var creator = _ModifCreatorService.GetCreatorById(model.Creator.Id);
            if (creator == null) return NotFound();

            creator.Bio = model.Creator.Bio;
            creator.LinkedIn = model.Creator.LinkedIn;
            creator.GitHub = model.Creator.GitHub;

            if (ProfileImageFile != null && ProfileImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(ProfileImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                ProfileImageFile.CopyTo(stream);

                creator.ProfileImage = "/uploads/" + fileName;
            }


            _ModifCreatorService.UpdateCreator(creator);

            return RedirectToAction(nameof(Profil));
        }




        public IActionResult Certif()
        {
            ViewBag.FirstName = HttpContext.Session.GetString("UserFirstName");
            ViewBag.LastName = HttpContext.Session.GetString("UserLastName");

            int? creatorId = HttpContext.Session.GetInt32("CreatorId");
            if (creatorId == null)
                return RedirectToAction("Login", "Account");

            var creator = _creatorService.GetCreatorByUserId(creatorId.Value);

            if (creator == null)
                return RedirectToAction("SecondDemon", "Demon");

            ViewBag.Bio = creator.Bio;
            ViewBag.ProfileImage = creator.ProfileImage;
            var certifs = _certifService.GetMyCertifs(creatorId.Value);
            return View(certifs);
        }

        [HttpPost]
        public IActionResult AddCertif(CertificationViewModel vm)
        {
            int? creatorId = HttpContext.Session.GetInt32("CreatorId");
            if (creatorId == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return RedirectToAction("Certif");

            vm.CreatorId = creatorId.Value; 

            _certifService.Create(vm);
            return RedirectToAction("Certif");
        }


        // GET : afficher formulaire avec données existantes
        public IActionResult EditCertif(int id)
        {
            var cert = _certifService.GetById(id);
            if (cert == null) return NotFound();

            var vm = new CertificationViewModel
            {
                Id = cert.Id,
                Title = cert.Title,
                Issuer = cert.Issuer,
                CredentialUrl = cert.CredentialUrl,
                DateIssued = cert.DateIssued
            };

            return View(vm);
        }

        // POST : sauvegarder modification
        [HttpPost]
        public IActionResult EditCertif(CertificationViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            _certifService.Update(vm);
            return RedirectToAction("Certif");
        }


        [HttpPost]
        public IActionResult DeleteCertif(int id)
        {
            _certifService.Delete(id);
            return RedirectToAction("Certif");
        }





        public IActionResult Projet()
        {
            ViewBag.FirstName = HttpContext.Session.GetString("UserFirstName");
            ViewBag.LastName = HttpContext.Session.GetString("UserLastName");

            int? creatorId = HttpContext.Session.GetInt32("CreatorId");
            if (creatorId == null)
                return RedirectToAction("Login", "Account");

            var creator = _creatorService.GetCreatorByUserId(creatorId.Value);

            if (creator == null)
                return RedirectToAction("SecondDemon", "Demon");

            ViewBag.Bio = creator.Bio;
            ViewBag.ProfileImage = creator.ProfileImage;
            var prj = _ProjetService.GetMyProjet(creatorId.Value);
            return View(prj);
        }


        public IActionResult AddProjet(ProjectViewModel vm)
        {
            int? creatorId = HttpContext.Session.GetInt32("CreatorId");
            if (creatorId == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return RedirectToAction("Projet");

            vm.CreatorId = creatorId.Value;

            _ProjetService.Create(vm);
            return RedirectToAction("Projet");
        }


        // GET : afficher formulaire avec données existantes
        public IActionResult EditProjet(int id)
        {
            var prj = _ProjetService.GetById(id);
            if (prj == null) return NotFound();

            var vm = new ProjectViewModel
            {
                Id = prj.Id,
                Title = prj.Title,
                Description = prj.Description,
                Technologies = prj.Technologies,
                Link = prj.Link,
                StartDate = prj.StartDate,
                EndDate = prj.EndDate

            };

            return View(vm);
        }

        // POST : sauvegarder modification
        [HttpPost]
        public IActionResult EditProjet(ProjectViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            _ProjetService.Update(vm);
            return RedirectToAction("Projet");
        }


        [HttpPost]
        public IActionResult DeleteProjet(int id)
        {
            _ProjetService.Delete(id);
            return RedirectToAction("Projet");
        }






        public IActionResult Experience()
        {
            ViewBag.FirstName = HttpContext.Session.GetString("UserFirstName");
            ViewBag.LastName = HttpContext.Session.GetString("UserLastName");

            int? creatorId = HttpContext.Session.GetInt32("CreatorId");
            if (creatorId == null)
                return RedirectToAction("Login", "Account");

            var creator = _creatorService.GetCreatorByUserId(creatorId.Value);

            if (creator == null)
                return RedirectToAction("SecondDemon", "Demon");

            ViewBag.Bio = creator.Bio;
            ViewBag.ProfileImage = creator.ProfileImage;
            var exp = _ExperienceService.GetMyExperience(creatorId.Value);
            return View(exp);
        }


        public IActionResult AddExperience(ExperienceViewModel vm)
        {
            int? creatorId = HttpContext.Session.GetInt32("CreatorId");
            if (creatorId == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return RedirectToAction("Experience");

            vm.CreatorId = creatorId.Value;

            _ExperienceService.Create(vm);
            return RedirectToAction("Experience");
        }


        // GET : afficher formulaire avec données existantes
        public IActionResult EditExperience(int id)
        {
            var exp = _ExperienceService.GetById(id);
            if (exp == null) return NotFound();

            var vm = new ExperienceViewModel
            {
                Id = exp.Id,
                Title = exp.Title,
                Company = exp.Company,
                Location = exp.Location,
                StartDate = exp.StartDate,
                EndDate = exp.EndDate,
                Description = exp.Description,

            };

            return View(vm);
        }

        // POST : sauvegarder modification
        [HttpPost]
        public IActionResult EditExperience(ExperienceViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            _ExperienceService.Update(vm);
            return RedirectToAction("Experience");
        }


        [HttpPost]
        public IActionResult DeleteExperience(int id)
        {
            _ExperienceService.Delete(id);
            return RedirectToAction("Experience");
        }







        public IActionResult competence()
        {
            ViewBag.FirstName = HttpContext.Session.GetString("UserFirstName");
            ViewBag.LastName = HttpContext.Session.GetString("UserLastName");

            int? creatorId = HttpContext.Session.GetInt32("CreatorId");
            if (creatorId == null)
                return RedirectToAction("Login", "Account");

            var creator = _creatorService.GetCreatorByUserId(creatorId.Value);

            if (creator == null)
                return RedirectToAction("SecondDemon", "Demon");

            ViewBag.Bio = creator.Bio;
            ViewBag.ProfileImage = creator.ProfileImage;
            var comp = _CompetenceService.GetMyCompetence(creatorId.Value);
            return View(comp);
        }


        public IActionResult Addcompetence(CompetenceViewModel vm)
        {
            int? creatorId = HttpContext.Session.GetInt32("CreatorId");
            if (creatorId == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return RedirectToAction("competence");

            vm.CreatorId = creatorId.Value;

            _CompetenceService.Create(vm);
            return RedirectToAction("competence");
        }


        // GET : afficher formulaire avec données existantes
        public IActionResult Editcompetence(int id)
        {
            var comp = _CompetenceService.GetById(id);
            if (comp == null) return NotFound();

            var vm = new CompetenceViewModel
            {
                Id = comp.Id,
                Name = comp.Name,
                Level = comp.Level,
                Category = comp.Category
            };

            return View(vm);
        }

        // POST : sauvegarder modification
        [HttpPost]
        public IActionResult Editcompetence(CompetenceViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            _CompetenceService.Update(vm);
            return RedirectToAction("competence");
        }


        [HttpPost]
        public IActionResult Deletecompetence(int id)
        {
            _CompetenceService.Delete(id);
            return RedirectToAction("competence");
        }




        // Message 

        public IActionResult Message()
        {
            ViewBag.FirstName = HttpContext.Session.GetString("UserFirstName");
            ViewBag.LastName = HttpContext.Session.GetString("UserLastName");

            int? creatorId = HttpContext.Session.GetInt32("CreatorId");
            if (creatorId == null)
                return RedirectToAction("Login", "Account");

            var creator = _creatorService.GetCreatorByUserId(creatorId.Value);

            if (creator == null)
                return RedirectToAction("SecondDemon", "Demon");

            ViewBag.Bio = creator.Bio;
            ViewBag.ProfileImage = creator.ProfileImage;

            // Récupérer les messages depuis la base
            var messages = _context.Contacts
                .Include(c => c.Recruiter)
                .Where(c => c.CreatorId == creator.Id)
                .OrderByDescending(c => c.DateSent)
                .Select(c => new MessageVM
                {
                    RecruiterName = c.Recruiter.User.FirstName + " " + c.Recruiter.User.LastName,
                    Email = c.Recruiter.User.Email,
                    Phone = c.Recruiter.User.Phone.ToString(),
                    Position = c.Recruiter.Position,
                    MessageContent = c.Message,
                    DateSent = c.DateSent
                })
                .ToList();

            return View(messages);
        }


        // download Cv pour creator
        public async Task<IActionResult> DownloadCv(int creatorId)
        {
            var creator = _creatorService.GetCreatorWithUserById(creatorId);
            if (creator == null)
                return NotFound("Creator not found");

            var model = new ProfilViewModel
            {
                Creator = creator,
                Experiences = _ExperienceService.GetMyExperience(creatorId) ?? new(),
                Projects = _ProjetService.GetMyProjet(creatorId) ?? new(),
                Competences = _CompetenceService.GetMyCompetence(creatorId) ?? new(),
                Certifications = _certifService.GetMyCertifs(creatorId) ?? new(),
                Langues = await _langueService.GetLanguesForCreatorAsync(creatorId) ?? new()
            };

            var pdf = new CreatorCvPdf(model);
            var pdfBytes = pdf.GeneratePdf();

            return File(pdfBytes, "application/pdf",
                $"{creator.User.FirstName}_{creator.User.LastName}_CV.pdf");
        }



        // POST : Edit Account Information
        [HttpPost]
        public IActionResult EditAccount(string FirstName, string LastName, int Phone, string Country, string Email)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("login", "Login");

            var user = _context.Users.FirstOrDefault(u => u.Id == userId.Value);
            if (user == null)
                return NotFound();

            // Mise à jour
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Phone = Phone;
            user.Country = Country;
            user.Email = Email;

            _context.Users.Update(user);
            _context.SaveChanges();

            // Mise à jour de la session (OBLIGATOIRE)
            HttpContext.Session.SetString("UserFirstName", user.FirstName);
            HttpContext.Session.SetString("UserLastName", user.LastName);
            HttpContext.Session.SetInt32("UserPhone", user.Phone);
            HttpContext.Session.SetString("UserCountry", user.Country);
            HttpContext.Session.SetString("UserEmail", user.Email);

            return RedirectToAction(nameof(Profil));
        }





    }
}
