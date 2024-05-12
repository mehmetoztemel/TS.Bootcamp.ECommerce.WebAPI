using Microsoft.AspNetCore.Diagnostics;
using MO.Result;
using System.Net;
using System.Net.Mime;

namespace TS.Bootcamp.ECommerce.WebAPI.Middlewares
{
    public class CustomExceptionMiddleware : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync(Result<string>.Fail(exception.Message).ToString());
            return true;
        }
    }
}
