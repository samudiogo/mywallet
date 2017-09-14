using System.Data.Entity.ModelConfiguration;
using MyWallet.Infra.Data.DataModels;

namespace MyWallet.Infra.Data.Mappings
{
    public class CardMap: EntityTypeConfiguration<CardDataModel>
    {

        public CardMap()
        {
            ToTable("Cards");

            HasKey(c => c.CardNumber);

            HasRequired(c => c.Wallet)
                .WithMany(w=> w.Cards);

            Property(c => c.CardNumber)
                .HasMaxLength(20);

            Property(c => c.DueDate).IsRequired();

            Property(c => c.ExpirationDate).IsRequired();

            Property(c => c.Cvv).HasMaxLength(4).IsRequired();

            Property(c => c.Limit).IsRequired()
                .IsConcurrencyToken();

            Property(c => c.IsReleasedCreditAcepted).IsRequired();
        }
        
    }
}