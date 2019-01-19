namespace OutPatientApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Checkups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PatientId = c.Guid(nullable: false),
                        Complaint = c.String(maxLength: 4000),
                        Diagnosis = c.String(maxLength: 4000),
                        CaseNumber = c.String(maxLength: 4000),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastName = c.String(maxLength: 4000),
                        FirstName = c.String(maxLength: 4000),
                        MiddleName = c.String(maxLength: 4000),
                        Birthday = c.DateTime(),
                        BirthPlace = c.String(maxLength: 4000),
                        CivilStatus = c.String(maxLength: 4000),
                        Sex = c.String(maxLength: 4000),
                        Address = c.String(maxLength: 4000),
                        NextKin = c.String(maxLength: 4000),
                        KinRelationship = c.String(maxLength: 4000),
                        IsInPatient = c.Boolean(nullable: false),
                        LastUpdated = c.DateTime(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Patients");
            DropTable("dbo.Checkups");
        }
    }
}
