using Microsoft.AspNetCore.Mvc;
using PZProject.Data.Requests.UserRequests;
using PZProject.Handlers.User;
using System;

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
        public IActionResult Register([FromBody] RegisterUserRequest request)
        {
            try
            {
                _userOperationsHandler.RegisterNewUser(request);

                return Created("", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserRequest request)
        {
            try
            {
                var result = _userOperationsHandler.LoginUser(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(401, ex.Message);
            }
        }
    }
}