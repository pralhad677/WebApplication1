using WebApplication1.Model;
using WebApplication1.Util;

namespace WebApplication1.IRepository
{
    public interface IUserRepository<T> where T : class
    {

        Task<CustomErrorClass<T>> CreateUser(User user);
        Task<CustomErrorClass<T>> LoginUser(User user);
        Task<IEnumerable<CustomErrorClass<T>>> GetAllUsers();
    }
}
