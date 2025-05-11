using MarvelApp.Domain.Entities;

namespace MarvelApp.Domain.Ports
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User?> GetUserByEmail(string email);
    }
}
