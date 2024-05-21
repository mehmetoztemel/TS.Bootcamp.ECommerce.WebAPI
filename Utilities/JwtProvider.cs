using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TS.Bootcamp.ECommerce.WebAPI.Models;

namespace TS.Bootcamp.ECommerce.WebAPI.Utilities
{
    public class JwtProvider
    {
        private readonly Jwt _jwtOptions;
        public JwtProvider(IOptionsMonitor<Jwt> options)
        {
            _jwtOptions = options.CurrentValue;
        }

        public string CreateToken(AppUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier ,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Role,"admin")
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.Now.AddDays(1),
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(jwtSecurityToken);
        }

    }
}
