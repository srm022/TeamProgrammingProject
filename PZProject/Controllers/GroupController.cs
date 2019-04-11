using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PZProject.Controllers.Utils;
using PZProject.Data.Requests.GroupRequests;
using PZProject.Handlers.Group;
using System;

namespace PZProject.Controllers
{
    [Route("groups")]
    public class GroupController : Controller
    {
        private readonly IGroupOperationsHandler _groupOperationsHandler;

        public GroupController(IGroupOperationsHandler groupOperationsHandler)
        {
            _groupOperationsHandler = groupOperationsHandler;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetGroups()
        {
            try
            {
                var userId = this.GetUserId();
                var groups = _groupOperationsHandler.GetGroupsForUser(userId);
                return Ok(groups);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateGroupRequest request)
        {
            try
            {
                var userId = this.GetUserId();
                _groupOperationsHandler.CreateNewGroup(request, userId);

                return Created(string.Empty, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] DeleteGroupRequest request)
        {
            try
            {
                var userId = this.GetUserId();
                _groupOperationsHandler.DeleteGroup(request, userId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
