using MarvelApp.Application.DTOs;
using MarvelApp.Application.Ports;
using MediatR;

namespace MarvelApp.Application.Queries.GetComicById
{
    public class GetComicByIdHandler : IRequestHandler<GetComicByIdQuery, ApiResponse<ComicDto>>
    {
        private readonly IMarvelApiService marvelApiService;
        public GetComicByIdHandler(IMarvelApiService marvelApiService)
        {
            this.marvelApiService = marvelApiService;
        }
        public async Task<ApiResponse<ComicDto>> Handle(GetComicByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ComicDto? comicDto = await marvelApiService.GetOneAsync(request.comicId);
                
                if(comicDto == null)
                {
                    throw new AggregateException("Comic no encontrado");
                }

                return ApiResponse<ComicDto>.Success(comicDto, "Comic encontrado");
            }
            catch (Exception ex)
            {
                return ApiResponse<ComicDto>.Error(ex.Message);
            }
        }
    }
}
