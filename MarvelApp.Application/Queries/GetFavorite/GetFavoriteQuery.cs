using MarvelApp.Application.DTOs;
using MediatR;

namespace MarvelApp.Application.Queries.GetFavorite
{
    public record GetFavoriteQuery() : IRequest<ApiResponse<List<ComicDto>?>>;
}
