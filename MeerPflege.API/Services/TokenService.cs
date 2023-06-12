using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeerPflege.Domain;
using Microsoft.IdentityModel.Tokens;

namespace MeerPflege.API.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string CreateToken(AppUser user, string role)
        {
            
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, role),
                new Claim("HomeId", user.HomeId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("UHAPDjXUzccXrJSbdFkQaxrhh"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}