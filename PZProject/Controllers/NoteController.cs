using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PZProject.Controllers.Utils;
using PZProject.Data.Requests.NoteRequests;
using PZProject.Handlers.Note;

namespace PZProject.Controllers
{
    [Route("groups/{id}/notes")]
    public class NoteController: Controller
    {
        private readonly INoteOperationsHandler _noteOperationsHandler;

        public NoteController(INoteOperationsHandler noteOperationsHandler)
        {
            _noteOperationsHandler = noteOperationsHandler;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetNotesForGroup(int id)
        {
            try
            {
                var userId = this.GetUserId();
                var notes = _noteOperationsHandler.GetNotesForGroup(id, userId);

                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateNoteForGroup([FromBody] CreateNoteRequest request, int id)
        {
            try
            {
                var userId = this.GetUserId();
                _noteOperationsHandler.CreateNoteForGroup(request, id, userId);

                return Created(string.Empty, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
