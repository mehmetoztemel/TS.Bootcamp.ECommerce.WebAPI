using Microsoft.AspNetCore.Mvc.Filters;

namespace TS.Bootcamp.ECommerce.WebAPI.Filters
{
    public class CustomAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var keyCheck = context.HttpContext.Request.Headers.TryGetValue("apiKey", out var secretKey);
            if (!(keyCheck && secretKey == "12345"))
            {
                throw new Exception("U dont have permission");
            }
        }
    }
}
