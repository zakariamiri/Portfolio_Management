using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Web_MZ.Data;
using Web_MZ.Entities;
using Web_MZ.Models;
using Web_MZ.Repository;

namespace Web_MZ.Services
{
    public class CreatorService : ICreatorService
    {
        private readonly ICreatorRepository _repo;
        private readonly IWebHostEnvironment _env;
        private readonly MyDbContext _context;

        public CreatorService(ICreatorRepository repo, IWebHostEnvironment env, MyDbContext context)
        {
            _repo = repo;
            _env = env;
            _context = context;

        }


        public Creator GetCreatorByUserId(int creatorId)
        {
            return _repo.GetByUserId(creatorId);
        }

        public Creator CreateCreator(int userId, CreatorViewModel model)
        {
            string imagePath = null;

            if (model.ProfileImage != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(model.ProfileImage.FileName);
                string folder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(stream);
                }

                imagePath = "/uploads/" + fileName;
            }

            var creator = new Creator
            {
                UserId = userId,
                Bio = model.Bio,
                LinkedIn = model.LinkedIn,
                GitHub = model.GitHub,
                ProfileImage = imagePath
            };

            _repo.Add(creator);
            _repo.Save();

            return creator;
        }

        public Creator GetCreatorWithUserById(int creatorId)
        {
            return _context.Creators
                .Include(c => c.User)
                .FirstOrDefault(c => c.Id == creatorId);
        }



    }
}
