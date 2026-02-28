using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public interface IAccountRepository
    {
        User GetById(int userId);
        void Update(User user);
    }
}
