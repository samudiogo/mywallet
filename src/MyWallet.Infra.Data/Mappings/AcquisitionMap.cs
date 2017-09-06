using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Domain.Models;

namespace MyWallet.Infra.Data.Mappings
{
    public class AcquisitionMap: IEntityTypeConfiguration<Acquisition>
    {
        

        public void Configure(EntityTypeBuilder<Acquisition> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Description)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Amount)
                .IsRequired();
        }

        
    }
}