using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Services;

namespace WebApi.Domain.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if(username == "admin" || password == "admin")
            {
                var token = TokenServices.GerenerateToken(new Model.Employee());
                return Ok(token);
            }

            return BadRequest("User invalid, Check the user name and password.");
        }
    }
}
