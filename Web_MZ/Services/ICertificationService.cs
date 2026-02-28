using Web_MZ.Entities;
using Web_MZ.Models;

namespace Web_MZ.Services
{
    public interface ICertificationService
    {
        List<Certification> GetMyCertifs(int creatorId);
        Certification GetById(int id);
        void Create(CertificationViewModel vm);
        void Update(CertificationViewModel vm);
        void Delete(int id);
    }
}
