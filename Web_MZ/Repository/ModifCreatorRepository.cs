using Web_MZ.Data;
using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public class ModifCreatorRepository : IModifCreatorRepository
    {
        private readonly MyDbContext _context;

        public ModifCreatorRepository(MyDbContext context)
        {
            _context = context;
        }

        public Creator GetByCreatorId(int creatorId)
        {
            return _context.Creators.FirstOrDefault(c => c.Id == creatorId);
        }

        public void Update(Creator creator)
        {
            _context.Creators.Update(creator);
            _context.SaveChanges();
        }
    }
}
