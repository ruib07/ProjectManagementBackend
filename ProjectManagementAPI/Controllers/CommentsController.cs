using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Entities.Efos;
using ProjectManagement.EntityFramework.Configurations;
using ProjectManagement.Services.Services;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        // GET api/comments
        [HttpGet]
        [ProducesResponseType(typeof(List<CommentsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<CommentsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<CommentsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<CommentsEfo>>> GetAllCommentsAsync()
        {
            List<CommentsEfo> comments = await _commentsService.GetAllCommentsAsync();

            return Ok(comments);
        }

        // GET api/comments/{commentId}
        [Authorize]
        [HttpGet("{commentsId}")]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetCommentByIdAsync(int commentId)
        {
            CommentsEfo comment = await _commentsService.GetCommentByIdAsync(commentId);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // POST api/comments
        [HttpPost]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<CommentsEfo>> SendCommentAsync([FromBody, Required] CommentsEfo comment)
        {
            if (ModelState.IsValid)
            {
                CommentsEfo newComment = await _commentsService.SendCommentAsync(comment);
                return StatusCode(StatusCodes.Status201Created, newComment);
            }

            return BadRequest(ModelState);
        }

        // PUT api/comments/{commentId}
        [HttpPut("{commentId}")]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateCommentAsync(int commentId, [FromBody] CommentsEfo updateComment)
        {
            try
            {
                CommentsEfo comment = await _commentsService.UpdateCommentAsync(commentId, updateComment);

                if (comment == null)
                {
                    return NotFound();
                }

                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/comments/{commentId}
        [HttpDelete("{commentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteCommentAsync(int commentId)
        {
            try
            {
                await _commentsService.DeleteCommentAsync(commentId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
