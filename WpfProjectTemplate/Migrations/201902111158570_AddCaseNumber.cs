namespace OutPatientApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCaseNumber : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaseNumbers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ForDate = c.DateTime(nullable: false),
                        Number = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CaseNumbers");
        }
    }
}
