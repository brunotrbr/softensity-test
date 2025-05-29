using AccessControl.Domain.Models;

namespace AccessControl.Domain.Interfaces.v1.Repository
{
    public interface ICardsRepository
    {
        public abstract Task<string> AddCard(int cardNumber, string firstName, string lastName);

        public abstract Task<string> GrantAccess(int cardNumber, int doorNumber);

        public abstract Task<string> CancelPermission(int cardNumber, int doorNumber);

        public abstract Task<IQueryable<Card>> ListCards();
    }
}
