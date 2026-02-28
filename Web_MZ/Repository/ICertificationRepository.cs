using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public interface ICertificationRepository
    {
        List<Certification> GetByCreator(int creatorId);
        Certification GetById(int id);
        void Add(Certification cert);
        void Update(Certification cert);
        void Delete(int id);
    }
}
