namespace EmployeeDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HouseNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "HouseNo", c => c.String());
            DropColumn("dbo.Addresses", "HoureNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "HoureNo", c => c.String());
            DropColumn("dbo.Addresses", "HouseNo");
        }
    }
}
