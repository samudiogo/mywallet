using System;
using MyWallet.Domain.Models.Core;

namespace MyWallet.Domain.Models
{
    public class Acquisition:Entity
    {
        public Acquisition(Guid id, string description, decimal amount):base(id)
        {
            Description = description;
            Amount = amount;
        }

        protected Acquisition() { }

        public string Description { get; private set; }
        public decimal Amount { get; private set; }
    }
}