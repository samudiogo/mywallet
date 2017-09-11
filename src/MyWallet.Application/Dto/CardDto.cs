using System;
using System.Collections.Generic;

namespace MyWallet.Application.Dto
{
    public class CardDto
    {
        public Guid Id { get; set; }
        public string NameInCard { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Limit { get; set; }
        public bool IsReleasedCreditAcepted { get; set; }
        public IEnumerable<AcquisitionDto> Purchases { get; set; }
    }
}