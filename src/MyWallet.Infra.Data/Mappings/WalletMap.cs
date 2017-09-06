using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Domain.Models;

namespace MyWallet.Infra.Data.Mappings
{
    public class WalletMap: IEntityTypeConfiguration<Wallet>
    {
        

        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id);

            builder.Property(w => w.RealLimit);
            
            builder.HasOne(w => w.Owner)
                .WithOne()
                .IsRequired();

            builder.HasMany(c => c.Cards);


        }

        
    }
}