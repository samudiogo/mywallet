using System.Threading.Tasks;
using MyWallet.Application.Dto;

namespace MyWallet.Application.Contracts
{
    public interface ICardAppService
    {
        Task<CardDto> RegisterAsync(CardSaveOrUpdateDto cardDto);

        Task<CardDto> UpdateAsync(CardSaveOrUpdateDto cardDto);

        Task<CardDto> GetCreditCardByNumber(string creditCardNumber);

        Task RemoveCreditCard(string creditCardNumber);

    }
}