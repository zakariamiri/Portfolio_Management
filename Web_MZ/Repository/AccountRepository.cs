using Web_MZ.Data;
using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyDbContext _context;

        public AccountRepository(MyDbContext context)
        {
            _context = context;
        }

        public User GetById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
