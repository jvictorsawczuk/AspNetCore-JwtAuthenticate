using AspNetCore_JwtAuthenticate.Models;
using AspNetCore_JwtAuthenticate.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_JwtAuthenticate.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public LoginController()
        {

        }

        [HttpPost]
        public ActionResult<dynamic> Authenticate([FromServices] AccessTokenService accessTokenService, [FromBody] User model)
        {
            User user = new User();

            if (model.email != user.email || model.password != user.password)
            {
                return Unauthorized("Email or Password incorrect");

            }
            else
            {
                var accessToken = accessTokenService.GenerateToken(model);
                return Ok(accessToken);

            }
        }

    }
}