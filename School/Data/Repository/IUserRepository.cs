using School.Data.Models;

namespace School.Data.Repository
{
    public interface IUserRepository
    {
        User GetUserByCredentials(string login, string password);
    }
}
