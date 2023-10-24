using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Entities.Efos;
using ProjectManagement.Services.Services;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StagesController : ControllerBase
    {
        private readonly IStagesService _stagesService;

        public StagesController(IStagesService stagesService)
        {
            _stagesService = stagesService;
        }

        // GET api/stages
        [HttpGet]
        [ProducesResponseType(typeof(List<StageEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<StageEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<StageEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<StageEfo>>> GetAllStagesAsync()
        {
            List<StageEfo> stages = await _stagesService.GetAllStagesAsync();

            return Ok(stages);
        }

        // GET api/stages/{stageId}
        [Authorize]
        [HttpGet("{stageId}")]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetStageByIdAsync(int stageId)
        {
            StageEfo stages = await _stagesService.GetStageByIdAsync(stageId);

            if (stages == null)
            {
                return NotFound();
            }

            return Ok(stages);
        }

        // GET api/stages/byname/{stageName}
        [HttpGet("byname/{stageName}")]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetStageByNameAsync(string stageName)
        {
            StageEfo stages = await _stagesService.GetStageByNameAsync(stageName);

            if (stages == null)
            {
                return NotFound();
            }

            return Ok(stages);
        }

        // POST api/stages
        [HttpPost]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<StageEfo>> SendStageAsync([FromBody, Required] StageEfo stage)
        {
            if (ModelState.IsValid)
            {
                StageEfo newStage = await _stagesService.SendStageAsync(stage);
                return StatusCode(StatusCodes.Status201Created, newStage);
            }

            return BadRequest(ModelState);
        }

        // PUT api/stages/{stageId}
        [HttpPut("{stageId}")]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(StageEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateStageAsync(int stageId, [FromBody] StageEfo updateStage)
        {
            try
            {
                StageEfo stage = await _stagesService.UpdateStageAsync(stageId, updateStage);

                if (stage == null)
                {
                    return NotFound();
                }

                return Ok(stage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/stages/{stageId}
        [HttpDelete("{stageId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteStageAsync(int stageId)
        {
            try
            {
                await _stagesService.DeleteStageAsync(stageId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
