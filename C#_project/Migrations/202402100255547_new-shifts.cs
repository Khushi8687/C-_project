namespace C__project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newshifts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shifts", "StaffId", c => c.Int(nullable: false));
            CreateIndex("dbo.Shifts", "StaffId");
            AddForeignKey("dbo.Shifts", "StaffId", "dbo.Staffs", "StaffID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shifts", "StaffId", "dbo.Staffs");
            DropIndex("dbo.Shifts", new[] { "StaffId" });
            DropColumn("dbo.Shifts", "StaffId");
        }
    }
}
