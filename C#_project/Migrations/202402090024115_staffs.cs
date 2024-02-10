namespace C__project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class staffs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.staffs",
                c => new
                    {
                        StaffID = c.Int(nullable: false, identity: true),
                        StaffName = c.String(),
                        Email = c.String(),
                        Contact = c.Int(nullable: false),
                        Position = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.StaffID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.staffs");
        }
    }
}
