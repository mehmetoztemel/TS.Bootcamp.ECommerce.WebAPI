using Microsoft.AspNetCore.Mvc;
using MO.Result;
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
        private readonly JwtProvider _jwtProvider;
        public AuthController(JwtProvider jwtProvider)
        {

            _jwtProvider = jwtProvider;

        }
        [HttpPost]
        public IActionResult Register(AppUserRegisterDto request)
        {
            bool isUserEmailExist = GlobalData.Users.Any(p => p.Email == request.Email);

            if (isUserEmailExist)
            {
                return BadRequest(Result<string>.Success("This email is already exists"));
            }

            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePassword(request.Password, out passwordHash, out passwordSalt);
            AppUser user = new AppUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            GlobalData.Users.Add(user);
            return Ok(Result<string>.Success("User create is successful"));

        }

        //[HttpPost]
        //public IActionResult Login(LoginDto request)
        //{
        //    AppUser? user = GlobalData.Users.FirstOrDefault(p => p.Email == request.Email);
        //    if (user is null)
        //    {
        //        return BadRequest(Result<string>.Fail((int)HttpStatusCode.BadRequest, "User not found"));
        //    }
        //    bool checkPassword = HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);
        //    if (!checkPassword)
        //    {
        //        return BadRequest(Result<string>.Fail((int)HttpStatusCode.BadRequest, "Password is wrong"));
        //    }

        //    return Ok(Result<object>.Success(new { Token = _jwtProvider.CreateToken(user) }));
        //}
    }
}
