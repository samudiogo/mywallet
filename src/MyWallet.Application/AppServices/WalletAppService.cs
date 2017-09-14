using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Application.Contracts;
using MyWallet.Application.Dto;
using MyWallet.Domain.Models;
using MyWallet.Domain.Services;
using MyWallet.Domain.Services.Contracts;
using MyWallet.Infra.Data.Contracts;
using MyWallet.Infra.Data.DataModels;

namespace MyWallet.Application.AppServices
{
    public class WalletAppService : IWalletAppService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUserAppService _userAppService;
        private readonly IAcquisitionRepository _acquisitionRepository;
        private readonly IMapper _mapper;

        public WalletAppService(IWalletRepository walletRepository, IMapper mapper, IUserAppService userAppService, IAcquisitionRepository acquisitionRepository)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
            _userAppService = userAppService;
            _acquisitionRepository = acquisitionRepository;
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

        public async Task<WalletDto> AssociateCreditCardToWallet(WalletDto walletDto)
        {
            throw new NotImplementedException();
        }


        public AcquisitionDto RegisterPurchase(AcquisitionSaveOrUpdateDto acquisitionSaveOrUpdateDto)
        {
            var walletDbModel = _walletRepository.GetById(acquisitionSaveOrUpdateDto.WalletId);

            if (walletDbModel == null) throw new Exception("wallet not found.");

            if (!walletDbModel.Cards.Any()) throw new Exception("wallet without a card.");

            var walletDomain = _mapper.Map<Wallet>(walletDbModel);



            var cardMonitor = new CardMonitor();

            var purchaseService = new Purchase(ref walletDomain, cardMonitor);

            var acquisitonDomain = _mapper.Map<Acquisition>(acquisitionSaveOrUpdateDto);

            if (!purchaseService.Buy(acquisitonDomain))
                throw new Exception("error while processing the payment");

            _walletRepository.Update(_mapper.Map<WalletDataModel>(purchaseService.Wallet));

            var purchaseDb = _acquisitionRepository.GetById(acquisitonDomain.Id);

            return _mapper.Map<AcquisitionDto>(purchaseDb);

        }
    }
}

