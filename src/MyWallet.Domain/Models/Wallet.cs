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
            var newLimit = newRealLimit< 0 ? RealLimit + newRealLimit: newRealLimit;
            if ( newLimit < 0) {RealLimit = 0m; return;}

            if (newLimit > GetMaximmumLimit()) throw new Exception("Your real limit can not to be bigger then the amount of your cards.");

            RealLimit = newLimit;
        }

        public void AddNewCard(Card newCard) => Cards.Add(newCard);


        public void RemoveCardById(Guid cardId)
        {
            var card = Cards.FirstOrDefault(c => c.Id.Equals(cardId));
            if (card == null) throw new Exception("There is no card registered with this identifier.");
            if(Cards.Remove(card)) AjustRealtLimit(-card.Limit);

        }

        public decimal GetMaximmumLimit() => Cards.Sum(c => c.Limit);

        //TODO: implment C# events to update realLimit
    }
}