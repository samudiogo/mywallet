using System.Data.Entity.ModelConfiguration;
using MyWallet.Domain.Models;

namespace MyWallet.Infra.Data.Mappings
{
    public class AcquisitionMap: EntityTypeConfiguration<Acquisition>
    {
        

        public AcquisitionMap()
        {
            HasKey(a => a.Id);
            Property(a => a.Description)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            Property(a => a.Amount)
                .IsRequired();
        }

        
    }
}