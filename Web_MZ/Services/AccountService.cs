using Web_MZ.Entities;
using Web_MZ.Repository;

namespace Web_MZ.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public User GetUserById(int userId)
        {
            return _repo.GetById(userId);
        }

        public void UpdateAccount(User user)
        {
            _repo.Update(user);
        }
    }
}
