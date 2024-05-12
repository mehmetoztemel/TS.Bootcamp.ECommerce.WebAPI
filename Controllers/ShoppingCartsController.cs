using Microsoft.AspNetCore.Mvc;
using MO.Result;
using System.Net;
using TS.Bootcamp.ECommerce.WebAPI.Controllers.Abstract;
using TS.Bootcamp.ECommerce.WebAPI.Models;
using TS.Bootcamp.ECommerce.WebAPI.Utilities;

namespace TS.Bootcamp.ECommerce.WebAPI.Controllers
{
    public class ShoppingCartsController : BaseApiController
    {
        [HttpGet]
        public IActionResult Add(Guid productId)
        {
            Product? product = GlobalData.Products.Find(p => p.Id == productId);

            if (product is null)
            {
                return BadRequest(Result<string>.Fail((int)HttpStatusCode.BadRequest, "Product not found"));
            }

            ShoppingCart shoppingCart = new()
            {
                ProductName = product.Name,
                ProductPrice = product.Price
            };

            GlobalData.ShoppingCarts.Add(shoppingCart);

            return Ok(Result<string>.Success("Product has been successfully added to shopping cart"));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(GlobalData.ShoppingCarts);
        }

        [HttpGet("{cartOwner}")]
        public IActionResult Pay(string cartOwner)
        {
            List<Order> orders = GlobalData.ShoppingCarts.Select(s => new Order()
            {
                ProductName = s.ProductName,
                ProductPrice = s.ProductPrice
            }).ToList();
            GlobalData.Orders.AddRange(orders);

            GlobalData.ShoppingCarts.RemoveRange(0, GlobalData.ShoppingCarts.Count);

            return Ok(Result<string>.Success("Payment is successful"));

        }
    }
}
