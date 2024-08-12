using LibrarySystem.Services.Entities;
using LibrarySystem.Services.Repositories;
using LibrarySystem.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LibrarySystem.Tests.Services
{
    [TestClass]
    public class RentalServiceTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<IRentalRepository> _rentalRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private RentalService _rentalService;

        [TestInitialize]
        public void Setup()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _rentalRepositoryMock = new Mock<IRentalRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _rentalService = new RentalService(_bookRepositoryMock.Object, _rentalRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [TestMethod]
        public void RentBook_DeveRetornarTrue_QuandoTemLivroEUsuario()
        {
            var book = new Book { Id = 1, IsRented = false };
            var user = new User { Id = 1 };
            _bookRepositoryMock.Setup(repo => repo.GetBookById(1)).Returns(book);
            _userRepositoryMock.Setup(repo => repo.GetUserById(1)).Returns(user);

            var result = _rentalService.RentBook(1, 1);

            Assert.IsTrue(result);
            _bookRepositoryMock.Verify(repo => repo.UpdateBook(It.Is<Book>(b => b.IsRented == true)), Times.Once);
            _rentalRepositoryMock.Verify(repo => repo.AddRental(It.IsAny<Rental>()), Times.Once);
        }

        [TestMethod]
        public void ReturnBook_DeveRetornarTrue_QuandoAluguelValido()
        {
            var rental = new Rental { Id = 1, BookId = 1 };
            var book = new Book { Id = 1, IsRented = true };
            _rentalRepositoryMock.Setup(repo => repo.GetRentalById(1)).Returns(rental);
            _bookRepositoryMock.Setup(repo => repo.GetBookById(1)).Returns(book);

            var result = _rentalService.ReturnBook(1);

            Assert.IsTrue(result);
            _rentalRepositoryMock.Verify(repo => repo.UpdateRent(It.Is<Rental>(r => r.IsReturned == true)), Times.Once);
            _bookRepositoryMock.Verify(repo => repo.UpdateBook(It.Is<Book>(b => b.IsRented == false)), Times.Once);
        }
    }
}
