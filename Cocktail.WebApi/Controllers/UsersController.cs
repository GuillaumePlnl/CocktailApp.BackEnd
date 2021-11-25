using Microsoft.AspNetCore.Mvc;
using Cocktail.WebApi.Models;
using Cocktail.WebApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace Cocktail.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //[AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
                
            return Ok(response);
        }

        [HttpGet("allusers/")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}