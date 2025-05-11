using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelApp.Application.Queries.GetAuthUser
{
    public class GetAuthUserValidator : AbstractValidator<GetAuthUserQuery>
    {
        public GetAuthUserValidator()
        {
            RuleFor(x => x.data.Email).NotEmpty().WithMessage("Correo electrónico es requerid");
            RuleFor(x => x.data.Password).NotEmpty().WithMessage("Contraseña es requerida");
        }
    }
}
