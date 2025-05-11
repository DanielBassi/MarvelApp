using MarvelApp.Domain.Entities;

namespace MarvelApp.Domain.Ports
{
    public interface IJwtTokenGenerator
    {
        string GenerarToken(User user);
    }
}
