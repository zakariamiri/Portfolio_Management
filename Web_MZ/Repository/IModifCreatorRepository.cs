using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public interface IModifCreatorRepository
    {
        Creator GetByCreatorId(int creatorId);
        void Update(Creator creator);
    }
}
