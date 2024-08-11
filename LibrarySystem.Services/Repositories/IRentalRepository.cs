using LibrarySystem.Services.Entities;

namespace LibrarySystem.Services.Repositories
{
    public interface IRentalRepository
    {
        void AddRental(Rental rental);
        Rental GetRentalById(int id);
        void UpdateRent(Rental rental);
        IEnumerable<Rental> GetRentalsByUser(int userId);
    }
}
