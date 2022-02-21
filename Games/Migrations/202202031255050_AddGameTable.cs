namespace Games.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGameTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameTitle = c.String(),
                        GameContent = c.String(),
                        GameAppraisal = c.String(),
                        GameImage = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Games", new[] { "CategoryId" });
            DropTable("dbo.Games");
        }
    }
}
