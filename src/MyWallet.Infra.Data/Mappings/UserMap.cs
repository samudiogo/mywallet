using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyWallet.Domain.Models;

namespace MyWallet.Infra.Data.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(u => u.Id);
            Property(u => u.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Email).IsRequired();
            Property(u => u.Name).IsRequired();
            Property(u => u.Password).IsRequired();
            Property(u => u.Token).IsOptional();
            
        }
    }
}