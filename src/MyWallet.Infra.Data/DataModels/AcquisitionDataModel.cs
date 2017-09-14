using System;

namespace MyWallet.Infra.Data.DataModels
{
    public class AcquisitionDataModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public string CardNumber { get; set; }
        public virtual CardDataModel Card { get; set; }
    }
}