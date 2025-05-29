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
        public async Task<Door> Create([FromQuery] int doorNumber, [FromQuery] int doorType, [FromQuery] string doorName)
        {
            return await _doorsUseCase.CreateDoor(doorNumber, doorType, doorName);
        }

        [HttpDelete(Name = "Remove Door")]
        public async Task<string> Remove([FromQuery] int doorNumber)
        {
            return await _doorsUseCase.RemoveDoor(doorNumber);
        }
    }
}
