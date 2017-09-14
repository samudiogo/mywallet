using System;
using System.Collections.Generic;

namespace MyWallet.Infra.Data.DataModels
{
    public class CardDataModel
    {
        public string CardNumber { get; set; }
        public string NameInCard { get; set; }
        public string Cvv { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Limit { get; set; }
        public bool IsReleasedCreditAcepted { get; set; }
        public Guid WalletId { get; set; }
        public virtual WalletDataModel Wallet { get; set; }

        public virtual ICollection<AcquisitionDataModel> Purchases { get; set; }
    }
}