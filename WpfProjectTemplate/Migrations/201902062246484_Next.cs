namespace OutPatientApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Next : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Birthday", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accounts", "FirstName", c => c.String());
            AddColumn("dbo.Accounts", "LastName", c => c.String());
            AddColumn("dbo.Accounts", "Sex", c => c.String());
            AddColumn("dbo.Accounts", "Specialization", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "Specialization");
            DropColumn("dbo.Accounts", "Sex");
            DropColumn("dbo.Accounts", "LastName");
            DropColumn("dbo.Accounts", "FirstName");
            DropColumn("dbo.Accounts", "Birthday");
        }
    }
}
