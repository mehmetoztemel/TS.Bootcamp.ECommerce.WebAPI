using Microsoft.AspNetCore.Mvc;
using TS.Bootcamp.ECommerce.WebAPI.Filters;

namespace TS.Bootcamp.ECommerce.WebAPI.Controllers.Abstract
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [CustomLog]
    [CustomAuth]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
