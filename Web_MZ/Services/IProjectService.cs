using Web_MZ.Entities;
using Web_MZ.Models;

namespace Web_MZ.Services
{
    public interface IProjectService
    {
        List<Project> GetMyProjet(int creatorId);
        Project GetById(int id);
        void Create(ProjectViewModel vm);
        void Update(ProjectViewModel vm);
        void Delete(int id);
    }
}
