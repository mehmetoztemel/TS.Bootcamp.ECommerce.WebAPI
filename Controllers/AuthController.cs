using Microsoft.AspNetCore.Mvc;
using MO.Result;
using System.Net;
using TS.Bootcamp.ECommerce.WebAPI.Dtos;
using TS.Bootcamp.ECommerce.WebAPI.Filters;
using TS.Bootcamp.ECommerce.WebAPI.Models;
using TS.Bootcamp.ECommerce.WebAPI.Utilities;

namespace TS.Bootcamp.ECommerce.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [CustomLog]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register(AppUserRegisterDto request)
        {
            bool isUserEmailExist = GlobalData.Users.Any(p => p.Email == request.Email);

            if (isUserEmailExist)
            {
                return BadRequest(Result<string>.Success("This email is already exists"));
            }

            AppUser user = new AppUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password
            };
            GlobalData.Users.Add(user);
            return Ok(Result<string>.Success("User create is successful"));

        }

        [HttpPost]
        public IActionResult Login(LoginDto request)
        {
            AppUser? user = GlobalData.Users.FirstOrDefault(p => p.Email == request.Email);
            if (user is null)
            {
                return BadRequest(Result<string>.Fail((int)HttpStatusCode.BadRequest, "User not found"));
            }

            if (user.Password != request.Password)
            {
                return BadRequest(Result<string>.Fail((int)HttpStatusCode.BadRequest, "Password is wrong"));
            }

            return Ok(Result<object>.Success(new { Token = Guid.NewGuid().ToString() }));
        }
    }
}
