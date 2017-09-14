using System.Data.Entity.ModelConfiguration;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.DataModels;

namespace MyWallet.Infra.Data.Mappings
{
    public class AcquisitionMap : EntityTypeConfiguration<AcquisitionDataModel>
    {


        public AcquisitionMap()
        {
            ToTable("Acquisitions");
            HasKey(a => a.Id);
            Property(a => a.Description)
                .HasMaxLength(100)
                .IsRequired();

            Property(a => a.Amount)
                .IsRequired();

            HasRequired(a => a.Card)
                .WithMany(c => c.Purchases)
                .HasForeignKey(a=> a.CardNumber);
        }


    }
}