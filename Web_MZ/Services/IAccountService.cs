using Web_MZ.Entities;

namespace Web_MZ.Services
{
    public interface IAccountService
    {
        User GetUserById(int userId);
        void UpdateAccount(User user);
    }
}
