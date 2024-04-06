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
            return Ok(createdUser); // ASP.NET Core automatically serializes the object to JSON
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser([FromBody] User user)
        {

          
            var loginUser = await _userService.LoginUser(user);
            return Ok(loginUser);
            if (loginUser.Data !=null)
            {
                return Ok(loginUser); // ASP.NET Core automatically serializes the object to JSON
            }
            else
            {
                return Ok(loginUser);
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
