using Microsoft.AspNetCore.Mvc;
using TS.Bootcamp.ECommerce.WebAPI.Controllers.Abstract;
using TS.Bootcamp.ECommerce.WebAPI.Utilities;

namespace TS.Bootcamp.ECommerce.WebAPI.Controllers
{
    public class OrdersController : BaseApiController
    {
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(GlobalData.Orders);
        }
    }
}
