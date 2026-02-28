using Web_MZ.Data;
using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public class RecruiterRepository : IRecruiterRepository
    {
        private readonly MyDbContext _context;

        public RecruiterRepository(MyDbContext context)
        {
            _context = context;
        }

        public void Add(Recruiter recruiter)
        {
            _context.Recruiters.Add(recruiter);
        }

        public Recruiter GetByUserId(int recruiterId)
        {
            return _context.Recruiters.FirstOrDefault(r => r.Id == recruiterId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
