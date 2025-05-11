using MarvelApp.Application;
using MarvelApp.Application.Commands.CreateFavorite;
using MarvelApp.Application.Commands.DeleteFavorite;
using MarvelApp.Application.DTOs;
using MarvelApp.Application.Queries.GetFavorite;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarvelApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IMediator mediator;
        public FavoriteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> CreateAsync([FromBody] CreateFavoriteCommand command) =>
            await mediator.Send(command);

        [HttpGet]
        public async Task<ApiResponse<List<ComicDto>?>> GetAsync() =>
            await mediator.Send(new GetFavoriteQuery());

        [HttpDelete]
        public async Task<ApiResponse<string>> RemoveAsync([FromQuery] int comicId) =>
            await mediator.Send(new DeleteFavoriteCommand(comicId));
    }
}
