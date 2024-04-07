using WebApplication1.Model;

namespace WebApplication1.DTO
{
    public class UserDto
    {
        public int Id { get; set; } // Primary key (usually auto-incrementing)

        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
