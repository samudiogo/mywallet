using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Domain.Models;

namespace MyWallet.Infra.Data.Mappings
{
    public class CardMap: IEntityTypeConfiguration<Card>
    {

        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Number)
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(c => c.DueDate).IsRequired();
            builder.Property(c => c.ExpirationDate).IsRequired();
            builder.Property(c => c.Cvv).HasMaxLength(4);

            builder.Property(c => c.Limit).IsRequired()
                .IsConcurrencyToken();

            builder.Property(c => c.IsReleasingCreditAccepted).IsRequired();
        }
        
    }
}