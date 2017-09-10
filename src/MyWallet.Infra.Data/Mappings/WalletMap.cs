using System.Data.Entity.ModelConfiguration;

using MyWallet.Domain.Models;

namespace MyWallet.Infra.Data.Mappings
{
    public class WalletMap: EntityTypeConfiguration<Wallet>
    {
        public WalletMap()
        {
            HasKey(w => w.Id);
            Property(w => w.Id);

            Property(w => w.RealLimit);

            //HasRequired(w => w.Owner);

            HasMany(c => c.Cards);
        }

        

        
    }
}