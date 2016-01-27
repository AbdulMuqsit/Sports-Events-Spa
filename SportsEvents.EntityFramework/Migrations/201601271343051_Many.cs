namespace SportsEvents.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Many : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.AspNetUsers", "Event_Id1", "dbo.Events");
            DropForeignKey("dbo.AspNetUsers", "Event_Id2", "dbo.Events");
            DropForeignKey("dbo.AspNetUsers", "Event_Id3", "dbo.Events");
            DropIndex("dbo.Events", new[] { "User_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Event_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Event_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "Event_Id2" });
            DropIndex("dbo.AspNetUsers", new[] { "Event_Id3" });
            CreateTable(
                "dbo.Bookmarks_User_Event",
                c => new
                    {
                        BookmarkedEventId = c.Int(nullable: false),
                        BookMarkerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BookmarkedEventId, t.BookMarkerId })
                .ForeignKey("dbo.Events", t => t.BookmarkedEventId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.BookMarkerId, cascadeDelete: true)
                .Index(t => t.BookmarkedEventId)
                .Index(t => t.BookMarkerId);
            
            CreateTable(
                "dbo.ClickedEvents_User_Events",
                c => new
                    {
                        ClickerUserId = c.Int(nullable: false),
                        ClickedEventId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ClickerUserId, t.ClickedEventId })
                .ForeignKey("dbo.Events", t => t.ClickerUserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ClickedEventId, cascadeDelete: true)
                .Index(t => t.ClickerUserId)
                .Index(t => t.ClickedEventId);
            
            CreateTable(
                "dbo.Registrations_User_Event",
                c => new
                    {
                        RegisteredUserId = c.Int(nullable: false),
                        RegisteredEventId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RegisteredUserId, t.RegisteredEventId })
                .ForeignKey("dbo.Events", t => t.RegisteredUserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.RegisteredEventId, cascadeDelete: true)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.RegisteredEventId);
            
            CreateTable(
                "dbo.RegistrationRequests_User_Event",
                c => new
                    {
                        RegisterRequestVisitorId = c.Int(nullable: false),
                        RegisterationRequestId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RegisterRequestVisitorId, t.RegisterationRequestId })
                .ForeignKey("dbo.Events", t => t.RegisterRequestVisitorId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.RegisterationRequestId, cascadeDelete: true)
                .Index(t => t.RegisterRequestVisitorId)
                .Index(t => t.RegisterationRequestId);
            
            DropColumn("dbo.Events", "User_Id");
            DropColumn("dbo.AspNetUsers", "Event_Id");
            DropColumn("dbo.AspNetUsers", "Event_Id1");
            DropColumn("dbo.AspNetUsers", "Event_Id2");
            DropColumn("dbo.AspNetUsers", "Event_Id3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Event_Id3", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Event_Id2", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Event_Id1", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Event_Id", c => c.Int());
            AddColumn("dbo.Events", "User_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.RegistrationRequests_User_Event", "RegisterationRequestId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RegistrationRequests_User_Event", "RegisterRequestVisitorId", "dbo.Events");
            DropForeignKey("dbo.Registrations_User_Event", "RegisteredEventId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Registrations_User_Event", "RegisteredUserId", "dbo.Events");
            DropForeignKey("dbo.ClickedEvents_User_Events", "ClickedEventId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClickedEvents_User_Events", "ClickerUserId", "dbo.Events");
            DropForeignKey("dbo.Bookmarks_User_Event", "BookMarkerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookmarks_User_Event", "BookmarkedEventId", "dbo.Events");
            DropIndex("dbo.RegistrationRequests_User_Event", new[] { "RegisterationRequestId" });
            DropIndex("dbo.RegistrationRequests_User_Event", new[] { "RegisterRequestVisitorId" });
            DropIndex("dbo.Registrations_User_Event", new[] { "RegisteredEventId" });
            DropIndex("dbo.Registrations_User_Event", new[] { "RegisteredUserId" });
            DropIndex("dbo.ClickedEvents_User_Events", new[] { "ClickedEventId" });
            DropIndex("dbo.ClickedEvents_User_Events", new[] { "ClickerUserId" });
            DropIndex("dbo.Bookmarks_User_Event", new[] { "BookMarkerId" });
            DropIndex("dbo.Bookmarks_User_Event", new[] { "BookmarkedEventId" });
            DropTable("dbo.RegistrationRequests_User_Event");
            DropTable("dbo.Registrations_User_Event");
            DropTable("dbo.ClickedEvents_User_Events");
            DropTable("dbo.Bookmarks_User_Event");
            CreateIndex("dbo.AspNetUsers", "Event_Id3");
            CreateIndex("dbo.AspNetUsers", "Event_Id2");
            CreateIndex("dbo.AspNetUsers", "Event_Id1");
            CreateIndex("dbo.AspNetUsers", "Event_Id");
            CreateIndex("dbo.Events", "User_Id");
            AddForeignKey("dbo.AspNetUsers", "Event_Id3", "dbo.Events", "Id");
            AddForeignKey("dbo.AspNetUsers", "Event_Id2", "dbo.Events", "Id");
            AddForeignKey("dbo.AspNetUsers", "Event_Id1", "dbo.Events", "Id");
            AddForeignKey("dbo.AspNetUsers", "Event_Id", "dbo.Events", "Id");
            AddForeignKey("dbo.Events", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
