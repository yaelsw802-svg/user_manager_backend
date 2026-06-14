using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service;

namespace ClothingStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService=jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            //כאן נבצע אימות של המשתמש ממול מסד הנתונים
            if(login.UserNam=="test" && login.Password=="password")
            {
                var token = _jwtService.GenerateToken(login.UserNam);
                return Ok(token);
            }
            return Unauthorized();
        }
    }

    public class LoginModel
    {
        public string UserNam { get; set; }
        public string Password { get; set; }
    }
}
