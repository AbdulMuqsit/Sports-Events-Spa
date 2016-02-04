namespace SportsEvents.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Address_LineOne", c => c.String());
            AlterColumn("dbo.Events", "Address_CityId", c => c.Int());
            AlterColumn("dbo.Events", "Address_Zip", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Address_LineOne", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Address_CityId", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Address_Zip", c => c.String());
            AlterColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_LineOne", c => c.String());
            AlterColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_CityId", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_Zip", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_Zip", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_CityId", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_LineOne", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Address_Zip", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Address_CityId", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Address_LineOne", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Address_Zip", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Address_CityId", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Address_LineOne", c => c.String(nullable: false));
        }
    }
}
