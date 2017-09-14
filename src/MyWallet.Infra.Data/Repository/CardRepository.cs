using System;
using System.Data.Entity;
using System.Threading.Tasks;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.Context;
using MyWallet.Infra.Data.Contracts;
using MyWallet.Infra.Data.DataModels;
using MyWallet.Infra.Data.Repository.Shared;

namespace MyWallet.Infra.Data.Repository
{
    public class CardRepository : Repository<CardDataModel>, ICardRepository
    {
        public CardRepository(MyWalletContext context) : base(context)
        {
        }

        public async Task<CardDataModel> GetCreditCardByNumber(string creditCardNumber)
        {
            return await Context.Cards.FirstOrDefaultAsync(c => c.CardNumber.Equals(creditCardNumber));
        }

        public async Task<bool> RemoveCreditCard(CardDataModel cardDataModel)
        {
            using (var ctxTrans = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.Cards.Remove(cardDataModel);

                    await Context.SaveChangesAsync();

                    ctxTrans.Commit();
                    return true;
                }
                catch (Exception)
                {
                    ctxTrans.Rollback();
                    throw;
                }
                finally
                {
                    ctxTrans.Dispose();
                }
            }
        }
    }
}