namespace C__project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StaffShifts", "Staff_StaffID", "dbo.Staffs");
            DropForeignKey("dbo.StaffShifts", "Shift_ShiftID", "dbo.Shifts");
            DropIndex("dbo.StaffShifts", new[] { "Staff_StaffID" });
            DropIndex("dbo.StaffShifts", new[] { "Shift_ShiftID" });
            AddColumn("dbo.Shifts", "StaffID", c => c.Int(nullable: false));
            AddColumn("dbo.Shifts", "Staff_StaffID", c => c.Int());
            AddColumn("dbo.Staffs", "Shift_ShiftID", c => c.Int());
            CreateIndex("dbo.Shifts", "StaffID");
            CreateIndex("dbo.Shifts", "Staff_StaffID");
            CreateIndex("dbo.Staffs", "Shift_ShiftID");
            AddForeignKey("dbo.Shifts", "Staff_StaffID", "dbo.Staffs", "StaffID");
            AddForeignKey("dbo.Shifts", "StaffID", "dbo.Staffs", "StaffID", cascadeDelete: true);
            AddForeignKey("dbo.Staffs", "Shift_ShiftID", "dbo.Shifts", "ShiftID");
            DropTable("dbo.StaffShifts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StaffShifts",
                c => new
                    {
                        Staff_StaffID = c.Int(nullable: false),
                        Shift_ShiftID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Staff_StaffID, t.Shift_ShiftID });
            
            DropForeignKey("dbo.Staffs", "Shift_ShiftID", "dbo.Shifts");
            DropForeignKey("dbo.Shifts", "StaffID", "dbo.Staffs");
            DropForeignKey("dbo.Shifts", "Staff_StaffID", "dbo.Staffs");
            DropIndex("dbo.Staffs", new[] { "Shift_ShiftID" });
            DropIndex("dbo.Shifts", new[] { "Staff_StaffID" });
            DropIndex("dbo.Shifts", new[] { "StaffID" });
            DropColumn("dbo.Staffs", "Shift_ShiftID");
            DropColumn("dbo.Shifts", "Staff_StaffID");
            DropColumn("dbo.Shifts", "StaffID");
            CreateIndex("dbo.StaffShifts", "Shift_ShiftID");
            CreateIndex("dbo.StaffShifts", "Staff_StaffID");
            AddForeignKey("dbo.StaffShifts", "Shift_ShiftID", "dbo.Shifts", "ShiftID", cascadeDelete: true);
            AddForeignKey("dbo.StaffShifts", "Staff_StaffID", "dbo.Staffs", "StaffID", cascadeDelete: true);
        }
    }
}
