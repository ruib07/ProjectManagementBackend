using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Entities.Efos;
using ProjectManagement.Services.Services;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        // GET api/tasks
        [HttpGet]
        [ProducesResponseType(typeof(List<TasksEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<TasksEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<TasksEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<TasksEfo>>> GetAllTasksAsync()
        {
            List<TasksEfo> tasks = await _tasksService.GetAllTasksAsync();

            return Ok(tasks);
        }

        // GET api/tasks/{taskId}
        [Authorize]
        [HttpGet("{taskId}")]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetTaskByIdAsync(int taskId)
        {
            TasksEfo tasks = await _tasksService.GetTaskByIdAsync(taskId);

            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        // GET api/tasks/byname/{taskName}
        [HttpGet("byname/{taskName}")]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetTaskByNameAsync(string taskName)
        {
            TasksEfo tasks = await _tasksService.GetTaskByNameAsync(taskName);

            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        // POST api/tasks
        [HttpPost]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<TasksEfo>> SendTaskAsync([FromBody, Required] TasksEfo task)
        {
            if (ModelState.IsValid)
            {
                TasksEfo newTask = await _tasksService.SendTaskAsync(task);
                return StatusCode(StatusCodes.Status201Created, newTask);
            }

            return BadRequest(ModelState);
        }

        // PUT api/tasks/{taskId}
        [HttpPut("{taskId}")]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TasksEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateTaskAsync(int taskId, [FromBody] TasksEfo updateTask)
        {
            try
            {
                TasksEfo task = await _tasksService.UpdateTaskAsync(taskId, updateTask);

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/tasks/{taskId}
        [HttpDelete("{taskId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteTaskAsync(int taskId)
        {
            try
            {
                await _tasksService.DeleteTaskAsync(taskId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
