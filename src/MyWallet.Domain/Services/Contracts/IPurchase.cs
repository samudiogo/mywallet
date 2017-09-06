using MyWallet.Domain.Models;

namespace MyWallet.Domain.Services.Contracts
{
    public interface IPurchase
    {
        Wallet Wallet { get;  }
        void Buy(Acquisition acquisition);
    }
}