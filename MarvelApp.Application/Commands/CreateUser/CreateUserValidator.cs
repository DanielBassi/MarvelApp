using FluentValidation;

namespace MarvelApp.Application.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.data.Email)
                .NotEmpty().WithMessage("El correo electrtónico es requedido");

            RuleFor(x => x.data.Name)
                .NotEmpty().WithMessage("El nombre es requedido");

            RuleFor(x => x.data.Password)
                .NotEmpty().WithMessage("La contraseña es requedida");

            RuleFor(x => x.data.Identification)
                .NotEmpty().WithMessage("La identificación es requedida");
        }
    }
}
