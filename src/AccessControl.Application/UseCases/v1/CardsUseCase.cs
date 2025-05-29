using AccessControl.Domain.Interfaces.v1.Repository;
using AccessControl.Domain.Interfaces.v1.UseCases;
using AccessControl.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace AccessControl.Application.UseCases.v1
{
    public class CardsUseCase(ICardsRepository cardsRepository, ILogger<CardsUseCase> logger) : ICardsUseCase
    {
        private readonly ICardsRepository _cardsRepository = cardsRepository ?? throw new ArgumentNullException(nameof(cardsRepository));
        private readonly ILogger<CardsUseCase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<string> CreateCard(int cardNumber, string firstName, string lastName)
        {
            _logger.LogInformation(nameof(CreateCard));
            return await _cardsRepository.AddCard(cardNumber, firstName, lastName);
        }

        public async Task<string> GrantAccess(int cardNumber, int doorNumber)
        {
            _logger.LogInformation(nameof(GrantAccess));
            return await _cardsRepository.GrantAccess(cardNumber, doorNumber);
        }

        public async Task<string> CancelPermission(int cardNumber, int doorNumber)
        {
            _logger.LogInformation(nameof(CancelPermission));
            return await _cardsRepository.CancelPermission(cardNumber, doorNumber);
        }

        public async Task<IQueryable<Card>> ListCards()
        {
            _logger.LogInformation(nameof(ListCards));
            return await _cardsRepository.ListCards();
        }
    }
}
