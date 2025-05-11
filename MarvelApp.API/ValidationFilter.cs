using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MarvelApp.Application
{
    public class ValidationFilter<T> : IAsyncActionFilter
    {
        private readonly IValidator<T> _validator;
        public ValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var parameterName = context.ActionDescriptor.Parameters.FirstOrDefault()?.Name;

            if (string.IsNullOrEmpty(parameterName) || !context.ActionArguments.ContainsKey(parameterName))
            {
                context.Result = new BadRequestObjectResult("El modelo es nulo o no se encontró en los parámetros de la acción.");
                return;
            }

            var model = context.ActionArguments[parameterName];

            if (model == null)
            {
                context.Result = new BadRequestObjectResult("El modelo es nulo.");
                return;
            }

            var validationResult = await _validator.ValidateAsync((T)model);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName.Split('.').Last(),
                    message = e.ErrorMessage
                })
                .ToList();

                context.Result = new BadRequestObjectResult(ApiResponse<object>.ErrorValidation(errors));
                return;
            }

            await next();
        }
    }
}
