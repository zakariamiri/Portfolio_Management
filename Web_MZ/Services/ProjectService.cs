using Web_MZ.Data;
using Web_MZ.Entities;
using Web_MZ.Models;
using Web_MZ.Repository;

namespace Web_MZ.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;

        public ProjectService(IProjectRepository repo)
        {
            _repo = repo;
        }

        public List<Project> GetMyProjet(int creatorId)
            => _repo.GetByCreator(creatorId);

        public void Create(ProjectViewModel vm)
        {
            var prj = new Project
            {
                Title = vm.Title,
                Description = vm.Description,
                Technologies = vm.Technologies,
                Link = vm.Link,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                CreatorId = vm.CreatorId
            };
            _repo.Add(prj);
        }

        public Project GetById(int id)
        {
            return _repo.GetById(id);
        }


        public void Update(ProjectViewModel vm)
        {
            var prj = _repo.GetById(vm.Id);
            if (prj == null) return;

            prj.Title = vm.Title;
            prj.Description = vm.Description;
            prj.Technologies = vm.Technologies;
            prj.Link = vm.Link;
            prj.StartDate = vm.StartDate;
            prj.EndDate = vm.EndDate;


            _repo.Update(prj);
        }

        public void Delete(int id) => _repo.Delete(id);
    }
}
