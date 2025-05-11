using MarvelApp.Application.DTOs;
using MediatR;

namespace MarvelApp.Application.Queries.GetComics
{
    public record GetComicsQuery() : IRequest<ApiResponse<List<ComicDto>>>;
}
