using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.Mappings;

namespace MyWallet.Infra.Data.Context
{
    public class MyWalletContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Acquisition> Acquisitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: ADD Mapping Class
            modelBuilder.ApplyConfiguration(new WalletMap());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var cfg = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //optionsBuilder.UseSqlServer(cfg.GetConnectionString("MyWalletContext"));
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;");
        }
    }
    
}