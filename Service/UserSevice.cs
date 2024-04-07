using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
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
       

       async Task<T> IUserSevice<T>.CreateUser(UserDto user)
        {
            
            var item = await Repository.CreateUser(user);
            return item;
        }

      async  Task<IEnumerable<T>> IUserSevice<T>.GetAllUsers()
        {
            var item = await Repository.GetAllUsers();
            return item;
        }

       async Task<string> IUserSevice<T>.LoginUser(UserDto user)
        {
             var x= await  Repository.LoginUser(user);
            return x;
        }
       

        async Task<dynamic> IUserSevice<T>.CreateCourse(CourseDto courseDto)
        {
            return await Repository.CreateCourse(courseDto);
        }
    }
}
