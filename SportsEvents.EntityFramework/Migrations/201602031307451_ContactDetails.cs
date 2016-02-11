namespace SportsEvents.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Link", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address_LineOne", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Address_LineTwo", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address_CityId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Address_CityName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address_CountryName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address_Zip", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Address_State", c => c.String());
            AddColumn("dbo.AspNetUsers", "OrganiztionName", c => c.String());
            AddColumn("dbo.AspNetUsers", "OrganizationDecription", c => c.String());
            AddColumn("dbo.AspNetUsers", "OrganaiztionLogo", c => c.String());
            AddColumn("dbo.AspNetUsers", "ContactDetails_Id", c => c.String());
            AddColumn("dbo.AspNetUsers", "ContactDetails_FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "ContactDetails_LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "ContactDetails_Email", c => c.String());
            AddColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_LineOne", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_LineTwo", c => c.String());
            AddColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_CityId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_CityName", c => c.String());
            AddColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_CountryName", c => c.String());
            AddColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_Zip", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_State", c => c.String());
            AddColumn("dbo.AspNetUsers", "ContactDetails_Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ContactDetails_Phone");
            DropColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_State");
            DropColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_Zip");
            DropColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_CountryName");
            DropColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_CityName");
            DropColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_CityId");
            DropColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_LineTwo");
            DropColumn("dbo.AspNetUsers", "ContactDetails_BillingAddress_LineOne");
            DropColumn("dbo.AspNetUsers", "ContactDetails_Email");
            DropColumn("dbo.AspNetUsers", "ContactDetails_LastName");
            DropColumn("dbo.AspNetUsers", "ContactDetails_FirstName");
            DropColumn("dbo.AspNetUsers", "ContactDetails_Id");
            DropColumn("dbo.AspNetUsers", "OrganaiztionLogo");
            DropColumn("dbo.AspNetUsers", "OrganizationDecription");
            DropColumn("dbo.AspNetUsers", "OrganiztionName");
            DropColumn("dbo.AspNetUsers", "Address_State");
            DropColumn("dbo.AspNetUsers", "Address_Zip");
            DropColumn("dbo.AspNetUsers", "Address_CountryName");
            DropColumn("dbo.AspNetUsers", "Address_CityName");
            DropColumn("dbo.AspNetUsers", "Address_CityId");
            DropColumn("dbo.AspNetUsers", "Address_LineTwo");
            DropColumn("dbo.AspNetUsers", "Address_LineOne");
            DropColumn("dbo.AspNetUsers", "Link");
        }
    }
}
