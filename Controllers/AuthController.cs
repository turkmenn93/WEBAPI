using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UdemyWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login()
        {
            return Created(string.Empty,new JwtTokenGenerater().GenerateToken());
        }
    }
}
