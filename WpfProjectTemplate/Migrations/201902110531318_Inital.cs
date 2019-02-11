namespace OutPatientApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(maxLength: 32),
                        Password = c.String(),
                        AccountType = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Sex = c.String(),
                        Specialization = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Checkups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PatientId = c.Guid(nullable: false),
                        DoctorId = c.Guid(nullable: false),
                        Complaint = c.String(),
                        Diagnosis = c.String(),
                        CaseNumber = c.String(),
                        IsDone = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastName = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        Birthday = c.DateTime(),
                        BirthPlace = c.String(),
                        CivilStatus = c.String(),
                        Sex = c.String(),
                        Address = c.String(),
                        NextKin = c.String(),
                        KinRelationship = c.String(),
                        IsInPatient = c.Boolean(nullable: false),
                        LastUpdated = c.DateTime(),
                        ContactNumber = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Checkups", "PatientId", "dbo.Patients");
            DropIndex("dbo.Checkups", new[] { "PatientId" });
            DropTable("dbo.Patients");
            DropTable("dbo.Checkups");
            DropTable("dbo.Accounts");
        }
    }
}
