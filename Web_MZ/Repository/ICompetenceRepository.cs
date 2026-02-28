using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public interface ICompetenceRepository
    {
        List<Competence> GetByCreator(int creatorId);
        Competence GetById(int id);
        void Add(Competence cert);
        void Update(Competence cert);
        void Delete(int id);
    }
}
