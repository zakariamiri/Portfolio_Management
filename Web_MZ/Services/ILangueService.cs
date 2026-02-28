using Web_MZ.Entities;
using Web_MZ.Models;

namespace Web_MZ.Services
{
    public interface ILangueService
    {
        void SaveLangues(int creatorId, LangueViewModel vm);
        Task<List<Langue>> GetLanguesForCreatorAsync(int creatorId);
    }
}
