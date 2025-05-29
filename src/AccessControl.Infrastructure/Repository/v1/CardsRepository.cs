using AccessControl.Domain.Interfaces.v1.Repository;
using AccessControl.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccessControl.Infrastructure.Repository.v1
{
    public class CardsRepository(DBContext context, ILogger<CardsRepository> logger) : ICardsRepository
    {
        private readonly DBContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly ILogger<CardsRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        private static string GenerateCardId(int cardNumber)
        {
            return $"Card{cardNumber}";
        }

        public Task<string> AddCard(int cardNumber, string firstName, string lastName)
        {
            _logger.LogInformation(nameof(AddCard));
            using (_context)
            {
                var cardId = GenerateCardId(cardNumber);

                if (_context.Cards.Any(c => c.Id == cardId))
                {
                    throw new InvalidOperationException($"Card with number {cardNumber} already exists.");
                }

                Card c = new Card()
                {
                    Id = $"Card{cardNumber}",
                    Number = cardNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    ValidFrom = DateTime.Now,
                    ValidTo = DateTime.Now + new TimeSpan(365, 0, 0, 0)
                };

                _context.Cards.Add(c);
                _context.SaveChanges();

                return Task.FromResult(c.Id);
            }
        }

        public Task<string> GrantAccess(int cardNumber, int doorNumber)
        {
            _logger.LogInformation(nameof(GrantAccess));
            var cardId = GenerateCardId(cardNumber);
            using (_context)
            {
                var card = _context.Cards.Where(c => c.Id == cardId).FirstOrDefault();
                if (card is null)
                {
                    throw new InvalidOperationException($"Card with number {cardNumber} does not exist.");
                }

                var door = _context.Doors.Where(d => d.Number == doorNumber).FirstOrDefault();
                if (door is null)
                {
                    throw new InvalidOperationException($"Door with number {doorNumber} does not exist.");
                }

                if (card.DoorsNumbersWithAccess.Any(d => d == doorNumber))
                {
                    throw new InvalidOperationException($"Card with number {cardNumber} already has access to door {doorNumber}.");
                }

                if (door.DoorsIdsWithAccess.Any(c => c == cardId))
                {
                    throw new InvalidOperationException($"Card with number {cardNumber} already has access to door {doorNumber}.");
                }

                card.DoorsNumbersWithAccess.Add(doorNumber);
                door.DoorsIdsWithAccess.Add(cardId);

                _context.SaveChanges();
            }

            return Task.FromResult("Granted");
        }

        public Task<string> CancelPermission(int cardNumber, int doorNumber)
        {
            _logger.LogInformation(nameof(CancelPermission));
            var cardId = GenerateCardId(cardNumber);

            using (_context)
            {
                var card = _context.Cards.Where(c => c.Id == cardId).FirstOrDefault();
                if (card is null)
                {
                    throw new InvalidOperationException($"Card with number {cardNumber} does not exist.");
                }

                var door = _context.Doors.Where(d => d.Number == doorNumber).FirstOrDefault();
                if (door is null)
                {
                    throw new InvalidOperationException($"Door with number {doorNumber} does not exist.");
                }

                if (!card.DoorsNumbersWithAccess.Any(d => d == doorNumber))
                {
                    throw new InvalidOperationException($"Card with number {cardNumber} does not have access to door {doorNumber}.");
                }

                if (!door.DoorsIdsWithAccess.Any(c => c == cardId))
                {
                    throw new InvalidOperationException($"Card with number {cardNumber} does not have access to door {doorNumber}.");
                }

                card.DoorsNumbersWithAccess.Remove(doorNumber);
                door.DoorsIdsWithAccess.Remove(cardId);
                _context.SaveChanges();
            }

            return Task.FromResult("Permission canceled");
        }

        public Task<IQueryable<Card>> ListCards()
        {
            _logger.LogInformation(nameof(ListCards));
            return Task.FromResult(_context.Cards.AsQueryable());
        }
    }
}
