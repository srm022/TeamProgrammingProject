using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PZProject.Controllers.Utils;
using PZProject.Handlers.Note.Attachment;
using System;

namespace PZProject.Controllers
{
    [Route("groups/{groupId}/notes/{noteId}/attachment")]
    public class AttachmentController : Controller
    {
        private readonly IAttachmentHandler _attachmentHandler;

        public AttachmentController(IAttachmentHandler attachmentHandler)
        {
            _attachmentHandler = attachmentHandler;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAttachmentForNote(int noteId)
        {
            try
            {
                var userId = this.GetUserId();
                var attachment = _attachmentHandler.GetAttachmentForNote(userId, noteId);

                return Ok(attachment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateAttachmentForNote(IFormFile file, int noteId)
        {
            try
            {
                var userId = this.GetUserId();
                _attachmentHandler.CreateAttachment(file, userId, noteId);

                return Created(string.Empty, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteAttachmentFromNote(int noteId)
        {
            try
            {
                var userId = this.GetUserId();
                _attachmentHandler.DeleteAttachment(userId, noteId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
