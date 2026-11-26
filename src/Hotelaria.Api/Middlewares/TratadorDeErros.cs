using Hotelaria.Application.Common;
using Hotelaria.Domain.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace Hotelaria.Api.Middlewares
{
    public class TratadorDeErros(IProblemDetailsService problemDetails) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
        {
            var (status, titulo) = exception switch
            {
                NaoEncontradoException => (StatusCodes.Status404NotFound, "Recurso não encontrado"),
                DomainException => (StatusCodes.Status400BadRequest, "Regra de negócio violada"),
                _ => (0, string.Empty)
            };

            // Qualquer outro erro não é tratado aqui: o ASP.NET devolve 500
            if (status == 0)
                return false;

            context.Response.StatusCode = status;

            return await problemDetails.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                Exception = exception,
                ProblemDetails =
                {
                    Status = status,
                    Title = titulo,
                    Detail = exception.Message
                }
            });
        }
    }
}
