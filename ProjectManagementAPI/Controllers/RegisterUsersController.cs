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
    public class RegisterUsersController : ControllerBase
    {
        private readonly IRegisterUserService _registerUserService;

        public RegisterUsersController(IRegisterUserService registerUserService)
        {
            _registerUserService=registerUserService;
        }

        // GET api/registerusers
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<RegisterUserEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<RegisterUserEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<RegisterUserEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<RegisterUserEfo>>> GetAllRegisterUsersAsync()
        {
            List<RegisterUserEfo> registerUsers = await _registerUserService.GetAllRegisterUsersAsync();

            return Ok(registerUsers);
        }

        // GET api/registerusers/byid/{registerUserId}
        [Authorize]
        [HttpGet("byid/{registerUserId}")]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetRegisterUserByIdAsync(int registerUserId)
        {
            RegisterUserEfo? registeruser = await _registerUserService.GetRegisterUserByIdAsync(registerUserId);

            if (registeruser == null)
            {
                return NotFound();
            }

            return Ok(registeruser);
        }

        // POST api/registerusers
        [HttpPost]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RegisterUserEfo>> SendRegisterUserAsync([FromBody, Required] RegisterUserEfo registerUser)
        {
            if (ModelState.IsValid)
            {
                RegisterUserEfo newRegistoUser = await _registerUserService.SendRegisterUserAsync(registerUser);

                UserEfo newUserProfile = await _registerUserService.SendNewUserProfileAsync(registerUser.UserName, registerUser.Password, registerUser.RegisterUserId);

                return StatusCode(StatusCodes.Status201Created, newRegistoUser);
            }

            return BadRequest(ModelState);
        }

        // POST api/registerusers/sendlogin
        [HttpPost("sendlogin")]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUserEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RegisterUserEfo>> SendLoginUserAsync([FromBody, Required] RegisterUserEfo registerUser)
        {
            RegisterUserEfo loginUser = await _registerUserService.SendLoginUserAsync(registerUser.UserName, registerUser.Password);

            if (loginUser != null)
            {
                return Ok(loginUser);
            }

            return Unauthorized();
        }

        // DELETE api/registerusers/{registerUserId}
        [Authorize]
        [HttpDelete("{registerUserId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteRegisterUserAsync(int registerUserId)
        {
            try
            {
                await _registerUserService.DeleteRegisterUserAsync(registerUserId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
