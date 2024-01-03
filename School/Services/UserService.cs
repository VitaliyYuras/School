using School.Data;
using School.Data.Models;
using School.Data.Repository;

namespace School.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string login, string password)
        {
            return _userRepository.GetUserByCredentials(login, password);
        }
    }
}
