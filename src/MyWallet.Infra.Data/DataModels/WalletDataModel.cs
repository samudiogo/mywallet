using System;
using System.Collections.Generic;

namespace MyWallet.Infra.Data.DataModels
{
    public class WalletDataModel
    {
        public Guid Id { get; set; }
        public UserDataModel Owner { get; set; }
        public decimal RealLimit { get; set; }
        public ICollection<CardDataModel> Cards { get; set; }
    }
}