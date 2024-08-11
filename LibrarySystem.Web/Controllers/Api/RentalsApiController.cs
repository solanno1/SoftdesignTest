using LibrarySystem.Services.Interfaces;
using LibrarySystem.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LibrarySystem.Web.Controllers
{
    [RoutePrefix("api/rentals")]
    public class RentalsApiController : ApiController
    {
        private readonly IRentalService _rentalService;
        private readonly IBookService _bookService;
        public RentalsApiController(IRentalService rentalService, IBookService bookService)
        {
            _rentalService = rentalService;
            _bookService = bookService;
        }

        [HttpPost]
        [Route("rent/{bookId}")]
        public IHttpActionResult RentBook(int bookId, [FromBody] RentBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var success = _rentalService.RentBook(bookId, model.UserId);
                if (!success) return BadRequest("O livro já está alugado");
                return Ok("Livro alugado com sucesso");
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("return/{rentalId}")]
        public IHttpActionResult ReturnBook(int id)
        {
            var success = _rentalService.ReturnBook(id);
            if (!success) return BadRequest("Erro ao devolver o livro");
            return Ok("Livro devolvido com sucesso");
        }

        [HttpGet]
        [Route("user/{userId}")]
        public IHttpActionResult GetRentalsByUser(int userId)
        {
            var rentals = _rentalService.GetRentalsByUser(userId);
            var rentalViewModels = rentals.Select(rental => new RentalViewModel
            {
                Id = rental.Id,
                BookTitle = _bookService.GetBookById(rental.BookId)?.Title,
                RentDate = rental.RentDate,
                ReturnDate = rental.ReturnDate
            }).ToList();
            return Ok(rentals);
        }
    }
}