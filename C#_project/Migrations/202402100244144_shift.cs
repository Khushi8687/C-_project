namespace C__project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shift : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shifts", "StaffID", c => c.Int(nullable: false));
            CreateIndex("dbo.Shifts", "StaffID");
            AddForeignKey("dbo.Shifts", "StaffID", "dbo.Staffs", "StaffID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shifts", "StaffID", "dbo.Staffs");
            DropIndex("dbo.Shifts", new[] { "StaffID" });
            DropColumn("dbo.Shifts", "StaffID");
        }
    }
}
