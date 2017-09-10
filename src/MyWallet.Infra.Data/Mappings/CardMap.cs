using System.Data.Entity.ModelConfiguration;
using MyWallet.Domain.Models;

namespace MyWallet.Infra.Data.Mappings
{
    public class CardMap: EntityTypeConfiguration<Card>
    {

        public CardMap()
        {
            HasKey(c => c.Id);

            Property(c => c.Number)
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20)
                .IsRequired();

            Property(c => c.DueDate).IsRequired();
            Property(c => c.ExpirationDate).IsRequired();
            Property(c => c.Cvv).HasMaxLength(4);

            Property(c => c.Limit).IsRequired()
                .IsConcurrencyToken();

            Property(c => c.IsReleasingCreditAccepted).IsRequired();
        }
        
    }
}