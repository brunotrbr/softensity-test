using AccessControl.Domain.Enums;
using AccessControl.Domain.Interfaces.v1.UseCases;
using AccessControl.Domain.Models;
using AccessControl.ExceptionHandlers;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Controllers.v1
{
    [ApiController]
    [Route("api/v1/doors")]
    public class DoorsController(IDoorsUseCase doorsUseCase, ILogger<DoorsController> logger) : ControllerBase
    {
        private readonly IDoorsUseCase _doorsUseCase = doorsUseCase ?? throw new ArgumentNullException(nameof(doorsUseCase));
        private readonly ILogger<DoorsController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        [HttpPost(Name = "Create Door")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Door))]
        public async Task<ActionResult<Door>> Create([FromQuery] int doorNumber, [FromQuery] DoorTypeEnum doorType, [FromQuery] string doorName)
        {
            _logger.LogInformation($"{nameof(DoorsController)}/{nameof(Create)}");
            _logger.LogInformation($"Creating door with number {doorNumber}, type {doorType.GetDescription()}, and name {doorName}");
            return new ObjectResult(await _doorsUseCase.CreateDoor(doorNumber, doorType, doorName)) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete(Name = "Remove Door")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult<string>> Remove([FromQuery] int doorNumber)
        {
            _logger.LogInformation(nameof(Remove));
            _logger.LogInformation($"Removing door with number {doorNumber}");
            return Ok(await _doorsUseCase.RemoveDoor(doorNumber));
        }

        [HttpGet(Name = "List doors")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Door>))]
        public async Task<ActionResult<List<Door>>> Get()
        {
            _logger.LogInformation(nameof(Get));
            return Ok(await _doorsUseCase.ListDoors());
        }
    }
}
