using WebApplication1.Model;
using WebApplication1.Util;

namespace WebApplication1.IService
{
    public interface IUserSevice<T>
    {


        Task<T> CreateUser(User user);
        Task<string> LoginUser(User user);
        Task<IEnumerable<T>> GetAllUsers();
    }
}
