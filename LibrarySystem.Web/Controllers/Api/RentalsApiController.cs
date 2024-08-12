using LibrarySystem.Services.Interfaces;
using LibrarySystem.Services.Services;
using LibrarySystem.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LibrarySystem.Web.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/rentals")]
    public class RentalsApiController : ApiController
    {
        private readonly IRentalService _rentalService;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        public RentalsApiController(IRentalService rentalService, IBookService bookService, IUserService userService)
        {
            _rentalService = rentalService;
            _bookService = bookService;
            _userService = userService;
        }

        [HttpPost]
        [Route("rent/{bookId}")]
        public IHttpActionResult RentBook(int bookId)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var user = _userService.GetUserByUsername(username);
                if (user == null) return BadRequest(ModelState);

                var success = _rentalService.RentBook(bookId, user.Id);
                if (!success) return BadRequest("O livro já está alugado");
                return Ok("Livro alugado com sucesso");
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("return/{rentalId}")]
        public IHttpActionResult ReturnBook(int rentalId)
        {
            var success = _rentalService.ReturnBook(rentalId);
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
            return Ok(rentalViewModels);
        }
    }
}