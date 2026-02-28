using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public interface IProjectRepository
    {
        List<Project> GetByCreator(int creatorId);
        Project GetById(int id);
        void Add(Project prj);
        void Update(Project prj);
        void Delete(int id);
    }
}
