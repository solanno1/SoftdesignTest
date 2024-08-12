using LibrarySystem.Services.Entities;
using LibrarySystem.Services.Interfaces;
using LibrarySystem.Web.Controllers.Api;
using LibrarySystem.Web.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace LibrarySystem.Tests.Controllers.Api
{
    [TestClass]
    public class RentalsApiControllerTests
    {
        private Mock<IRentalService> _rentalServiceMock;
        private Mock<IBookService> _bookServiceMock;
        private Mock<IUserService> _userServiceMock;
        private RentalsApiController _controller;

        [TestInitialize]
        public void Setup()
        {
            _rentalServiceMock = new Mock<IRentalService>();
            _bookServiceMock = new Mock<IBookService>();
            _userServiceMock = new Mock<IUserService>();
            _controller = new RentalsApiController(_rentalServiceMock.Object, _bookServiceMock.Object, _userServiceMock.Object);
        }

        [TestMethod]
        public void RentBook_DeveRetornarOk_QuandoLivroForAlugado()
        {
            var user = new User { Id = 1, Username = "solano" };
            _userServiceMock.Setup(service => service.GetUserByUsername(It.IsAny<string>())).Returns(user);
            _rentalServiceMock.Setup(service => service.RentBook(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var result = _controller.RentBook(1) as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(result);
            Assert.AreEqual("Livro alugado com sucesso", result.Content);
        }

        [TestMethod]
        public void RentBook_DeveRetornarBadRequest_QuandoAluguelFalhar()
        {
            var user = new User { Id = 1, Username = "solano1" };
            _userServiceMock.Setup(service => service.GetUserByUsername(It.IsAny<string>())).Returns(user);
            _rentalServiceMock.Setup(service => service.RentBook(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            var result = _controller.RentBook(1);

            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void ReturnBook_DeveRetornarOk_QuandoDevolucaoForSucedida()
        {
            _rentalServiceMock.Setup(service => service.ReturnBook(It.IsAny<int>())).Returns(true);

            var result = _controller.ReturnBook(1) as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(result);
            Assert.AreEqual("Livro devolvido com sucesso", result.Content);
        }

        [TestMethod]
        public void ReturnBook_DeveRetornarBadRequest_QuandoDevolucaoFalhar()
        {
            _rentalServiceMock.Setup(service => service.ReturnBook(It.IsAny<int>())).Returns(false);

            var result = _controller.ReturnBook(1);

            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void GetRentalsByUser_DeveRetornarOk_QuandoUsuarioTemLivro()
        {
            var rentals = new List<Rental>
            {
                new Rental { Id = 1, BookId = 1, UserId = 1, ReturnDate = null, RentDate = DateTime.Now }
            };
            var book = new Book { Id = 1, Title = "Book 1", Author = "Author 1" };

            _rentalServiceMock.Setup(service => service.GetRentalsByUser(It.IsAny<int>())).Returns(rentals);
            _bookServiceMock.Setup(service => service.GetBookById(It.IsAny<int>())).Returns(book);

            var result = _controller.GetRentalsByUser(1) as OkNegotiatedContentResult<List<RentalViewModel>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Count);
            Assert.AreEqual("Book 1", result.Content[0].BookTitle);
            Assert.AreEqual(rentals[0].RentDate, result.Content[0].RentDate);
            Assert.IsNull(result.Content[0].ReturnDate);
        }
    }
}
