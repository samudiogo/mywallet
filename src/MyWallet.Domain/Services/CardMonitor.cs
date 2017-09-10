using System.Linq;
using MyWallet.Domain.Models;
using MyWallet.Domain.Services.Contracts;

namespace MyWallet.Domain.Services
{
    public class CardMonitor : ICardMonitor
    {
        public void DecreaseLimit(ref Card creditCard, decimal amountValue)
        {
            var newLimit = creditCard.Limit - amountValue;
            creditCard.UpdateLimit(newLimit);
        }

        public void IncreaseLimit(ref Card creditCard, decimal amountValue)
        {
            var newLimit = creditCard.Limit + amountValue;
            creditCard.UpdateLimit(newLimit);
            
        }
    }
}