using School.Data.Models;

namespace School.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SchoolDbContext _context;

        public UserRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public User GetUserByCredentials(string login, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        }
    }
}
