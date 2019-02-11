namespace OutPatientApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Checkups", "DoctorId", c => c.Guid(nullable: false));
            AddColumn("dbo.Checkups", "IsDone", c => c.Boolean(nullable: false));
            AddColumn("dbo.Checkups", "SequenceNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Checkups", "SequenceNumber");
            DropColumn("dbo.Checkups", "IsDone");
            DropColumn("dbo.Checkups", "DoctorId");
        }
    }
}
