using LibrarySystem.Services.Entities;
using LibrarySystem.Services.Helpers;
using LibrarySystem.Services.Interfaces;
using LibrarySystem.Web.Controllers.Api;
using LibrarySystem.Web.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace LibrarySystem.Tests.Controllers.Api
{
    [TestClass]
    public class UsersApiControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<IAuthService> _authServiceMock;
        private UsersApiController _controller;

        [TestInitialize]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _authServiceMock = new Mock<IAuthService>();
            _controller = new UsersApiController(_userServiceMock.Object, _authServiceMock.Object);
        }

        [TestMethod]
        public void Login_DeveRetornarNaoAutorizado_QuandoCredenciaisForInvalidas()
        {
            var model = new LoginViewModel { Username = "solano", Password = "testeErro" };
            _userServiceMock.Setup(service => service.ValidateUser(model.Username, model.Password)).Returns(false);

            var result = _controller.Login(model);

            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void Register_DeveRetornarOk_QuandoModelForValida()
        {
            var model = new RegisterUserViewModel { Username = "solano1", Password = "teste1" };

            var result = _controller.Register(model);

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<string>));
            _userServiceMock.Verify(service => service.RegisterUser(model.Username, model.Password), Times.Once);
        }
    }
}
