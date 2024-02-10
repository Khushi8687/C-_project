namespace C__project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "PositionID", c => c.Int(nullable: false));
            CreateIndex("dbo.Staffs", "PositionID");
            AddForeignKey("dbo.Staffs", "PositionID", "dbo.Positions", "PositionID", cascadeDelete: true);
            DropColumn("dbo.Staffs", "Position");
            DropColumn("dbo.Staffs", "StafffID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Staffs", "StafffID", c => c.Int(nullable: false));
            AddColumn("dbo.Staffs", "Position", c => c.String());
            DropForeignKey("dbo.Staffs", "PositionID", "dbo.Positions");
            DropIndex("dbo.Staffs", new[] { "PositionID" });
            DropColumn("dbo.Staffs", "PositionID");
        }
    }
}
