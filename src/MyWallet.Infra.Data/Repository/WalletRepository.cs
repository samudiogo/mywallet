using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.Context;
using MyWallet.Infra.Data.Contracts;
using MyWallet.Infra.Data.DataModels;
using MyWallet.Infra.Data.Repository.Shared;

namespace MyWallet.Infra.Data.Repository
{
    public class WalletRepository : Repository<WalletDataModel>, IWalletRepository
    {
        public WalletRepository(MyWalletContext context) : base(context)
        {
        }
        public new void Add(WalletDataModel wallet)
        {
            Context.Entry(wallet.Owner).State = EntityState.Unchanged;
            base.Add(wallet);
        }

        public new IEnumerable<WalletDataModel> Find(Expression<Func<WalletDataModel, bool>> predicate)
        {
            return Context.Wallets.AsNoTracking().Include(w => w.Owner).Include(w => w.Cards).Where(predicate);
        }
    }
}