using Web_MZ.Entities;
using Web_MZ.Models;

namespace Web_MZ.Services
{
    public interface IExperienceService
    {
        List<Experience> GetMyExperience(int creatorId);
        Experience GetById(int id);
        void Create(ExperienceViewModel vm);
        void Update(ExperienceViewModel vm);
        void Delete(int id);
    }
}
