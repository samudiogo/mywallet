using MyWallet.Domain.Models;

namespace MyWallet.Domain.Services.Contracts
{
    public interface ICardMonitor
    {
        void DecreaseLimit(ref Card creditCard, decimal amountValue);
        void IncreaseLimit(ref Card creditCard, decimal amountValue);

    }
}