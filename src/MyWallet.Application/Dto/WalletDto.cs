using System;
using System.Collections.Generic;

namespace MyWallet.Application.Dto
{
    public class WalletDto
    {
        public Guid Id { get; set; }
        public UserDto Owner { get; set; }
        public IEnumerable<CardDto> Cards { get; set; }
        public decimal RealLimit { get; set; }

    }

    public class WalletRegistrationDto
    {
        public Guid OwnerId { get; set; }
    }
}