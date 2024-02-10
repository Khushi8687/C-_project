namespace C__project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class three : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Shifts", "StaffId", "dbo.Staffs");
            DropIndex("dbo.Shifts", new[] { "StaffId" });
            RenameColumn(table: "dbo.Shifts", name: "StaffId", newName: "Staff_StaffID");
            AlterColumn("dbo.Shifts", "Staff_StaffID", c => c.Int());
            CreateIndex("dbo.Shifts", "Staff_StaffID");
            AddForeignKey("dbo.Shifts", "Staff_StaffID", "dbo.Staffs", "StaffID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shifts", "Staff_StaffID", "dbo.Staffs");
            DropIndex("dbo.Shifts", new[] { "Staff_StaffID" });
            AlterColumn("dbo.Shifts", "Staff_StaffID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Shifts", name: "Staff_StaffID", newName: "StaffId");
            CreateIndex("dbo.Shifts", "StaffId");
            AddForeignKey("dbo.Shifts", "StaffId", "dbo.Staffs", "StaffID", cascadeDelete: true);
        }
    }
}
