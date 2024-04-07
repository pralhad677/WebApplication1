using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using WebApplication1.Model;
using WebApplication1.Util;

namespace WebApplication1.IService
{
    public interface IUserSevice<T>
    {


        Task<T> CreateUser(UserDto user);
        Task<string> LoginUser(UserDto user);
        Task<IEnumerable<T>> GetAllUsers();
        Task<dynamic> CreateCourse(CourseDto courseDto);
    }
}
