using Microsoft.AspNetCore.Mvc;
using WebApplication1.IRepository;
using WebApplication1.Model;
using WebApplication1.Util;
namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Text;
    using System.Threading.Tasks;
    using WebApplication1.IService;

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowFrontend")]
    public class UserController : ControllerBase
    {
        private readonly IUserSevice<User> _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserSevice<User> userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
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
            return Ok(allUsers);  
        }
        [HttpPost]
        public async Task<IActionResult> pratice()
        {
            try
            {
                string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"]!;
                string[] parts = authorizationHeader.Split(' ');
                var token = parts[1];
                TokenValidation tokenValidation = new TokenValidation();
                var claimsPrincipal = tokenValidation.ValidateTokenAndGetClaims(token);

                if (claimsPrincipal != null)
                {
                    string userEmail = tokenValidation.ExtractUserEmailFromClaims(claimsPrincipal);
                    Console.WriteLine($"User email: {userEmail}");
                }
                return Ok("hello");
            }
            catch (System.Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
    


}
