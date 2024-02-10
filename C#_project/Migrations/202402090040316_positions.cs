namespace C__project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class positions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.positions",
                c => new
                    {
                        PositionID = c.Int(nullable: false, identity: true),
                        PositionName = c.String(),
                    })
                .PrimaryKey(t => t.PositionID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.positions");
        }
    }
}
