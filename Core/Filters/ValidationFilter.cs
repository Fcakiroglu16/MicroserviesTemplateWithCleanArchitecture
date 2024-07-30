using System.Net;
using Core.ResultDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Filters;

public class FluentValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;

        var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

        context.Result = new BadRequestObjectResult(ServiceResult.Fail(errors, HttpStatusCode.BadRequest));
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}