using System;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class ApiExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        if (IsBadRequest(exception))
        {
            var errorMessage = exception.Message;
            var errorName = exception.GetType().Name;
            var error = new
            {
                ErrorName = errorName,
                ErrorMessage = errorMessage
            };
            context.Result = new BadRequestObjectResult(error);
            context.ModelState.AddModelError(errorName, errorMessage);
        }

        base.OnException(context);
    }

    private static bool IsBadRequest(Exception exception)
    {
        return exception is BusinessValidationException;
    }
}