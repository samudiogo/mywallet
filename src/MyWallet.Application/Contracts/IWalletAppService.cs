using System;
using System.Threading.Tasks;
using MyWallet.Application.Dto;

namespace MyWallet.Application.Contracts
{
    public interface IWalletAppService
    {
        Task<WalletDto> GetWalletById(Guid id);
        Task<WalletDto> CreateWallet(WalletSaveOrUpdateDto wallet);

        Task<WalletDto> AssociateCreditCardToWallet(WalletDto walletDto);
        AcquisitionDto RegisterPurchase(AcquisitionSaveOrUpdateDto acquisitionSaveOrUpdateDto);
    }
}