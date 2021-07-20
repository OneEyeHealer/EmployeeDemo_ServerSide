namespace EmployeeDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddress1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "City", c => c.String());
            AddColumn("dbo.Addresses", "State", c => c.String());
            AddColumn("dbo.Addresses", "Country", c => c.String());
            AddColumn("dbo.Addresses", "Pincode", c => c.Int(nullable: false));
            AddColumn("dbo.Addresses", "Landmark", c => c.String());
            AlterColumn("dbo.Employees", "Phone", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Phone", c => c.Int(nullable: false));
            DropColumn("dbo.Addresses", "Landmark");
            DropColumn("dbo.Addresses", "Pincode");
            DropColumn("dbo.Addresses", "Country");
            DropColumn("dbo.Addresses", "State");
            DropColumn("dbo.Addresses", "City");
        }
    }
}
