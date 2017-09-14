using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Application.Contracts;
using MyWallet.Application.Dto;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.Contracts;

namespace MyWallet.Application.AppServices
{
    public class CardAppService : ICardAppService
    {

        private readonly ICardRepository _cardRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;
        public CardAppService(ICardRepository cardRepository, IMapper mapper, IWalletRepository walletRepository)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _walletRepository = walletRepository;
        }

        public async Task<CardDto> RegisterAsync(CardSaveOrUpdateDto cardDto)
        {
            var wallet = (await Task.Run(() => _walletRepository.Find(w => w.Id.Equals(cardDto.WalletId)))).FirstOrDefault();

            if (wallet == null) throw new Exception("wallet not found");

            var newCard = _mapper.Map<Card>(cardDto);

            wallet.Cards.Add(newCard);
            _walletRepository.Update(wallet);

            return _mapper.Map<CardDto>(newCard);

        }

        public async Task<CardDto> UpdateAsync(CardDto cardDto)
        {
            throw new System.NotImplementedException();
        }
    }
}