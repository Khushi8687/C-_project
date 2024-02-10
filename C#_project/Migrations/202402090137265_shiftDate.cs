namespace C__project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shiftDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shifts", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Shifts", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shifts", "Description", c => c.String());
            DropColumn("dbo.Shifts", "Date");
        }
    }
}
