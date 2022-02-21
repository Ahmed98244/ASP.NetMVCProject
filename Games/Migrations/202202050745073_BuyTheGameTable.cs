namespace Games.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuyTheGameTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyTheGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Messsage = c.String(),
                        Date = c.DateTime(nullable: false),
                        GameId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.GameId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuyTheGames", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BuyTheGames", "GameId", "dbo.Games");
            DropIndex("dbo.BuyTheGames", new[] { "UserId" });
            DropIndex("dbo.BuyTheGames", new[] { "GameId" });
            DropTable("dbo.BuyTheGames");
        }
    }
}
