﻿using System;
using MyWallet.Domain.Models.Core;

namespace MyWallet.Domain.Models
{
    public class Purchase:Entity
    {
        public Purchase(Guid id, string description, decimal amount):base(id)
        {
            Description = description;
            Amount = amount;
        }

        protected Purchase() { }

        public string Description { get; private set; }
        public decimal Amount { get; private set; }
    }
}