using LibrarySystem.Services.Interfaces;
using LibrarySystem.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace LibrarySystem.Web.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksApiController : ApiController
    {
        private readonly IBookService _bookService;

        public BooksApiController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<BookViewModel> GetBooks(string search = null)
        {
            var books = _bookService.GetBooks(search);
            var bookViewModel = new List<BookViewModel>();

            foreach (var book in books)
            {
                bookViewModel.Add(new BookViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    IsRented = book.IsRented
                });
            }
            return bookViewModel;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetBook(int id)
        {
            var book = _bookService.GetBookById(id);
            if(book == null) return NotFound();

            var bookViewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsRented = book.IsRented
            };

            return Ok(bookViewModel);
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult RegisterBook([FromBody] RegisterBookViewModel model)
        {
            if(ModelState.IsValid)
            {
                _bookService.RegisterBook(model.Title, model.Author);
                return Ok("Registro realizado");
            }
            return BadRequest();
        }
    }
}