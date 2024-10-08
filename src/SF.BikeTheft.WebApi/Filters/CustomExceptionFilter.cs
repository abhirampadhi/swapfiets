using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SF.BikeTheft.Common.Exceptions;
using SF.BikeTheft.WebApi.Models;

namespace SF.BikeTheft.WebApi.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    private readonly ILogger<CustomExceptionFilter> _logger;

    public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, context.Exception.Message);

        var code = HttpStatusCode.InternalServerError;
        string details = context.Exception.StackTrace;

        if (context.Exception is NotFoundException)
        {
            code = HttpStatusCode.NotFound;
        }
        else if (context.Exception is System.ComponentModel.DataAnnotations.ValidationException)
        {
            code = HttpStatusCode.BadRequest;
        }
        else if (context.Exception is ArgumentException)
        {
            code = HttpStatusCode.BadRequest;
        }

        var errorResponse = new ErrorResponse((int)code, context.Exception.Message, details);
        context.Result = new JsonResult(errorResponse)
        {
            StatusCode = (int)code
        };
        context.ExceptionHandled = true;
    }
}
