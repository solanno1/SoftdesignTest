using LibrarySystem.Services.Entities;
using LibrarySystem.Services.Interfaces;
using LibrarySystem.Web.Controllers.Api;
using LibrarySystem.Web.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace LibrarySystem.Tests.Controllers.Api
{
    [TestClass]
    public class BooksApiControllerTests
    {
        private Mock<IBookService> _bookServiceMock;
        private BooksApiController _controller;

        [TestInitialize]
        public void Setup()
        {
            _bookServiceMock = new Mock<IBookService>();
            _controller = new BooksApiController(_bookServiceMock.Object);
        }

        [TestMethod]
        public void GetBooks_DeveRetornarBooks_QuandoChamado()
        {
            var books = new List<BookViewModel>
            {
                new BookViewModel { Id = 1, Title = "Book 1", Author = "Author 1" },
                new BookViewModel { Id = 2, Title = "Book 2", Author = "Author 2" }
            };
            _bookServiceMock.Setup(service => service.GetBooks(It.IsAny<string>())).Returns(new List<Book>());

            var result = _controller.GetBooks() as IEnumerable<BookViewModel>;

            Assert.IsNotNull(result);
            _bookServiceMock.Verify(service => service.GetBooks(null), Times.Once);
        }

        [TestMethod]
        public void GetBook_DeveRetornarOk_QuandoTiverBook()
        {
            var book = new Book { Id = 1, Title = "Book 1", Author = "Author 1", IsRented = false };
            _bookServiceMock.Setup(service => service.GetBookById(1)).Returns(book);

            var result = _controller.GetBook(1) as OkNegotiatedContentResult<BookViewModel>;

            Assert.IsNotNull(result);
            Assert.AreEqual(book.Id, result.Content.Id);
            Assert.AreEqual(book.Title, result.Content.Title);
            Assert.AreEqual(book.Author, result.Content.Author);
            Assert.AreEqual(book.IsRented, result.Content.IsRented);
        }

        [TestMethod]
        public void GetBook_DeveRetornarNotFound_QuandoNaoTiverBook()
        {
            _bookServiceMock.Setup(service => service.GetBookById(It.IsAny<int>())).Returns<Book>(null);

            var result = _controller.GetBook(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void RegisterBook_DeveRetornarOk_QuandoModelForValida()
        {
            var model = new RegisterBookViewModel { Title = "New Book", Author = "New Author" };

            var result = _controller.RegisterBook(model);

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<string>));
            _bookServiceMock.Verify(service => service.RegisterBook(model.Title, model.Author), Times.Once);
        }

        [TestMethod]
        public void RegisterBook_DeveRetornarBadRequest_QuandoModelForInvalida()
        {
            _controller.ModelState.AddModelError("Title", "Title is required");

            var result = _controller.RegisterBook(new RegisterBookViewModel());

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
    }
}
