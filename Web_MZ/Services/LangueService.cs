using Web_MZ.Entities;
using Web_MZ.Models;
using Web_MZ.Repository;

namespace Web_MZ.Services
{
    public class LangueService : ILangueService
    {
        private readonly ILangueRepository _repo;

        public LangueService(ILangueRepository repo)
        {
            _repo = repo;
        }

        public void SaveLangues(int creatorId, LangueViewModel vm)
        {
            if (!string.IsNullOrEmpty(vm.Langue1))
                _repo.Add(new Langue { Nom = vm.Langue1, Niveau = vm.Niveau1, CreatorId = creatorId });

            if (!string.IsNullOrEmpty(vm.Langue2))
                _repo.Add(new Langue { Nom = vm.Langue2, Niveau = vm.Niveau2, CreatorId = creatorId });

            if (!string.IsNullOrEmpty(vm.Langue3))
                _repo.Add(new Langue { Nom = vm.Langue3, Niveau = vm.Niveau3, CreatorId = creatorId });
        }

        public async Task<List<Langue>> GetLanguesForCreatorAsync(int creatorId)
        {
            return await _repo.GetLanguesByCreatorIdAsync(creatorId);
        }
    }

}
