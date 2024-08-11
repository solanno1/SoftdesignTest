using LibrarySystem.Services.Entities;
using LibrarySystem.Services.Interfaces;
using LibrarySystem.Services.Repositories;

namespace LibrarySystem.Services.Services
{
    public class RentalService : IRentalService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IUserRepository _userRepository;

        public RentalService(IBookRepository bookRepository, IRentalRepository rentalRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _rentalRepository = rentalRepository;
            _userRepository = userRepository;
        }

        public bool RentBook(int bookId, int userId)
        {
            var book = _bookRepository.GetBookById(bookId);
            var user = _userRepository.GetUserById(userId);

            if(book is null || user is null || book.IsRented) return false;

            var rental = new Rental
            {
                BookId = bookId,
                UserId = userId,
                RentDate = DateTime.Now
            };

            _rentalRepository.AddRental(rental);
            book.IsRented = true;
            _bookRepository.UpdateBook(book);
            return true;
        }

        public bool ReturnBook(int rentalId)
        {
            var rental = _rentalRepository.GetRentalById(rentalId);
            if(rental is null || rental.IsReturned) return false;

            rental.ReturnDate = DateTime.Now;            
            _rentalRepository.UpdateRent(rental);

            var book = _bookRepository.GetBookById(rental.BookId);
            if(book != null)
            {
                book.IsRented = false;
                _bookRepository.UpdateBook(book);
            }
            return true;
        }

        public IEnumerable<Rental> GetRentalsByUser(int userId)
        {
            return _rentalRepository.GetRentalsByUser(userId);
        }        
    }
}
