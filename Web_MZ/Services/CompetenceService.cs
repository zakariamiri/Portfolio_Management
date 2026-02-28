using Web_MZ.Entities;
using Web_MZ.Models;
using Web_MZ.Repository;

namespace Web_MZ.Services
{
    public class CompetenceService : ICompetenceService
    {
        private readonly ICompetenceRepository _repo;

        public CompetenceService(ICompetenceRepository repo)
        {
            _repo = repo;
        }

        public List<Competence> GetMyCompetence(int creatorId)
            => _repo.GetByCreator(creatorId);

        public void Create(CompetenceViewModel vm)
        {
            var comp = new Competence
            {
                Name = vm.Name,
                Level = vm.Level,
                Category = vm.Category,
                CreatorId = vm.CreatorId
            };
            _repo.Add(comp);
        }

        public Competence GetById(int id)
        {
            return _repo.GetById(id);
        }


        public void Update(CompetenceViewModel vm)
        {
            var comp = _repo.GetById(vm.Id);
            if (comp == null) return;

            comp.Name = vm.Name;
            comp.Level = vm.Level;
            comp.Category = vm.Category;
         

            _repo.Update(comp);
        }

        public void Delete(int id) => _repo.Delete(id);
    }
}
