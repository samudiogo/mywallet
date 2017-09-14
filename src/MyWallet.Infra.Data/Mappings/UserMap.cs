using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.DataModels;

namespace MyWallet.Infra.Data.Mappings
{
    public class UserMap : EntityTypeConfiguration<UserDataModel>
    {
        public UserMap()
        {
            ToTable("Users");

            HasKey(u => u.Id);

            Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Email).IsRequired();

            Property(u => u.Name).IsRequired();

            Property(u => u.Password).IsRequired();

            Property(u => u.Token).IsOptional();
            
        }
    }
}