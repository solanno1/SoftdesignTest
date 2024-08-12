using LibrarySystem.Services.Interfaces;
using LibrarySystem.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace LibrarySystem.Tests.Services
{
    [TestClass]
    public class AuthServiceTests
    {
        private AuthService _authService;

        [TestInitialize]
        public void Setup()
        {
            _authService = new AuthService();
        }

        [TestMethod]
        public void GenerateJwtToken_DeveRetornarToken_QuandoUsernameForValido()
        {
            var username = "testuser";
            
            var token = _authService.GenerateJwtToken(username);

            Assert.IsFalse(string.IsNullOrEmpty(token));
            Assert.IsTrue(token.Contains("."));
        }

        [TestMethod]
        public void GenerateJwtToken_DeveConterClaimsCorretos()
        {
            var username = "testuser";

            var token = _authService.GenerateJwtToken(username);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.AreEqual(username, jwtToken.Claims.First(c => c.Type == ClaimTypes.Name).Value);
            Assert.AreEqual(username, jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
