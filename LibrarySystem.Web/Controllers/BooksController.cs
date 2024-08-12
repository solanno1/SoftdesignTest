using LibrarySystem.Services.Interfaces;
using LibrarySystem.Services.Repositories;
using LibrarySystem.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Web.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IUserRepository _userRepository;

        public BooksController(IBookService bookService, IUserRepository userRepository)
        {
            _bookService = bookService;
            _userRepository = userRepository;
        }

        public ActionResult Index(string search = null)
        {
            var books = _bookService.GetBooks(search);
            var bookViewModels = books.Select(book => new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsRented = book.IsRented
            }).ToList();

            return View(bookViewModels);
        }

        public ActionResult Details(int id)
        {
            var book = _bookService.GetBookById(id);
            
            if (book == null) return HttpNotFound();

            var bookViewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsRented = book.IsRented
            };

            return View(bookViewModel);
        }
        public ActionResult RegisterBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterBook(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                _bookService.RegisterBook(model.Title, model.Author);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}