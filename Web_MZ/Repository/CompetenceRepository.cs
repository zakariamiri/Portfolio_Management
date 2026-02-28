using Web_MZ.Data;
using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public class CompetenceRepository : ICompetenceRepository
    {
        private readonly MyDbContext _context;

        public CompetenceRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<Competence> GetByCreator(int creatorId)
            => _context.Competences
                       .Where(c => c.CreatorId == creatorId)
                       .ToList();

        public Competence GetById(int id)
            => _context.Competences.Find(id);

        public void Add(Competence comp)
        {
            _context.Competences.Add(comp);
            _context.SaveChanges();
        }

        public void Update(Competence comp)
        {
            _context.Competences.Update(comp);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var comp = GetById(id);
            if (comp != null)
            {
                _context.Competences.Remove(comp);
                _context.SaveChanges();
            }
        }

    }
}
