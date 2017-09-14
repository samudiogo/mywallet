using System.Threading.Tasks;
using MyWallet.Application.Dto;

namespace MyWallet.Application.Contracts
{
    public interface ICardAppService
    {
        Task<CardDto> RegisterAsync(CardSaveOrUpdateDto cardDto);

        Task<CardDto> UpdateAsync(CardDto cardDto);

    }
}