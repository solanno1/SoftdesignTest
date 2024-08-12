using LibrarySystem.Services.Entities;
using LibrarySystem.Services.Repositories;
using LibrarySystem.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LibrarySystem.Tests.Services
{
    [TestClass]
    public class BookServiceTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private BookService _bookService;

        [TestInitialize]
        public void Setup()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookService = new BookService(_bookRepositoryMock.Object);
        }

        [TestMethod]
        public void GetBookById_DeveRetornarBook_QuandoTemBook()
        {            
            var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author" };
            _bookRepositoryMock.Setup(repo => repo.GetBookById(1)).Returns(book);

            var result = _bookService.GetBookById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(book, result);
        }

        [TestMethod]
        public void RegisterBook_DeveAdicionarBook_QuandoOsDadosForPreenchidos()
        {
            var title = "New Book";
            var author = "New Author";

            _bookService.RegisterBook(title, author);

            _bookRepositoryMock.Verify(repo => repo.AddBook(It.Is<Book>(b => b.Title == title && b.Author == author && !b.IsRented)), Times.Once);
        }
    }
}
