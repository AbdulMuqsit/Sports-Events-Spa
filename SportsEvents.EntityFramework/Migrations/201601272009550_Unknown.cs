namespace SportsEvents.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unknown : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Address_State", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Address_State", c => c.String(nullable: false));
        }
    }
}
