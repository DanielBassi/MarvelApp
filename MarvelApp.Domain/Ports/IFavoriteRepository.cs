using MarvelApp.Domain.Entities;

namespace MarvelApp.Domain.Ports
{
    public interface IFavoriteRepository
    {
        Task<string> CreateAsync(int comicId, Guid userId);
        Task<List<Favorite>> GetAsync(Guid userId);
        Task<Favorite?> GetAsync(Guid userId, int comicId);
        Task RemoveAsync(Favorite favorite);
    }
}
