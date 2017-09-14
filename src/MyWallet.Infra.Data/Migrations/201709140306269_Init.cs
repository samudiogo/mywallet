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
                        Description = c.String(nullable: false, maxLength: 100),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CardNumber = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.CardNumber, cascadeDelete: true)
                .Index(t => t.CardNumber);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardNumber = c.String(nullable: false, maxLength: 20),
                        NameInCard = c.String(),
                        Cvv = c.String(nullable: false, maxLength: 4),
                        DueDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        Limit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsReleasedCreditAcepted = c.Boolean(nullable: false),
                        WalletId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CardNumber)
                .ForeignKey("dbo.Wallets", t => t.WalletId, cascadeDelete: true)
                .Index(t => t.WalletId);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RealLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Owner_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id, cascadeDelete: true)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Token = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Acquisitions", "CardNumber", "dbo.Cards");
            DropForeignKey("dbo.Cards", "WalletId", "dbo.Wallets");
            DropForeignKey("dbo.Wallets", "Owner_Id", "dbo.Users");
            DropIndex("dbo.Wallets", new[] { "Owner_Id" });
            DropIndex("dbo.Cards", new[] { "WalletId" });
            DropIndex("dbo.Acquisitions", new[] { "CardNumber" });
            DropTable("dbo.Users");
            DropTable("dbo.Wallets");
            DropTable("dbo.Cards");
            DropTable("dbo.Acquisitions");
        }
    }
}
