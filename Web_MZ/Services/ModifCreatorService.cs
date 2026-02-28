using Web_MZ.Entities;
using Web_MZ.Repository;

namespace Web_MZ.Services
{
    public class ModifCreatorService : IModifCreatorService
    {
        private readonly IModifCreatorRepository _repo;

        public ModifCreatorService(IModifCreatorRepository repo)
        {
            _repo = repo;
        }

        public Creator GetCreatorById(int creatorId)
        {
            return _repo.GetByCreatorId(creatorId);
        }

        public void UpdateCreator(Creator creator)
        {
            _repo.Update(creator);
        }
    }
}
