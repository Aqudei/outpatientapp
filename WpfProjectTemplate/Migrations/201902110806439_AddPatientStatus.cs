namespace OutPatientApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPatientStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "Status");
        }
    }
}
