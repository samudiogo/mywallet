using System.Data.Entity;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.Context;
using MyWallet.Infra.Data.Contracts;
using MyWallet.Infra.Data.Repository.Shared;

namespace MyWallet.Infra.Data.Repository
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(MyWalletContext context) : base(context)
        {
        }
        public new void Add(Wallet wallet)
        {
            Context.Entry(wallet.Owner).State = EntityState.Unchanged;
            base.Add(wallet);
        }
    }
}