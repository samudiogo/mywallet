using System.Threading.Tasks;
using MyWallet.Infra.Data.DataModels;

namespace MyWallet.Infra.Data.Contracts
{
    public interface ICardRepository:IRepository<CardDataModel>
    {
        Task<CardDataModel> GetCreditCardByNumber(string creditCardNumber);

        Task<bool> RemoveCreditCard(CardDataModel cardDataModel);
    }
}