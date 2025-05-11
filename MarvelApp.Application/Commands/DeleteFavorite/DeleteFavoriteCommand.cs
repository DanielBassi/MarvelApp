using MediatR;

namespace MarvelApp.Application.Commands.DeleteFavorite
{
    public record DeleteFavoriteCommand(int comicId) : IRequest<ApiResponse<string>>;
}
