using MarvelApp.Application.DTOs;
using MediatR;

namespace MarvelApp.Application.Queries.GetAuthUser
{
    public record GetAuthUserQuery(LoginDto data) : IRequest<ApiResponse<string>>;
}
