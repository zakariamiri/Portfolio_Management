using Web_MZ.Entities;
using Web_MZ.Models;
using Web_MZ.Repository;

namespace Web_MZ.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _repo;

        public ExperienceService(IExperienceRepository repo)
        {
            _repo = repo;
        }

        public List<Experience> GetMyExperience(int creatorId)
            => _repo.GetByCreator(creatorId);

        public void Create(ExperienceViewModel vm)
        {
            var exp = new Experience
            {
                Title = vm.Title,
                Company = vm.Company,
                Location = vm.Location,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                Description = vm.Description,
                CreatorId = vm.CreatorId
            };
            _repo.Add(exp);
        }

        public Experience GetById(int id)
        {
            return _repo.GetById(id);
        }


        public void Update(ExperienceViewModel vm)
        {
            var exp = _repo.GetById(vm.Id);
            if (exp == null) return;

            exp.Title = vm.Title;
            exp.Company = vm.Company;
            exp.Location = vm.Location;
            exp.StartDate = vm.StartDate;
            exp.EndDate = vm.EndDate;
            exp.Description = vm.Description;


            _repo.Update(exp);
        }

        public void Delete(int id) => _repo.Delete(id);
    }
}
