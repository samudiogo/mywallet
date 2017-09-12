namespace MyWallet.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTokenOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Token", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Token", c => c.String(nullable: false));
        }
    }
}
