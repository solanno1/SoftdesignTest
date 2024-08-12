using LibrarySystem.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly SymmetricSecurityKey _signingKey;

        public AuthService()
        {
            _issuer = "https://localhost:44364/";
            _audience = "https://localhost:44364/";
            _signingKey = new SymmetricSecurityKey(Convert.FromBase64String("3q2+796tvuJOtRgv9fFsppCvnPjm3nTEZ+RbRtN+kNk="));
        }

        public string GenerateJwtToken(string username)
        {
            var signinCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, username)
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
    }
}
