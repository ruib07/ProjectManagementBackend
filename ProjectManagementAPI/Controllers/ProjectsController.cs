using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Entities.Efos;
using ProjectManagement.Services.Services;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService=projectService;
        }

        // GET api/projects
        [HttpGet]
        [ProducesResponseType(typeof(List<ProjectsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ProjectsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<ProjectsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<ProjectsEfo>>> GetAllProjectsAsync()
        {
            List<ProjectsEfo> projects = await _projectService.GetAllProjectsAsync();

            return Ok(projects);
        }

        // GET api/projects/{projectId}
        [Authorize]
        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetProjectByIdAsync(int projectId)
        {
            ProjectsEfo projects = await _projectService.GetProjectByIdAsync(projectId);

            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects);
        }

        // GET api/projects/byname/{projectName}
        [HttpGet("byname/{projectName}")]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetProjectByNameAsync(string projectName)
        {
            ProjectsEfo projects = await _projectService.GetProjectByNameAsync(projectName);

            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects);
        }

        // POST api/projects
        [HttpPost]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ProjectsEfo>> SendProjectAsync([FromBody, Required] ProjectsEfo project)
        {
            if (ModelState.IsValid)
            {
                ProjectsEfo newProject = await _projectService.SendProjectAsync(project);
                return StatusCode(StatusCodes.Status201Created, newProject);
            }

            return BadRequest(ModelState);
        }

        // PUT api/projects/{projectId}
        [HttpPut("{projectId}")]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProjectsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateProjectAsync(int projectId, [FromBody] ProjectsEfo updateProject)
        {
            try
            {
                ProjectsEfo project = await _projectService.UpdateProjectAsync(projectId, updateProject);

                if (project == null)
                {
                    return NotFound();
                }

                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/projects/{projectId}
        [HttpDelete("{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteProjectAsync(int projectId)
        {
            try
            {
                await _projectService.DeleteProjectAsync(projectId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
