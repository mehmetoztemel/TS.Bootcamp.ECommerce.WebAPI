using Microsoft.AspNetCore.Mvc;
using MO.Result;
using System.Net;
using TS.Bootcamp.ECommerce.WebAPI.Controllers.Abstract;
using TS.Bootcamp.ECommerce.WebAPI.Dtos;
using TS.Bootcamp.ECommerce.WebAPI.Models;
using TS.Bootcamp.ECommerce.WebAPI.Utilities;

namespace TS.Bootcamp.ECommerce.WebAPI.Controllers
{
    public class ProductsController : BaseApiController
    {
        [HttpPost]
        public IActionResult Create(CreateProductDto request)
        {
            bool isNameExists = GlobalData.Products.Any(p => p.Name == request.Name);
            if (isNameExists)
            {
                return BadRequest(Result<string>.Fail((int)HttpStatusCode.BadRequest, "This product name is already exists"));
            }

            if (request.Price <= 0)
            {
                return BadRequest(Result<string>.Fail((int)HttpStatusCode.BadRequest, "Product price must be greater then 0"));
            }

            Product product = new()
            {
                Name = request.Name,
                Price = request.Price
            };

            GlobalData.Products.Add(product);
            return Ok(Result<string>.Success("Product create is successful"));
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(GlobalData.Products.OrderBy(p => p.Name));
        }

        [HttpDelete]
        public IActionResult DeleteById(Guid id)
        {
            Product? product = GlobalData.Products.Find(p => p.Id == id);

            if (product is null)
            {
                return BadRequest(Result<string>.Fail((int)HttpStatusCode.BadRequest, "Product not found"));
            }

            GlobalData.Products.Remove(product);

            return Ok(Result<string>.Success("Product delete is seccessful"));
        }

        [HttpPut]
        public IActionResult Update(UpdateProductDto request)
        {
            Product? product = GlobalData.Products.Find(p => p.Id == request.Id);

            if (product is null)
            {
                return BadRequest(Result<string>.Fail((int)HttpStatusCode.BadRequest, "Product not found"));
            }

            if (product.Name != request.Name)
            {
                bool isNameExists = GlobalData.Products.Any(p => p.Name == request.Name);
                if (isNameExists)
                {
                    return BadRequest(Result<string>.Fail((int)HttpStatusCode.BadRequest, "This product name is already exists"));
                }
            }

            product.Name = request.Name;
            product.Price = request.Price;

            return Ok(Result<string>.Success("Product update is successful"));
        }
    }
}
