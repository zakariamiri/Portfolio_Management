using Web_MZ.Data;
using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public class ProjectRepository: IProjectRepository
    {

        private readonly MyDbContext _context;

        public ProjectRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<Project> GetByCreator(int creatorId)
            => _context.Projects
                       .Where(p => p.CreatorId == creatorId)
                       .ToList();

        public Project GetById(int id)
            => _context.Projects.Find(id);

        public void Add(Project prj)
        {
            _context.Projects.Add(prj);
            _context.SaveChanges();
        }

        public void Update(Project prj)
        {
            _context.Projects.Update(prj);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var prj = GetById(id);
            if (prj != null)
            {
                _context.Projects.Remove(prj);
                _context.SaveChanges();
            }
        }

    }
}
