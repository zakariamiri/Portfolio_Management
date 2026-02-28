using Web_MZ.Data;
using Web_MZ.Entities;

namespace Web_MZ.Services
{
    public class LoginService : ILoginService
    {

        private readonly MyDbContext _context;

        public LoginService(MyDbContext context)
        {
            _context = context;
        }

      public User Authenticate(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password); //cherche le premier enregistrement
        }



    }
}
