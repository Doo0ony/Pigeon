using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using Shared.Models;

namespace Shared.Extensions;

public static class ControllerExtensions
{
    public static IActionResult ToActionResult<T>(this ServiceResult<T> result)
    {
        if (result.Success) return new OkObjectResult(result.Data);

        return CreateErrorResult(result.ErrorCode, result.Message);
    }

    public static IActionResult ToActionResult(this ServiceResult result)
    {
        if (result.Success && !string.IsNullOrEmpty(result.Message))
            return new OkObjectResult(result.Message);

        if (result.Success) return new OkResult();

        return CreateErrorResult(result.ErrorCode, result.Message);
    }

    private static ObjectResult CreateErrorResult(ErrorCode errorCode, string message)
    { 
        return errorCode switch
        {
            ErrorCode.NotFound => new NotFoundObjectResult(message),
            ErrorCode.ValidationError => new BadRequestObjectResult(message),
            ErrorCode.Conflict => new ConflictObjectResult(message),
            ErrorCode.UnprocessableEntity => new UnprocessableEntityObjectResult(message),
            ErrorCode.Unauthorized => new ObjectResult(message) { StatusCode = 401 },
            _ => new ObjectResult(message) { StatusCode = 500 },
        };
    }
}