using LibrarySystem.Services.Entities;

namespace LibrarySystem.Services.Repositories
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        User GetUserById(int id);
        void AddUser(User user);
    }
}
