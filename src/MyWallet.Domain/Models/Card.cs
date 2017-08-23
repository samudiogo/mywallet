using System;
using System.Security.Cryptography;
using MyWallet.Domain.Models.Core;
using static System.Text.Encoding;
using Convert = System.Convert;

namespace MyWallet.Domain.Models
{
    public class Card: Entity
    {
        public Card(Guid id, string number, DateTime dueDate, DateTime expirationDate, int cvv, decimal limit, bool isReleasingCreditAccepted)
        {
            Id = id;
            Number = HashingCardNumber(number);
            DueDate = dueDate;
            ExpirationDate = expirationDate;
            Cvv = cvv;
            if(limit < 0) throw new Exception("The limit cannot be negative.");
            Limit = limit;

            _isReleasingCreditAccepted = isReleasingCreditAccepted;
        }

        public Card(){}
        public string Number { get; }
        public DateTime DueDate { get; }
        public DateTime ExpirationDate { get; } 
        public int Cvv { get; }
        public decimal Limit { get; }

        private readonly bool _isReleasingCreditAccepted;

        public bool IsReleasingCreditAccepted()
        {
            return _isReleasingCreditAccepted;
        }

        

        private static string HashingCardNumber( string cardNumber)
        {
            using (var sha = SHA512.Create())
                return Convert.ToBase64String(sha.ComputeHash(UTF8.GetBytes(cardNumber)));
        }



    
    }
}