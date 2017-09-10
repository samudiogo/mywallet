using MyWallet.Domain.Models;
using MyWallet.Infra.Data.Context;
using MyWallet.Infra.Data.Contracts;
using MyWallet.Infra.Data.Repository.Shared;

namespace MyWallet.Infra.Data.Repository
{
    public class AcquisitionRepository : Repository<Acquisition>, IAcquisitionRepository
    {
        public AcquisitionRepository(MyWalletContext context) : base(context)
        {
        }
    }
}