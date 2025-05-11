using MarvelApp.Application.Ports;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MarvelApp.Infrastructure.Adapters
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            string result = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            return Guid.Parse(result);
        }
    }
}
