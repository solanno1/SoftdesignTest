using LibrarySystem.Services.Entities;
using LibrarySystem.Services.Helpers;
using LibrarySystem.Services.Interfaces;
using LibrarySystem.Services.Repositories;

namespace LibrarySystem.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public IEnumerable<Book> GetBooks(string search)
        {
            return _bookRepository.GetBooks(search);
        }

        public void RegisterBook(string title, string author)
        {
            var newBook = new Book
            {
                Title = title,
                Author = author,
                IsRented = false
            };
            _bookRepository.AddBook(newBook);
        }
    }
}
