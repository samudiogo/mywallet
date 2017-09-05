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

        public void Buy(Purchase purchase)
        {
            //check if amount is over the realimit
            if (purchase.Amount > RealLimit) throw new Exception("Amount over RealLimit");

            //get cards lists orderning by last due date and minimum limit
            var idealCardList = Cards.OrderByDescending(c => c.DueDate)
                                 .ThenBy(c => c.Limit);
            Card idealCard;
            var cardsSameDue = new List<Card>();
            if (idealCardList.Count() == 1)
                idealCard = idealCardList.First();
            else
            {

                for (var i = 0; i < idealCardList.Count(); i++)
                {
                    var card = idealCardList.ElementAtOrDefault(i);
                    var next = idealCardList.ElementAtOrDefault(i + 1);
                    if ((card != null && next != null) && card.IsSameDueDate(next))
                    {
                        cardsSameDue.Add(card);
                        cardsSameDue.Add(next);
                    }
                }
                idealCard = cardsSameDue.OrderBy(c => c.Limit).First();
            }
            if (idealCard.IsPurchaseFitsLimit(purchase.Amount))
                idealCard.RegisterPurchase(purchase);
            else if (purchase.Amount <= GetMaximmumLimit())
            {
                var firtstPortion = new Purchase(purchase.Description, purchase.Amount - idealCard.Limit);
                idealCard.RegisterPurchase(firtstPortion);

                var remainAmount = (int)(purchase.Amount - firtstPortion.Amount) / cardsSameDue.Count;

                //if wallet have another same due card, te
                var remainCards = cardsSameDue.Any() ?
                    cardsSameDue.Where(c => c.Id != idealCard.Id) :
                    idealCardList.Where(c => c.Id != idealCard.Id);
                
                var nextCard = remainCards
                    .OrderByDescending(c => c.DueDate)//Assert the right order
                    .ThenBy(c => c.Limit)// TODO: transform it on extension method
                    .First();

                if (nextCard.IsPurchaseFitsLimit(remainAmount))
                    nextCard.RegisterPurchase(new Purchase(purchase.Description, remainAmount));
                
                //TODO: refact this method (turn it recursive)

            }
        }

        #endregion


        //TODO: implment C# events to update realLimit
    }
}