using MarvelApp.Application.Ports;
using MarvelApp.Domain.Entities;
using MarvelApp.Domain.Ports;
using MediatR;

namespace MarvelApp.Application.Commands.DeleteFavorite
{
    public class DeleteFavoriteHandler : IRequestHandler<DeleteFavoriteCommand, ApiResponse<string>>
    {
        private readonly IFavoriteRepository favoriteRepository;
        private readonly IUserContextService userContextService;
        public DeleteFavoriteHandler(IFavoriteRepository favoriteRepository, IUserContextService userContextService)
        {
            this.favoriteRepository = favoriteRepository;
            this.userContextService = userContextService;
        }
        public async Task<ApiResponse<string>> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = userContextService.GetUserId();
                Favorite? favorite = await favoriteRepository.GetAsync(userId, request.comicId);
                
                if(favorite == null)
                {
                    throw new AggregateException("No existe el favorito");
                }

                await favoriteRepository.RemoveAsync(favorite);
                
                return ApiResponse<string>.Success("", "Favorito removido");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Error(ex.Message);
            }

        }
    }
}
