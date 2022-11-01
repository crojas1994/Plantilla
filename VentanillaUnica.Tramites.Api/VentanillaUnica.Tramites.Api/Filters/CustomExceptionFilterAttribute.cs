using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using VentanillaUnica.Tramites.Common.Exceptions;

namespace VentanillaUnica.Tramites.Api.Filters
{
    public sealed class CustomExceptionFilterAttribute : ExceptionFilterAttribute, IActionFilter
    {
        public CustomExceptionFilterAttribute()
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Metodo intencionalmente vacío por la implementación de la interfaz
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
            {
                throw new InvalidProgramException("No existe un contexto en la aplicación");
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public override void OnException(ExceptionContext context)
        {
            int hashCode;
            var exceptionType = context.Exception;
            string errorMessage;

            switch (exceptionType)
            {
                case ArgumentNullException:
                case ArgumentException:
                    errorMessage = string.IsNullOrWhiteSpace(context.Exception?.InnerException?.Message) ? context.Exception.Message : context.Exception.InnerException.Message;
                    hashCode = HttpStatusCode.BadRequest.GetHashCode();
                    break;
                case CustomBusinessException:
                    errorMessage = string.IsNullOrWhiteSpace(context.Exception?.InnerException?.Message) ? context.Exception.Message : context.Exception.InnerException.Message;
                    hashCode = HttpStatusCode.PreconditionFailed.GetHashCode();
                    break;
                default:
                    errorMessage = string.IsNullOrWhiteSpace(context.Exception?.InnerException?.Message) ? context.Exception.Message : context.Exception.InnerException.Message;
                    hashCode = HttpStatusCode.InternalServerError.GetHashCode();
                    break;
            }

            context.Result = new ContentResult
            {
                Content = errorMessage,
                ContentType = "text/html; charset=utf-8",
                StatusCode = hashCode
            };
        }
    }
}
