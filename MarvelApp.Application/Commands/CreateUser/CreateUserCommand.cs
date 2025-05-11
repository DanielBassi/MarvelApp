using MarvelApp.Application.DTOs;
using MediatR;

namespace MarvelApp.Application.Commands.CreateUser
{
    public record CreateUserCommand(RegisterDto data) : IRequest<ApiResponse<Guid>>;
}
