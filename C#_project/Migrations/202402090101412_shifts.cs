namespace C__project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shifts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        ShiftID = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ShiftID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Shifts");
        }
    }
}
