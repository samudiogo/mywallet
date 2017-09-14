using System.Data.Entity;
using System.IO;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.DataModels;
using MyWallet.Infra.Data.Mappings;

namespace MyWallet.Infra.Data.Context
{
    public class MyWalletContext : DbContext
    {
        public MyWalletContext() : base(nameof(MyWalletContext)) { }
        public DbSet<UserDataModel> Users { get; set; }
        public DbSet<WalletDataModel> Wallets { get; set; }
        public DbSet<CardDataModel> Cards { get; set; }
        public DbSet<AcquisitionDataModel> Acquisitions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //TODO: ADD Mapping Class
            modelBuilder.Configurations.Add(new WalletMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new CardMap());
            modelBuilder.Configurations.Add(new AcquisitionMap());
            base.OnModelCreating(modelBuilder);
        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var cfg = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    optionsBuilder.UseSqlServer(cfg.GetConnectionString("MyWalletContext"));
        //    //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;");
        //}
    }

}