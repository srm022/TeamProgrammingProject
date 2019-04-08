using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PZProject.Handlers.Group;
using PZProject.Handlers.Group.Model;
using PZProject.Handlers.Utils;

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

        [Authorize(Roles = "Administrator, Petitioner")]
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
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Administrator, Petitioner")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateGroupModel model)
        {
            try
            {
                var userId = this.GetUserId();
                _groupOperationsHandler.CreateNewGroup(model, userId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Administrator, Petitioner")]
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] int groupId)
        {
            try
            {
                var userId = this.GetUserId();
                _groupOperationsHandler.DeleteGroup(groupId, userId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
