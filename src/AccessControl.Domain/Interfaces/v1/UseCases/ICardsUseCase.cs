using AccessControl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Domain.Interfaces.v1.UseCases
{
    public interface ICardsUseCase
    {
        public abstract Task<string> CreateCard(int cardNumber, string firstName, string lastName);

        public abstract Task<string> GrantAccess(int cardNumber, int doorNumber);

        public abstract Task<string> CancelPermission(int cardNumber, int doorNumber);
        public abstract Task<IQueryable<Card>> ListCards();
    }
}
