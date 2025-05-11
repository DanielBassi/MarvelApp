using MarvelApp.Application.DTOs;
using MediatR;

namespace MarvelApp.Application.Commands.CreateFavorite
{
    public record CreateFavoriteCommand(FavoriteDto data) : IRequest<ApiResponse<string>>;
}
