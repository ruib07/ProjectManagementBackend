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
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        // GET api/users
        [HttpGet]
        [ProducesResponseType(typeof(List<UserEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<UserEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<UserEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<UserEfo>>> GetAllUsersAsync()
        {
            List<UserEfo> users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        // GET api/users/{userId}
        [Authorize]
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            UserEfo user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET api/users/byname/{username}
        [HttpGet("byname/{username}")]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetUserByNameAsync(string username)
        {
            UserEfo user = await _userService.GetUserByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT api/users/byname/{username}
        [HttpPut("{username}")]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateUserProfileAsync(string username, [FromBody] UserEfo updateUser)
        {
            try
            {
                UserEfo user = await _userService.UpdateUserProfileAsync(username, updateUser);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/users/{userId}
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteUserAync(int userId)
        {
            try
            {
                await _userService.DeleteUserAsync(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
