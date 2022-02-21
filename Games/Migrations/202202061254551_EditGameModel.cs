namespace Games.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditGameModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Games", "UserId");
            AddForeignKey("dbo.Games", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Games", new[] { "UserId" });
            DropColumn("dbo.Games", "UserId");
        }
    }
}
