using WebApplication1.IRepository;
using WebApplication1.IService;
using WebApplication1.Model;
using WebApplication1.Util;

namespace WebApplication1.Service
{
    public class UserSevice<T> : IUserSevice<T> where T : class
    {
        public readonly IUserRepository<T> Repository;

        public UserSevice(IUserRepository<T> repository)
        {
            Repository = repository;
        }
       

       async Task<CustomErrorClass<T>> IUserSevice<T>.CreateUser(User user)
        {
            return await Repository.CreateUser(user);
        }

      async  Task<IEnumerable<CustomErrorClass<T>>> IUserSevice<T>.GetAllUsers()
        {
            return await Repository.GetAllUsers();
        }

       async Task<CustomErrorClass<T>> IUserSevice<T>.LoginUser(User user)
        {
            return await Repository.LoginUser(user);
        }
    }
}
