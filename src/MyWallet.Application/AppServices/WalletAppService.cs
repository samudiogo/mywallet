using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Application.Contracts;
using MyWallet.Application.Dto;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.Contracts;
using MyWallet.Infra.Data.DataModels;

namespace MyWallet.Application.AppServices
{
    public class WalletAppService : IWalletAppService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUserAppService _userAppService;
        private readonly IMapper _mapper;

        public WalletAppService(IWalletRepository walletRepository, IMapper mapper, IUserAppService userAppService)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
            _userAppService = userAppService;
        }

        public async Task<WalletDto> GetWalletById(Guid id)
        {
            var result = (await Task.Run(() => _walletRepository.Find(w => w.Id.Equals(id)))).FirstOrDefault();
            if (result == null) throw new Exception("wallet not exists");

            return _mapper.Map<WalletDto>(result);
        }

        public async Task<WalletDto> CreateWallet(WalletSaveOrUpdateDto walletDto)
        {

            var userDto = await _userAppService.GetUserByEmailAsync(walletDto.UserEmail);
            var walletId = Guid.NewGuid();
            if (userDto == null) throw new Exception("user not found.");
            var wallet = new Wallet(walletId, _mapper.Map<User>(userDto));

            _walletRepository.Add(_mapper.Map<WalletDataModel>(wallet));

            return _mapper.Map<WalletDto>(wallet);
        }
    }
}
