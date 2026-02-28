using Web_MZ.Data;
using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly MyDbContext _context;

        public ExperienceRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<Experience> GetByCreator(int creatorId)
            => _context.Experiences
                       .Where(e => e.CreatorId == creatorId)
                       .ToList();

        public Experience GetById(int id)
            => _context.Experiences.Find(id);

        public void Add(Experience exp)
        {
            _context.Experiences.Add(exp);
            _context.SaveChanges();
        }

        public void Update(Experience exp)
        {
            _context.Experiences.Update(exp);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var exp = GetById(id);
            if (exp != null)
            {
                _context.Experiences.Remove(exp);
                _context.SaveChanges();
            }
        }
    }
}
