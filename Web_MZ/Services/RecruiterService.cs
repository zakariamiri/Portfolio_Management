using Microsoft.AspNetCore.Http.HttpResults;
using Web_MZ.Entities;
using Web_MZ.Models;
using Web_MZ.Repository;

namespace Web_MZ.Services
{
    public class RecruiterService : IRecruiterService
    {
        private readonly IRecruiterRepository _repo;
        private readonly IWebHostEnvironment _env;

        public RecruiterService(IRecruiterRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }


        public Recruiter GetRecruiterByUserId(int recruiterId)
        {
            return _repo.GetByUserId(recruiterId);
        }

        public Recruiter CreateRecruiter(int userId, RecruiterViewModel model)
        {
            string imagePath = null;

            if (model.ProfileImage != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(model.ProfileImage.FileName);
                string folder = Path.Combine(_env.WebRootPath, "recruiter");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(stream);
                }

                imagePath = "/recruiter/" + fileName;
            }

            var recruiter = new Recruiter
            {
                UserId = userId,
                Bio = model.Bio,
                CompanyName = model.CompanyName,
                Position = model.Position,
                ProfileImage = imagePath
            };

            _repo.Add(recruiter);
            _repo.Save();

            return recruiter;
        }

    }
}
