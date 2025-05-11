using MarvelApp.Application;
using MarvelApp.Application.DTOs;
using MarvelApp.Application.Queries.GetComicById;
using MarvelApp.Application.Queries.GetComics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarvelApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComicController : ControllerBase
    {
        private readonly IMediator mediator;
        public ComicController(IMediator mediator)
        {
            this.mediator = mediator;
        }
       
        [HttpGet("{comicId}")]
        public async Task<ApiResponse<ComicDto>> GetOneAsync(int comicId) =>
            await mediator.Send(new GetComicByIdQuery(comicId));
        
        [HttpGet]
        public async Task<ApiResponse<List<ComicDto>>> GetListAsync() =>
           await mediator.Send(new GetComicsQuery());
    }
}
