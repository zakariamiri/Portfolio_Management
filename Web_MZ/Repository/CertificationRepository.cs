using Web_MZ.Data;
using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public class CertificationRepository : ICertificationRepository
    {

        private readonly MyDbContext _context;

        public CertificationRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<Certification> GetByCreator(int creatorId)
            => _context.Certifications
                       .Where(c => c.CreatorId == creatorId)
                       .ToList();

        public Certification GetById(int id)
            => _context.Certifications.Find(id);

        public void Add(Certification cert)
        {
            _context.Certifications.Add(cert);
            _context.SaveChanges();
        }

        public void Update(Certification cert)
        {
            _context.Certifications.Update(cert);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var cert = GetById(id);
            if (cert != null)
            {
                _context.Certifications.Remove(cert);
                _context.SaveChanges();
            }
        }

    }
}
