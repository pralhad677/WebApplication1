using WebApplication1.Model;
using WebApplication1.Util;

namespace WebApplication1.IRepository
{
    public interface IUserRepository<T> where T : class
    {

        Task< T> CreateUser(User user);
        Task<string> LoginUser(User user);
        Task<IEnumerable<T>> GetAllUsers();
    }
}
