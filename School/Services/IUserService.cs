using School.Data.Models;

namespace School.Services
{
    public interface IUserService
    {
        User Authenticate(string login, string password);
    }
}
