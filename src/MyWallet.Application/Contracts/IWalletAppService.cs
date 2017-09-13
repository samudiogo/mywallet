using System;
using System.Threading.Tasks;
using MyWallet.Application.Dto;

namespace MyWallet.Application.Contracts
{
    public interface IWalletAppService
    {
        Task<WalletDto> GetWalletById(Guid id);
        Task<WalletDto> CreateWallet(WalletRegistrationDto wallet);
    }
}