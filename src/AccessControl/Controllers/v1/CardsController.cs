using Microsoft.AspNetCore.Mvc;
using AccessControl.Domain.Interfaces.v1.UseCases;

namespace AccessControl.Controllers.v1
{
    [ApiController]
    [Route("api/v1/cards")]
    public class CardsController(ICardsUseCase cardsUseCase) : ControllerBase
    {
        private readonly ICardsUseCase _cardsUseCase = cardsUseCase  ?? throw new ArgumentNullException(nameof(cardsUseCase));

        [HttpPost(Name = "Create Card")]
        public async Task<string> Create([FromQuery] int cardNumber, [FromQuery] string firstName, [FromQuery] string lastName)
        {
            return await _cardsUseCase.CreateCard(cardNumber, firstName, lastName);
        }

        [HttpPut(Name = "Grant Access")]
        public async Task<string> GrantAccess([FromQuery] int cardNumber , [FromQuery] int doorNumber)
        {
            return await _cardsUseCase.GrantAccess(cardNumber, doorNumber);
        }

        [HttpDelete(Name = "Cancel Permission")]
        public async Task<string> CancelPermission([FromQuery] string permissionId)
        {
            return await _cardsUseCase.CancelPermission(permissionId);
        }
    }
}
