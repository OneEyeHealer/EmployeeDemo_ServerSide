namespace EmployeeDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataCheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "AddressType", c => c.Boolean(nullable: false));
            AddColumn("dbo.Addresses", "Street", c => c.String());
            AddColumn("dbo.Employees", "Department", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Department");
            DropColumn("dbo.Addresses", "Street");
            DropColumn("dbo.Addresses", "AddressType");
        }
    }
}
