using LibrarySystem.Services.Entities;

namespace LibrarySystem.Services.Interfaces
{
    public interface IRentalService
    {
        bool RentBook(int bookId, int userId);
        bool ReturnBook(int rentalId);
        IEnumerable<Rental> GetRentalsByUser(int userId);
    }
}
