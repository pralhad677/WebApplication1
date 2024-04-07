using Microsoft.AspNetCore.Mvc;
using WebApplication1.IRepository;
using WebApplication1.Model;
using WebApplication1.Util;
namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Text;
    using System.Threading.Tasks;
    using WebApplication1.IService;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserSevice<User> _userService;

        public UserController(IUserSevice<User> userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {

            var createdUser = await _userService.CreateUser(user);
            if(createdUser == null)
            {
                return Ok("user already exist");
            }
            return Ok(createdUser); // ASP.NET Core automatically serializes the object to JSON
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser([FromBody] User user)
        {

          
            var data = await _userService.LoginUser(user);
            //return data;
            if (data  ==null)
            {
                return Ok("unAuhtorized");  
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var allUsers = await _userService.GetAllUsers();
            return Ok(allUsers); // ASP.NET Core automatically serializes the object to JSON
        }
    }
    


}
