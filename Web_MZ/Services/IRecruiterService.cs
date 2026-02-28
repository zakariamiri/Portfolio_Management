using Web_MZ.Entities;
using Web_MZ.Models;

namespace Web_MZ.Services
{
    public interface IRecruiterService
    {
        Recruiter CreateRecruiter(int userId, RecruiterViewModel model);
        Recruiter GetRecruiterByUserId(int recruiterId);
    }
}
