using Web_MZ.Entities;

namespace Web_MZ.Services
{
    public interface ILoginService
    {
            User Authenticate(string email, string password);
        
    }
}
