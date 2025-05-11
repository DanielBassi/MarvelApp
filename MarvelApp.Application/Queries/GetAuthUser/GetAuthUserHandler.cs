using MarvelApp.Domain.Ports;
using MediatR;

namespace MarvelApp.Application.Queries.GetAuthUser
{
    public class GetAuthUserhandler : IRequestHandler<GetAuthUserQuery, ApiResponse<string>>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        public GetAuthUserhandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<ApiResponse<string>> Handle(GetAuthUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.GetUserByEmail(request.data.Email);
                if (user == null || !passwordHasher.Verify(user.PasswordHash, request.data.Password))
                {
                    throw new UnauthorizedAccessException("Credenciales invalidas");
                }
                var token = jwtTokenGenerator.GenerarToken(user);
                return ApiResponse<string>.Success(token, "Usuario logueado");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Error(ex.Message);
            }
        }
    }
}
