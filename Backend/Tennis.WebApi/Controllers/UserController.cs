using Microsoft.AspNetCore.Mvc;
using Tennis.Model;
using Tennis.Service.Common;
using Tennis.Service;

namespace Tennis.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IUserService _userService;
        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User request)
        {
            
            if (string.IsNullOrEmpty(request.Username) ||
                string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Password) ||
                string.IsNullOrEmpty(request.FullName))
            {
                return BadRequest("Name, email, username, and password must not be empty.");
            }

            try
            {
                
                var user = await _userService.CreateUserAsync(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(User request)
        {
            
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password must not be empty.");
            }

            try
            {
                var result = await _userService.GetUserLoginAsync(request);

                if (!string.IsNullOrEmpty(result.Username))
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Username and/or password are incorrect.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
