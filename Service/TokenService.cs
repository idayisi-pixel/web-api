using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _symmetricKey;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JWT:SigningKey"] ?? throw new InvalidOperationException("JWT signing key is not configured.")));
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? throw new InvalidOperationException("email not configured")),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName ?? throw new InvalidOperationException("username not configured"))
            };

            var credentials = new SigningCredentials(_symmetricKey, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = credentials,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}