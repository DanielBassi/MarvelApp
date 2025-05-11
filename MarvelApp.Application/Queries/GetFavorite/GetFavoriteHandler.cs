using AutoMapper;
using MarvelApp.Application.DTOs;
using MarvelApp.Application.Ports;
using MarvelApp.Domain.Entities;
using MarvelApp.Domain.Ports;
using MediatR;

namespace MarvelApp.Application.Queries.GetFavorite
{
    public class GetFavoriteHandler : IRequestHandler<GetFavoriteQuery, ApiResponse<List<ComicDto>?>>
    {
        private readonly IFavoriteRepository favoriteRepository;
        private readonly IMarvelApiService marvelApiService;
        private readonly IUserContextService userContextService;
        public GetFavoriteHandler(IFavoriteRepository favoriteRepository, IUserContextService userContextService, IMarvelApiService marvelApiService)
        {
            this.favoriteRepository = favoriteRepository;
            this.userContextService = userContextService;
            this.marvelApiService = marvelApiService;
        }
        public async Task<ApiResponse<List<ComicDto>?>> Handle(GetFavoriteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = userContextService.GetUserId();
                List<Favorite> favorites = await favoriteRepository.GetAsync(userId);
                List<ComicDto>? comics = await marvelApiService.GetListAsync();
                
                var list = comics?.Where(w => favorites.Any(f => f.ComicId.Equals(w.Id)));

                comics = list?.Select(s => {
                    s.isFavorite = true;
                    return s;
                }).ToList();

                return ApiResponse<List<ComicDto>?>.Success(comics, "Listado de favoritos");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ComicDto>?>.Error(ex.Message);
            }
        }
    }
}
