using MarvelApp.Domain.Entities;
using MarvelApp.Domain.Ports;
using MarvelApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MarvelApp.Infrastructure.Adapters
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext dbContext;
        public FavoriteRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<string> CreateAsync(int comicId, Guid userId)
        {
            Favorite favorite = new Favorite();
            favorite.Id = Guid.NewGuid();
            favorite.ComicId = comicId;
            favorite.UserId = userId;
            dbContext.favorites.Add(favorite);
            await dbContext.SaveChangesAsync();
            return favorite.Id.ToString();
        }

        public async Task<List<Favorite>> GetAsync(Guid userId) =>
            await dbContext.favorites.Where(w => w.UserId.Equals(userId)).ToListAsync();

        public async Task<Favorite?> GetAsync(Guid userId, int comicId)
        {
            return await dbContext.favorites.FirstOrDefaultAsync(f => f.UserId.Equals(userId) && f.ComicId.Equals(comicId));
        }

        public async Task RemoveAsync(Favorite favorite)
        {
            dbContext.favorites.Remove(favorite);
            await dbContext.SaveChangesAsync();
        }
    }
}
