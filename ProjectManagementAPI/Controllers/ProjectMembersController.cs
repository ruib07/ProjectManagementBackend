using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Entities.Efos;
using ProjectManagement.Services.Services;
using System.Net.Mime;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectMembersController : ControllerBase
    {
        private readonly IProjectMembersService _projectMembersService;

        public ProjectMembersController(IProjectMembersService projectMembersService)
        {
            _projectMembersService = projectMembersService;
        }

        // GET api/projectmembers
        [HttpGet]
        [ProducesResponseType(typeof(List<ProjectMembersEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ProjectMembersEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<ProjectMembersEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<ProjectMembersEfo>>> GetAllProjectMembersAsync()
        {
            List<ProjectMembersEfo> projectMembers = await _projectMembersService.GetAllProjectMembersAsync();

            return Ok(projectMembers);
        }

        // GET api/projectmembers/{projectMembersId}
        [Authorize]
        [HttpGet("{projectMembersId}")]
        [ProducesResponseType(typeof(ProjectMembersEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProjectMembersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectMembersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProjectMembersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetProjectMembersByIdAsync(int projectMembersId)
        {
            ProjectMembersEfo projectMembers = await _projectMembersService.GetProjectMembersByIdAsync(projectMembersId);

            if (projectMembers == null)
            {
                return NotFound();
            }

            return Ok(projectMembers);
        }

        // DELETE api/projectmembers/{projectMembersId}
        [HttpDelete("{projectMembersId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteProjectMembersAsync(int projectMembersId)
        {
            try
            {
                await _projectMembersService.DeleteProjectMembersAsync(projectMembersId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
