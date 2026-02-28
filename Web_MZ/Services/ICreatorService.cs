using Web_MZ.Entities;
using Web_MZ.Models;

namespace Web_MZ.Services
{
    public interface ICreatorService
    {
        Creator CreateCreator(int userId, CreatorViewModel model);
        Creator GetCreatorByUserId(int creatorId);

        Creator GetCreatorWithUserById(int creatorId);
    }
}
