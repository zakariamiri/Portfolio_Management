using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public interface IExperienceRepository
    {
        List<Experience> GetByCreator(int creatorId);
        Experience GetById(int id);
        void Add(Experience exp);
        void Update(Experience exp);
        void Delete(int id);
    }
}
