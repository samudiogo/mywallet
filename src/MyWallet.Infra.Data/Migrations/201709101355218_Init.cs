namespace MyWallet.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Acquisitions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Card_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.Card_Id)
                .Index(t => t.Card_Id);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Limit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Wallet_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wallets", t => t.Wallet_Id)
                .Index(t => t.Wallet_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RealLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Owner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wallets", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Cards", "Wallet_Id", "dbo.Wallets");
            DropForeignKey("dbo.Acquisitions", "Card_Id", "dbo.Cards");
            DropIndex("dbo.Wallets", new[] { "Owner_Id" });
            DropIndex("dbo.Cards", new[] { "Wallet_Id" });
            DropIndex("dbo.Acquisitions", new[] { "Card_Id" });
            DropTable("dbo.Wallets");
            DropTable("dbo.Users");
            DropTable("dbo.Cards");
            DropTable("dbo.Acquisitions");
        }
    }
}
