using LibrarySystem.Services.Interfaces;
using LibrarySystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace LibrarySystem.Web.Controllers
{
    [System.Web.Mvc.Authorize]
    public class RentalsController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        public RentalsController(IRentalService rentalService, IBookService bookService, IUserService userService)
        {
            _rentalService = rentalService;
            _bookService = bookService;
            _userService = userService;
        }

        public ActionResult MyRentals()
        {
            string username = User.Identity.Name;

            var user = _userService.GetUserByUsername(username);
            if (user == null) return RedirectToAction("AvailableBooks");
            
            var rentals = _rentalService.GetRentalsByUser(user.Id);
            var rentalViewModels = rentals.Select(rental => new RentalViewModel
            {
                Id = rental.Id,
                BookTitle = _bookService.GetBookById(rental.BookId)?.Title,
                RentDate = rental.RentDate,
                ReturnDate = rental.ReturnDate
            }).ToList();

            return View(rentalViewModels);
        }

        public ActionResult AvailableBooks()
        {
            var books = _bookService.GetBooks(null).Where(b => !b.IsRented).ToList();
            var bookViewModels = books.Select(book => new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsRented = book.IsRented
            }).ToList();
            return View(bookViewModels);
        }

        public ActionResult RentBook(int bookId)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var user = _userService.GetUserByUsername(username);
                if (user == null) return View("Details", new { id = bookId });

                var success = _rentalService.RentBook(bookId, user.Id);
                if (success)
                {
                    return RedirectToAction("MyRentals");
                }
                else
                {
                    ModelState.AddModelError("", "Livro já alugado ou indisponível");
                }

            }
            return View("Details", new {id = bookId});
        }

        public ActionResult ReturnBook(int rentalId)
        {
            _rentalService.ReturnBook(rentalId);
            return RedirectToAction("Index", "Books");
        }
    }
}