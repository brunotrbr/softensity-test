using AccessControl.Application.UseCases.v1;
using AccessControl.Domain.Enums;
using AccessControl.Domain.Interfaces.v1.UseCases;
using AccessControl.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Controllers.v1
{
    [ApiController]
    [Route("api/v1/doors")]
    public class DoorsController(IDoorsUseCase doorsUseCase) : ControllerBase
    {
        private readonly IDoorsUseCase _doorsUseCase = doorsUseCase ?? throw new ArgumentNullException(nameof(doorsUseCase));

        [HttpPost(Name = "Create Door")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Door))]
        public async Task<ActionResult<Door>> Create([FromQuery] int doorNumber, [FromQuery] DoorTypeEnum doorType, [FromQuery] string doorName)
        {
            return new ObjectResult(await _doorsUseCase.CreateDoor(doorNumber, doorType, doorName)) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete(Name = "Remove Door")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult<string>> Remove([FromQuery] int doorNumber)
        {
            return Ok(await _doorsUseCase.RemoveDoor(doorNumber));
        }

        [HttpGet(Name = "List doors")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Door>))]
        public async Task<ActionResult<List<Door>>> Get()
        {
            return Ok(await _doorsUseCase.ListDoors());
        }
    }
}
