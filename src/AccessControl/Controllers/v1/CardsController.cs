using AccessControl.Domain.Interfaces.v1.UseCases;
using AccessControl.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Controllers.v1
{
    [ApiController]
    [Route("api/v1/cards")]
    public class CardsController(ICardsUseCase cardsUseCase, ILogger<CardsController> logger) : ControllerBase
    {
        private readonly ICardsUseCase _cardsUseCase = cardsUseCase ?? throw new ArgumentNullException(nameof(cardsUseCase));
        private readonly ILogger<CardsController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        [HttpPost(Name = "Create Card")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Door))]
        public async Task<ActionResult<string>> Create([FromQuery] int cardNumber, [FromQuery] string firstName, [FromQuery] string lastName)
        {
            _logger.LogInformation($"{nameof(CardsController)}/{nameof(Create)}");
            _logger.LogInformation($"Creating card with number {cardNumber}, first name {firstName}, and last name {lastName}");
            return new ObjectResult(await _cardsUseCase.CreateCard(cardNumber, firstName, lastName)) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut(Name = "Grant Access")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult<string>> GrantAccess([FromQuery] int cardNumber, [FromQuery] int doorNumber)
        {
            _logger.LogInformation(nameof(GrantAccess));
            _logger.LogInformation($"Granting access for card number {cardNumber} to door number {doorNumber}");
            return Ok(await _cardsUseCase.GrantAccess(cardNumber, doorNumber));
        }

        [HttpDelete(Name = "Cancel Permission")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<string> CancelPermission([FromQuery] int cardNumber, [FromQuery] int doorNumber)
        {
            _logger.LogInformation(nameof(CancelPermission));
            return await _cardsUseCase.CancelPermission(cardNumber, doorNumber);
        }

        [HttpGet(Name = "List cards")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Card>))]
        public async Task<ActionResult<List<Card>>> Get()
        {
            _logger.LogInformation(nameof(Get));
            return Ok(await _cardsUseCase.ListCards());
        }
    }
}
