using System.Net;
using Core.ResultDto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Core.Middlewares;

public static class ExceptionHandlerMiddleware
{
    public static void UseCustomExceptionHandlerMiddleware(this WebApplication app)
    {
        app.UseExceptionHandler(x =>
        {
            x.Run(context =>
            {
                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                var exception = exceptionFeature!.Error;


                var result = ServiceResult.Fail(exception.Message, HttpStatusCode.InternalServerError);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";


                return context.Response.WriteAsJsonAsync(result);
            });
        });
    }
}