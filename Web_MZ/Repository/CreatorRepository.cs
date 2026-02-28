using Web_MZ.Data;
using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public class CreatorRepository : ICreatorRepository
    {
        private readonly MyDbContext _context;

        public CreatorRepository(MyDbContext context)
        {
            _context = context;
        }

        public void Add(Creator creator)
        {
            _context.Creators.Add(creator);
        }

        public Creator GetByUserId(int creatorId)
        {
            return _context.Creators.FirstOrDefault(c => c.Id == creatorId);
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
