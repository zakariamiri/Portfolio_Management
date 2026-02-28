using Web_MZ.Entities;

namespace Web_MZ.Services
{
    public interface IModifCreatorService 
    {
        Creator GetCreatorById(int creatorId);
        void UpdateCreator(Creator creator);
    }
}
