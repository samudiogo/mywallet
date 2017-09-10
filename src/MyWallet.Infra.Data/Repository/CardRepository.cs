using MyWallet.Domain.Models;
using MyWallet.Infra.Data.Context;
using MyWallet.Infra.Data.Contracts;
using MyWallet.Infra.Data.Repository.Shared;

namespace MyWallet.Infra.Data.Repository
{
    public class CardRepository:Repository<Card>,ICardRepository
    {
        public CardRepository(MyWalletContext context) : base(context)
        {
        }
    }
}