using AccessControl.Domain.Interfaces.v1.Repository;
using AccessControl.Domain.Interfaces.v1.UseCases;

namespace AccessControl.Application.UseCases.v1
{
    public class CardsUseCase(ICardsRepository cardsRepository) : ICardsUseCase
    {
        private readonly ICardsRepository _cardsRepository = cardsRepository ?? throw new ArgumentNullException(nameof(cardsRepository));

        public async Task<string> CreateCard(int cardNumber, string firstName, string lastName)
        {
            return await _cardsRepository.AddCard(cardNumber, firstName, lastName);
        }

        public async Task<string> GrantAccess(int cardNumber, int doorNumber)
        {
            return await _cardsRepository.GrantAccess(cardNumber, doorNumber);
        }

        public async Task<string> CancelPermission(string permissionId)
        {
            return await _cardsRepository.CancelPermission(permissionId);
        }
    }
}
