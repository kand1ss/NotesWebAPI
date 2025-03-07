using System.ComponentModel.DataAnnotations;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Exception_Filters;

public class ExceptionFilter(ILogger<ExceptionFilter> logger) : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        logger.LogError(exception, "API Exception");

        if (exception is AccountNotFoundException accountNotFound)
            context.Result = new NotFoundObjectResult(accountNotFound.Message);
        else if (exception is ValidationException validationException)
            context.Result = new BadRequestObjectResult(validationException.Message);
        else if (exception is NoteNotFoundException noteNotFound)
            context.Result = new NotFoundObjectResult(noteNotFound.Message);
        else
            context.Result = new ObjectResult(new
            {
                Message = "Internal Server Error",
                StatusCode = 500,
            });
        
        context.ExceptionHandled = true;
    }
}