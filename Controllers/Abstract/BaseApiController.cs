using Microsoft.AspNetCore.Mvc;

namespace TS.Bootcamp.ECommerce.WebAPI.Controllers.Abstract
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
