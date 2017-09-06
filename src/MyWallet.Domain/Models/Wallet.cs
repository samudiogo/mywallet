using System;
using System.Collections.Generic;
using System.Linq;
using MyWallet.Domain.Models.Core;

namespace MyWallet.Domain.Models
{
    public class Wallet : Entity
    {

        #region Events
        //The Observable pattern suits well when Wallet updates your card list and need to ajust automaticaly the realimit.
        //but, now (08/30/2017 04am) I have no idea about how to implement this pattern.

        #endregion

        public Wallet(Guid id, User owner) : base(id)
        {
            Owner = owner;
            RealLimit = Cards.Any() ? Cards.Sum(c => c.Limit) : 0;
        }


        public User Owner { get; }
        public ICollection<Card> Cards { get; } = new List<Card>();

        public decimal RealLimit { get; private set; }

        public void AjustRealtLimit(decimal newRealLimit)
        {
            var newLimit = newRealLimit < 0 ? RealLimit + newRealLimit : newRealLimit;
            if (newLimit < 0) { RealLimit = 0m; return; }

            if (newLimit > GetMaximmumLimit()) throw new Exception("Your real limit can not to be bigger then the amount of your cards.");

            RealLimit = newLimit;
        }

        public void AddNewCard(Card newCard) => Cards.Add(newCard);

        /// <summary>
        /// This method should have only one responsability //TODO: Improve it
        /// </summary>
        /// <param name="cardId"></param>
        public void RemoveCardByIdAndUpdateLimit(Guid cardId)
        {
            var card = Cards.FirstOrDefault(c => c.Id.Equals(cardId));
            if (card == null) throw new Exception("There is no card registered with this identifier.");
            if (Cards.Remove(card)) AjustRealtLimit(-card.Limit);

        }

        public decimal GetMaximmumLimit() => Cards.Sum(c => c.Limit);

        #region purchase

        private IEnumerable<Card> GetIdealCardList()
        {
            return IsSameDueDateCardList() ? Cards.OrderBy(c => c.Limit) : Cards.OrderByDescending(c => c.DueDate).ThenBy(c => c.Limit);
        }

        private bool IsSameDueDateCardList()
        {
            var cardList = Cards.OrderByDescending(c => c.DueDate).ToList();
            var isSameDueList = false;
            for (var i = 0; i < cardList.Count - 1; i++)
            {
                var card = cardList[i];//need to improve with elementAt
                var next = cardList[i + 1];
                if (card != null && next != null && card.IsSameDueDate(next)) isSameDueList = true;
                else isSameDueList = false;
            }
            return isSameDueList;
        }

        public void Buy(Purchase purchase)
        {
            //check if amount is over the realimit
            if (purchase.Amount > RealLimit || purchase.Amount > GetMaximmumLimit())
                throw new Exception("Amount over RealLimit");

            if (!Cards.Any()) throw new Exception("No credit card was found in the wallet");

            //get cards lists orderning by last due date and minimum limit
            var idealCardList = GetIdealCardList().ToList();

            var remainPurchaseAmount = purchase.Amount;
            foreach (var card in idealCardList)
                if (card.IsPurchaseFitsLimit(remainPurchaseAmount))
                    card.RegisterPurchase(new Purchase(purchase.Id, purchase.Description, remainPurchaseAmount));
                else
                {
                    remainPurchaseAmount -= card.Limit;
                    card.RegisterPurchase(new Purchase(purchase.Id, purchase.Description, card.Limit));
                }
        }

        #endregion


        //TODO: implment C# events to update realLimit
    }
}