using MarvelApp.Domain.Entities;
using MarvelApp.Domain.Ports;
using MarvelApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MarvelApp.Infrastructure.Adapters
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            return await dbContext.users.Where(x => x.Email.Value.Equals(email)).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(User user)
        {
            dbContext.users.Add(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
