using System;

namespace MyWallet.Application.Dto
{
    public class AcquisitionDto
    {
        public Guid Id { get; set; }
        public string Description { get;  set; }
        public decimal Amount { get; set; }
    }

    public class AcquisitionSaveOrUpdateDto
    {
        public Guid WalletId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

    }
}