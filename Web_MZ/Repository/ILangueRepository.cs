using Web_MZ.Entities;

namespace Web_MZ.Repository
{
    public interface ILangueRepository
    {
        void Add(Langue langue);
        Task<List<Langue>> GetLanguesByCreatorIdAsync(int creatorId);
    }
}
