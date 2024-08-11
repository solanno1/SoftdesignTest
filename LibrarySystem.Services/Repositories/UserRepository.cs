using LibrarySystem.Services.Database;
using LibrarySystem.Services.Entities;
using System.Data.Entity.Infrastructure;

namespace LibrarySystem.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;

        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Ocorreu um erro, o usuário não foi salvo na base de dados.", ex);
            }
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username);
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(x =>x.Id == id);
        }
    }
}
