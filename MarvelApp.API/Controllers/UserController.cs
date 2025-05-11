using MarvelApp.Application;
using MarvelApp.Application.Commands.CreateUser;
using MarvelApp.Application.Queries.GetAuthUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarvelApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [ServiceFilter(typeof(ValidationFilter<CreateUserCommand>))]
        [HttpPost]
        public async Task<ApiResponse<Guid>> CreateAsync(CreateUserCommand command)
            => await mediator.Send(command);

        [ServiceFilter(typeof (ValidationFilter<GetAuthUserQuery>))]
        [HttpPost("login")]
        public async Task<ApiResponse<string>> GetAuthUser(GetAuthUserQuery query)
            => await mediator.Send(query);
    }
}
