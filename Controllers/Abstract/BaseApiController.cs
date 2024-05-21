using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TS.Bootcamp.ECommerce.WebAPI.Filters;

namespace TS.Bootcamp.ECommerce.WebAPI.Controllers.Abstract
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [CustomLog]
    [Authorize]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
