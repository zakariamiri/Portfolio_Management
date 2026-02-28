using Web_MZ.Entities;
using Web_MZ.Models;
using Web_MZ.Repository;

namespace Web_MZ.Services
{
    public class CertificationService : ICertificationService
    {
        private readonly ICertificationRepository _repo;

        public CertificationService(ICertificationRepository repo)
        {
            _repo = repo;
        }

        public List<Certification> GetMyCertifs(int creatorId)
            => _repo.GetByCreator(creatorId);

        public void Create(CertificationViewModel vm)
        {
            var cert = new Certification
            {
                Title = vm.Title,
                Issuer = vm.Issuer,
                CredentialUrl = vm.CredentialUrl,
                CredentialId = vm.CredentialId,
                DateIssued = vm.DateIssued,
                CreatorId = vm.CreatorId
            };
            _repo.Add(cert);
        }

        public Certification GetById(int id)
        {
            return _repo.GetById(id);
        }


        public void Update(CertificationViewModel vm)
        {
            var cert = _repo.GetById(vm.Id);
            if (cert == null) return;

            cert.Title = vm.Title;
            cert.Issuer = vm.Issuer;
            cert.CredentialUrl = vm.CredentialUrl;
            cert.CredentialId = vm.CredentialId;
            cert.DateIssued = vm.DateIssued;

            _repo.Update(cert);
        }

        public void Delete(int id) => _repo.Delete(id);
    }
}
