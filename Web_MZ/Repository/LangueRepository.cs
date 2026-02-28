using Microsoft.EntityFrameworkCore;
using Web_MZ.Data;
using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public class LangueRepository : ILangueRepository
    {
        private readonly MyDbContext _context;

        public LangueRepository(MyDbContext context)
        {
            _context = context;
        }

        public void Add(Langue langue)
        {
            _context.Langue.Add(langue);
            _context.SaveChanges();
        }

        public async Task<List<Langue>> GetLanguesByCreatorIdAsync(int creatorId)
        {
            return await _context.Langue
                .Where(l => l.CreatorId == creatorId)
                .ToListAsync();
        }
    }
}
