namespace SportsEvents.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pics : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pictures", "Event_Id", "dbo.Events");
            DropIndex("dbo.Pictures", new[] { "Event_Id" });
            AlterColumn("dbo.Pictures", "Event_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Pictures", "Event_Id");
            AddForeignKey("dbo.Pictures", "Event_Id", "dbo.Events", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "Event_Id", "dbo.Events");
            DropIndex("dbo.Pictures", new[] { "Event_Id" });
            AlterColumn("dbo.Pictures", "Event_Id", c => c.Int());
            CreateIndex("dbo.Pictures", "Event_Id");
            AddForeignKey("dbo.Pictures", "Event_Id", "dbo.Events", "Id");
        }
    }
}
