using Microsoft.AspNetCore.Mvc;
using ContactBook.Domain.Common;
using ContactBook.Domain.Enums;

namespace ContactBook.Api.Extensions;

public static class ControllerExtensions
{
    public static IActionResult ToActionResult<T>(this ServiceResult<T> result)
    {
        if (result.Success) return new OkObjectResult(result.Data);

        return result.ErrorCode switch
        {
            ErrorCode.NotFound => new NotFoundObjectResult(result.Message),
            ErrorCode.ValidationError => new BadRequestObjectResult(result.Message),
            ErrorCode.Unauthorized => new UnauthorizedObjectResult(result.Message),
            ErrorCode.Conflict => new ConflictObjectResult(result.Message),
            ErrorCode.UnprocessableEntity => new UnprocessableEntityObjectResult(result.Message),
            _ => new ObjectResult(result.Message) { StatusCode = 500 },

        };
    }

    public static IActionResult ToActionResult(this ServiceResult result)
    {
        if (result.Success && result.Message.Length > 0) return new OkObjectResult(result.Message);
        if (result.Success) return new OkResult();

        return result.ErrorCode switch
        {
            ErrorCode.NotFound => new NotFoundObjectResult(result.Message),
            ErrorCode.ValidationError => new BadRequestObjectResult(result.Message),
            ErrorCode.Unauthorized => new UnauthorizedObjectResult(result.Message),
            ErrorCode.Conflict => new ConflictObjectResult(result.Message),
            ErrorCode.UnprocessableEntity => new UnprocessableEntityObjectResult(result.Message),
            _ => new ObjectResult(result.Message) { StatusCode = 500 },

        };
    }
}