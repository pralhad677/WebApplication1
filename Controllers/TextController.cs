using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextController : ControllerBase
    {
        [HttpGet]
        public string GetText()
        {
            return "Hello text";
        }
    }
}
