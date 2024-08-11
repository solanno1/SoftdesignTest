using LibrarySystem.Services.Database;
using LibrarySystem.Services.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LibrarySystem.Services.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly LibraryDbContext _context;
        public RentalRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public void AddRental(Rental rental)
        {
            try
            {
                _context.Rentals.Add(rental);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Ocorreu um erro, o aluguel do livro não foi salvo na base de dados.", ex);
            }
        }

        public Rental GetRentalById(int id)
        {
            return _context.Rentals.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Rental> GetRentalsByUser(int userId)
        {
            return _context.Rentals.Where(x => x.UserId == userId).ToList();
        }

        public void UpdateRent(Rental rental)
        {
            try
            {
                _context.Entry(rental).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Ocorreu um erro, o aluguel do livro não foi salvo na base de dados.", ex);
            }
        }
    }
}
