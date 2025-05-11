using MarvelApp.Application.DTOs;
using MediatR;

namespace MarvelApp.Application.Queries.GetComicById
{
    public record GetComicByIdQuery(int comicId) : IRequest<ApiResponse<ComicDto>>;
}
