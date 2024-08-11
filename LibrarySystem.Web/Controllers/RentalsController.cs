using LibrarySystem.Services.Interfaces;
using LibrarySystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Web.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly IBookService _bookService;

        public RentalsController(IRentalService rentalService, IBookService bookService)
        {
            _rentalService = rentalService;
            _bookService = bookService;
        }

        public ActionResult MyRentals(int userId)
        {
            var rentals = _rentalService.GetRentalsByUser(userId);
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
    }
}