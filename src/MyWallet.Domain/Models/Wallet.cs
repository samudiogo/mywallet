using System;
using System.Collections.Generic;
using System.Linq;
using MyWallet.Domain.Models.Core;

namespace MyWallet.Domain.Models
{
    public class Wallet : Entity
    {
        public Wallet(Guid id, User owner) : base(id)
        {
            Owner = owner;
            RealLimit = Cards.Any() ? Cards.Sum(c => c.Limit) : 0;
        }


        public User Owner { get; }
        public ICollection<Card> Cards { get; } = new List<Card>();

        public decimal RealLimit { get; private set; }

        public void AjusRealtLimit(decimal newRealLimit)
        {
            if (newRealLimit < 0) throw new Exception("Limit can not to be negative.");

            if(newRealLimit > GetMaximmumLimit()) throw new Exception("Your real limit can not to be bigger then the amount of your cards.");

            RealLimit = newRealLimit;
        }

        public decimal GetMaximmumLimit() => Cards.Sum(c => c.Limit);
    }
}