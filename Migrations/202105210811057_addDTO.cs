namespace EmployeeDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDTO : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberDTOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        Phone = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Employees", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employees", "Age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Age");
            DropColumn("dbo.Employees", "DateOfBirth");
            DropTable("dbo.MemberDTOes");
        }
    }
}
