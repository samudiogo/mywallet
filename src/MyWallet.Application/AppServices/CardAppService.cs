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

            wallet.Cards.Add(_mapper.Map<CardDataModel>(newCard));

            _walletRepository.Update(wallet);

            return _mapper.Map<CardDto>(newCard);

        }

        public async Task<CardDto> UpdateAsync(CardSaveOrUpdateDto cardDto)
        {
            var dataCard = _mapper.Map<CardDataModel>(cardDto);
            _cardRepository.Update(dataCard);

            var updatedCard = await _cardRepository.GetCreditCardByNumber(dataCard.CardNumber);

            return _mapper.Map<CardDto>(updatedCard);
        }

        public async Task<CardDto> GetCreditCardByNumber(string creditCardNumber)
        {
            if (string.IsNullOrEmpty(creditCardNumber)) throw new Exception("creditCard can not to be null or empty");

            var result = await _cardRepository.GetCreditCardByNumber(creditCardNumber);

            if (result == null) throw new Exception($"Credit card not found using the {creditCardNumber} identification.");

            return _mapper.Map<CardDto>(result);

        }

        public async Task RemoveCreditCard(string creditCardNumber)
        {
            if (string.IsNullOrEmpty(creditCardNumber)) throw new Exception("creditCard can not to be null or empty");

            var result = await _cardRepository.GetCreditCardByNumber(creditCardNumber);

            if (result == null) throw new Exception($"Credit card not found using the {creditCardNumber} identification.");

            await _cardRepository.RemoveCreditCard(_mapper.Map<CardDataModel>(result));
        }
    }
}