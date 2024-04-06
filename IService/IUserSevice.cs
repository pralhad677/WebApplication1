using WebApplication1.Model;
using WebApplication1.Util;

namespace WebApplication1.IService
{
    public interface IUserSevice<T>
    {
    

        Task<CustomErrorClass<T>> CreateUser(User user);
        Task<CustomErrorClass<T>> LoginUser(User user);
        Task<IEnumerable<CustomErrorClass<T>>> GetAllUsers();
    }
}
