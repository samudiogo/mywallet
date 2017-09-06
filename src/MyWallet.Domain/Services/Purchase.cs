using System;
using System.Linq;
using MyWallet.Domain.Models;
using MyWallet.Domain.Services.Contracts;

namespace MyWallet.Domain.Services
{
    public class Purchase : IPurchase
    {
        private readonly ICardMonitor _cardMonitor;
        public Purchase(ref Wallet wallet, ICardMonitor cardMonitor)
        {
            _cardMonitor = cardMonitor;
            Wallet = wallet ?? throw new Exception("wallet cannot be null");
        }

        public Wallet Wallet { get; }

        public void Buy(Acquisition acquisition)
        {
            //check if amount is over the realimit
            if (acquisition.Amount > Wallet.RealLimit || acquisition.Amount > Wallet.GetMaximmumLimit())
                throw new Exception("Amount over RealLimit");

            if (!Wallet.Cards.Any()) throw new Exception("No credit card was found in the wallet");

            //get cards lists orderning by last due date and minimum limit
            var sortedCardList = Wallet.GetSortedCardList().ToList();

            var remainPurchaseAmount = acquisition.Amount;
            foreach (var card in sortedCardList)
            {
                if (card.IsPurchaseFitsLimit(remainPurchaseAmount))
                {
                    card.RegisterPurchase(new Acquisition(acquisition.Id, acquisition.Description, remainPurchaseAmount));
                    DecreaseLimit(card, remainPurchaseAmount);
                    break;
                }
                remainPurchaseAmount -= card.Limit;
                var partialPurchase = new Acquisition(acquisition.Id, acquisition.Description, card.Limit);
                card.RegisterPurchase(partialPurchase);
                DecreaseLimit(card, partialPurchase.Amount);
            }
        }

        private void DecreaseLimit(Card card, decimal amount)
        {
            var cardRef = Wallet.Cards.FirstOrDefault(c => c.Id.Equals(card.Id));
            _cardMonitor.DecreaseLimit(ref cardRef, amount);

        }
    }
}