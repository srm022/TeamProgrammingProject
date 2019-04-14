using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PZProject.Handlers.Utils
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(int userId, string roleName);
    }

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly SystemSettings _appSettings;

        public JwtTokenGenerator(IOptions<SystemSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        
        public string GenerateJwtToken(int userId, string roleName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userId.ToString()),
                new Claim(ClaimTypes.Role, roleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}