using Microsoft.AspNetCore.Mvc.Filters;

namespace TS.Bootcamp.ECommerce.WebAPI.Filters
{
    public class CustomAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool keyCheck = context.HttpContext.Request.Headers.Keys.Any(x => x == "apiKey");
            if (keyCheck)
            {
                string value = context.HttpContext.Request.Headers["apiKey"]!;
                if (value != "12345")
                {
                    throw new Exception("U dont have permission");
                }
            }
            else throw new Exception("U dont have permission");
        }
    }
}
