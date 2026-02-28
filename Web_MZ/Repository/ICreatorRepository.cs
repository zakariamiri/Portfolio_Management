using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public interface ICreatorRepository
    {
        void Add(Creator creator);
        Creator GetByUserId(int creatorId);
        void Save();
    }
}
