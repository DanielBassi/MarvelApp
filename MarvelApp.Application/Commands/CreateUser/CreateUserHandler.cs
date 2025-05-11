using AutoMapper;
using MarvelApp.Domain.Entities;
using MarvelApp.Domain.Ports;
using MarvelApp.Domain.ValueObjects;
using MediatR;

namespace MarvelApp.Application.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ApiResponse<Guid>>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;
        public CreateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
        }
        public async Task<ApiResponse<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var email = new Email(request.data.Email);
                var identificacion = new Identification(request.data.Identification);
                var hashedPassword = passwordHasher.Hash(request.data.Password);
                var user = new User();
                user.Id = Guid.NewGuid();
                user.Email = email;
                user.Identification = identificacion;
                user.Name = request.data.Name;
                user.PasswordHash = hashedPassword;

                await userRepository.CreateAsync(user);

                return ApiResponse<Guid>.Success(user.Id, "Usuario creado");
            }
            catch(ArgumentException ex)
            {
                return ApiResponse<Guid>.ErrorValidation(new List<object>
                {
                    new {
                        field = ex.ParamName,
                        message = ex.Message.Split(".")?.FirstOrDefault(),
                    }
                });
            }
            catch (Exception ex)
            {
                return ApiResponse<Guid>.Error(ex.Message);
            }
        }
    }
}
