using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Net.Mime;

namespace TS.Bootcamp.ECommerce.WebAPI.Filters
{
    public class CustomAuthAttribute : Attribute, IAuthorizationFilter
    {
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("apiKey", out StringValues value);
            if (string.IsNullOrEmpty(value) && value != "12345")
            {
                context.HttpContext.Response.StatusCode = 401;
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
                await context.HttpContext.Response.WriteAsync("U dont have permission");
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
