using Microsoft.AspNetCore.Mvc;
using AccessControl.Domain.Models;

namespace AccessControl.Controllers.v1
{
    [ApiController]
    [Route("api/v1/doors")]
    public class DoorsController : ControllerBase
    {
        private AccessControlService _doorsService;

        public DoorsController(AccessControlService doorsService)
        {
            _doorsService = doorsService;
        }

        [HttpPost(Name = "Create Door")]
        public Door Create([FromQuery] int doorNumber, [FromQuery] int doorType, [FromQuery] string doorName)
        {
            return _doorsService.AddDoor(doorNumber, doorType, doorName);
        }

        [HttpDelete(Name = "Remove Door")]
        public string Remove([FromQuery] int doorNumber)
        {
            throw new NotImplementedException();
        }
    }
}
