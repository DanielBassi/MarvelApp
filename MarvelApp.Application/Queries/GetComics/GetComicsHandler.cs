using MarvelApp.Application.DTOs;
using MarvelApp.Application.Ports;
using MarvelApp.Domain.Entities;
using MarvelApp.Domain.Ports;
using MediatR;

namespace MarvelApp.Application.Queries.GetComics
{
    public class GetComicsHandler : IRequestHandler<GetComicsQuery, ApiResponse<List<ComicDto>>>
    {
        private readonly IMarvelApiService marvelApiService;
        private readonly IFavoriteRepository favoriteRepository;
        private readonly IUserContextService userContextService;
        public GetComicsHandler(IMarvelApiService marvelApiService, IFavoriteRepository favoriteRepository, IUserContextService userContextService)
        {
            this.marvelApiService = marvelApiService;
            this.favoriteRepository = favoriteRepository;
            this.userContextService = userContextService;
        }
        public async Task<ApiResponse<List<ComicDto>>> Handle(GetComicsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<ComicDto>? comics = await marvelApiService.GetListAsync();
                
                if (comics == null)
                {
                    return ApiResponse<List<ComicDto>>.NoContent("No hay comics que mostrar");
                }

                List<Favorite> favorites = await favoriteRepository.GetAsync(userContextService.GetUserId());

                comics?.Select(s =>
                {
                    s.isFavorite = favorites.Any(a => a.ComicId.Equals(s.Id));
                    return s;
                }).ToList();

                return ApiResponse<List<ComicDto>>.Success(comics, "Listado de comics");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ComicDto>>.Error(ex.Message);
            }
        }
    }
}
