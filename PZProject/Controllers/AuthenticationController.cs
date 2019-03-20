using System;
using Microsoft.AspNetCore.Mvc;
using PZProject.Handlers.User;
using PZProject.Handlers.User.Model;

namespace PZProject.Controllers
{
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly IUserOperationsHandler _userOperationsHandler;
        
        public AuthenticationController(IUserOperationsHandler userOperationsHandler)
        {
            _userOperationsHandler = userOperationsHandler;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserModel model)
        {
            try
            {
                _userOperationsHandler.RegisterNewUser(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserModel model)
        {
            try
            {
                var result = _userOperationsHandler.LoginUser(model);

                return Ok(result);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
    }
}