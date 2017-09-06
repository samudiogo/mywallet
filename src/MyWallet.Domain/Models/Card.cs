using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using MyWallet.Domain.Models.Core;
using static System.Text.Encoding;
using Convert = System.Convert;

namespace MyWallet.Domain.Models
{
    public class Card : Entity
    {
        public Card(Guid id, string number, DateTime dueDate, DateTime expirationDate, string cvv, decimal limit, bool isReleasingCreditAccepted)
            : base(id)
        {
            
            if (string.IsNullOrEmpty(number) || !Regex.IsMatch(number, "")) throw new Exception("Invalid credit card number.");

            Number = HashingCardNumber(number);
            DueDate = dueDate;
            ExpirationDate = expirationDate;

            if (string.IsNullOrEmpty(cvv) || !Regex.IsMatch(cvv, @"^[0-9]{3,4}$")) throw new Exception("the cvv code should have three or four digits.");
            Cvv = cvv;

            if (limit < 0) throw new Exception("The limit cannot be negative.");
            Limit = limit;

            _isReleasingCreditAccepted = isReleasingCreditAccepted;
        }
        /// <summary>
        /// used only to EF
        /// </summary>
        protected Card() { }
        /// <summary>
        /// Credit Card number
        /// </summary>
        public string Number { get; }
        /// <summary>
        /// Date limit to pay your bill
        /// </summary>
        public DateTime DueDate { get; }
        /// <summary>
        /// Date limit to use this card
        /// </summary>
        public DateTime ExpirationDate { get; }
        /// <summary>
        /// Credit card code validation (behind the card)
        /// </summary>
        public string Cvv { get; }
        /// <summary>
        /// max value avaliable for uses
        /// </summary>
        public decimal Limit { get; private set; }

        public ICollection<Acquisition> Purchases { get;} = new List<Acquisition>();

        public void RegisterPurchase(Acquisition purchase) => Purchases.Add(purchase);

        public void UpdateLimit(decimal newLimit)
        {
            if(newLimit < 0) throw new Exception("Limit cannot to be negative");
            Limit = newLimit;

        }

        /// <summary>
        /// indicates if this card accept to pay before the due date 
        /// </summary>
        private readonly bool _isReleasingCreditAccepted;

        /// <summary>
        /// returns if this card accept to pay before the due date 
        /// </summary>
        /// <returns>true if accepts</returns>
        public bool IsReleasingCreditAccepted()
        {
            return _isReleasingCreditAccepted;
        }

        /// <summary>
        /// Check if the full value of the purchase can be make using this card 
        /// </summary>
        /// <param name="purchase">value of the purchase (decimal)</param>
        /// <returns>returns true if purchase is less or equal to limit</returns>
        public bool IsPurchaseFitsLimit(decimal purchase) => purchase <= Limit;

        /// <summary>
        /// Compare the duedate between two cards 
        /// </summary>
        /// <param name="other">card to be compared</param>
        /// <returns>true if is the same due date</returns>
        public bool IsSameDueDate(Card other) => DueDate.ToString("d").Equals(other.DueDate.ToString("d"));

        /// <summary>
        /// Verify if this credit card is over due
        /// </summary>
        /// <returns>return true if over due</returns>
        public bool IsOverDue() => (DueDate.Day < DateTime.Today.Day && DueDate.Month <= DateTime.Today.Month);

        /// <summary>
        /// verifiy the status of ExpiredDate
        /// </summary>
        /// <returns>returns true if this card are expired</returns>
        public bool IsExpiredCard() => ExpirationDate.Date <= DateTime.Today;

        /// <summary>
        /// To avoid security leak of credit card number, this method encrypt the card number as hasg sha512 
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns>hashed string of credit card number</returns>
        private static string HashingCardNumber(string cardNumber)
        {
            using (var sha = SHA512.Create())
                return Convert.ToBase64String(sha.ComputeHash(UTF8.GetBytes(cardNumber)));
        }



    }
}