namespace C__project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Shifts", "Staff_StaffID", "dbo.Staffs");
            DropIndex("dbo.Shifts", new[] { "Staff_StaffID" });
            CreateTable(
                "dbo.StaffShifts",
                c => new
                    {
                        Staff_StaffID = c.Int(nullable: false),
                        Shift_ShiftID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Staff_StaffID, t.Shift_ShiftID })
                .ForeignKey("dbo.Staffs", t => t.Staff_StaffID, cascadeDelete: true)
                .ForeignKey("dbo.Shifts", t => t.Shift_ShiftID, cascadeDelete: true)
                .Index(t => t.Staff_StaffID)
                .Index(t => t.Shift_ShiftID);
            
            DropColumn("dbo.Shifts", "Staff_StaffID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shifts", "Staff_StaffID", c => c.Int());
            DropForeignKey("dbo.StaffShifts", "Shift_ShiftID", "dbo.Shifts");
            DropForeignKey("dbo.StaffShifts", "Staff_StaffID", "dbo.Staffs");
            DropIndex("dbo.StaffShifts", new[] { "Shift_ShiftID" });
            DropIndex("dbo.StaffShifts", new[] { "Staff_StaffID" });
            DropTable("dbo.StaffShifts");
            CreateIndex("dbo.Shifts", "Staff_StaffID");
            AddForeignKey("dbo.Shifts", "Staff_StaffID", "dbo.Staffs", "StaffID");
        }
    }
}
