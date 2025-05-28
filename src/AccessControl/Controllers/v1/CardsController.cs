using Microsoft.AspNetCore.Mvc;
using AccessControl.Domain.Models;

namespace AccessControl.Controllers.v1
{
    [ApiController]
    [Route("api/v1/cards")]
    public class CardsController : ControllerBase
    {
        private AccessControlService _cardsService;

        public CardsController(AccessControlService cardsService)
        {
            _cardsService = cardsService;
        }

        [HttpPost]
        public string Create([FromQuery] int cardNumber, [FromQuery] string firstName, [FromQuery] string lastName)
        {
            return _cardsService.AddCard(cardNumber, firstName, lastName);
        }

        [HttpPut]
        public string GrantAccess([FromQuery] int cardNumber , [FromQuery] int doorNumber)
        {
            return _cardsService.GrantAccess(cardNumber, doorNumber);
        }

        [HttpDelete]
        public string CancelPermission([FromQuery] string permissionId)
        {
            throw new NotImplementedException();
        }
    }
}
