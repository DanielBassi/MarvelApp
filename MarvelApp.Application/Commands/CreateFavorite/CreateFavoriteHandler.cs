using MarvelApp.Application.Ports;
using MarvelApp.Domain.Ports;
using MediatR;

namespace MarvelApp.Application.Commands.CreateFavorite
{
    public class CreateFavoriteHandler : IRequestHandler<CreateFavoriteCommand, ApiResponse<string>>
    {
        private readonly IFavoriteRepository favoriteRepository;
        private readonly IUserContextService userContextService;
        public CreateFavoriteHandler(IFavoriteRepository favoriteRepository, IUserContextService userContextService) 
        { 
            this.favoriteRepository = favoriteRepository;
            this.userContextService = userContextService;
        }
        public async Task<ApiResponse<string>> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = userContextService.GetUserId();
                var result = await favoriteRepository.CreateAsync(request.data.ComicId, userId);
                return ApiResponse<string>.Success(result, "favorito agregado");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Error(ex.Message);
            }
        }
    }
}
