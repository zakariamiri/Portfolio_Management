using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public interface IRecruiterRepository
    {
        void Add(Recruiter recruiter);
        Recruiter GetByUserId(int recruiterId);
        void Save();
    }
}
