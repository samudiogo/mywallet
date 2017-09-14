using System.Data.Entity.ModelConfiguration;

using MyWallet.Infra.Data.DataModels;

namespace MyWallet.Infra.Data.Mappings
{
    public class WalletMap: EntityTypeConfiguration<WalletDataModel>
    {
        public WalletMap()
        {
            ToTable("Wallets");

            HasKey(w => w.Id);
            Property(w => w.Id);

            Property(w => w.RealLimit);

            HasRequired(w => w.Owner);

            HasMany(c => c.Cards);
        }

        

        
    }
}