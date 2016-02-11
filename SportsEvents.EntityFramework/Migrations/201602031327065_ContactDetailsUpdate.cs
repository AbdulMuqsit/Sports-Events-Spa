namespace SportsEvents.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactDetailsUpdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "ContactDetails_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ContactDetails_Id", c => c.String());
        }
    }
}
