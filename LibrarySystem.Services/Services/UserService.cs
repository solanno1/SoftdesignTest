using LibrarySystem.Services.Entities;
using LibrarySystem.Services.Helpers;
using LibrarySystem.Services.Interfaces;
using LibrarySystem.Services.Repositories;

namespace LibrarySystem.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool ValidateUser(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null) return false;

            return PasswordHelper.VerifyPassword(password, user.PasswordHash);
        }
        public void RegisterUser(string username, string password)
        {
            if(_userRepository.GetUserByUsername(username) != null)
            {
                throw new InvalidOperationException("Username already exists");
            }
            var hashedPassword = PasswordHelper.HashPassword(password);
            var newUser = new User
            {
                Username = username,
                PasswordHash = hashedPassword
            };
            _userRepository.AddUser(newUser);
        }
    }
}
