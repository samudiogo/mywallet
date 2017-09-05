using System;
using MyWallet.Domain.Models.Core;

namespace MyWallet.Domain.Models
{
    public class Purchase:Entity
    {
        public Purchase( string description, decimal amount):base(Guid.NewGuid())
        {
            Description = description;
            Amount = amount;
        }

        protected Purchase() { }

        public string Description { get; private set; }
        public decimal Amount { get; private set; }
    }
}