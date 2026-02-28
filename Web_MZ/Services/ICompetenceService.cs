using Web_MZ.Entities;
using Web_MZ.Models;

namespace Web_MZ.Services
{
    public interface ICompetenceService
    {
        List<Competence> GetMyCompetence(int creatorId);
        Competence GetById(int id);
        void Create(CompetenceViewModel vm);
        void Update(CompetenceViewModel vm);
        void Delete(int id);
    }
}
