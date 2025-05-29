using AccessControl.Domain.Interfaces.v1.Repository;
using AccessControl.Domain.Models;

namespace AccessControl.Infrastructure.Repository.v1
{
    public class CardsRepository(DBContext context) : ICardsRepository
    {
        private readonly DBContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public Task<string> AddCard(int cardNumber, string firstName, string lastName)
        {
            using (_context)
            {
                Card c = new Card()
                {
                    Id = $"Card{cardNumber}",
                    Number = cardNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    ValidFrom = DateTime.Now,
                    ValidTo = DateTime.Now + new TimeSpan(365, 0, 0, 0)
                };

                _context.Cards.AddAsync(c);
                _context.SaveChangesAsync();

                return Task.FromResult(c.Id);
            }
        }

        public Task<string> GrantAccess(int cardNumber, int doorNumber)
        {
            try
            {
                using (_context)
                {
                    _context.Cards.Where(c => c.Number == cardNumber).First().DoorsNumbersWithAccess.Add(doorNumber);
                    _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult("Failure");
            }

            return Task.FromResult("Success");
        }

        public Task<string> CancelPermission(string permissionId)
        {
            throw new NotImplementedException();
        }
    }
}
