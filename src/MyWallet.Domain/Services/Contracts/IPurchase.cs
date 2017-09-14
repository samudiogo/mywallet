using MyWallet.Domain.Models;

namespace MyWallet.Domain.Services.Contracts
{
    public interface IPurchase
    {
        Wallet Wallet { get;  }
        bool Buy(Acquisition acquisition);
    }
}