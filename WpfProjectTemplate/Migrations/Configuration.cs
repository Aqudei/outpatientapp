using OutPatientApp.Models;

namespace OutPatientApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OutPatientApp.Persistence.OPContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OutPatientApp.Persistence.OPContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Accounts.Any(account => account.UserName == "clerk"))
            {
                var entity2 = new Account
                {
                    UserName = "clerk",
                    AccountType = AccountType.Clerk,
                    Birthday = new DateTime(1989,12,22),
                    FirstName = "Kiko",
                };
                entity2.SetPassword("clerk");
                context.Accounts.Add(entity2);
            }

            if (!context.Accounts.Any(account => account.UserName == "admin"))
            {
                var entity = new Account
                {
                    UserName = "admin",
                    AccountType = AccountType.ChiefNurse,
                    Birthday = new DateTime(1989, 12, 22),
                    FirstName = "Kiko-Admin",
                };
                entity.SetPassword("admin");
                context.Accounts.Add(entity);
            }

            if (!context.Accounts.Any(account => account.UserName == "doctor"))
            {
                var entity1 = new Account
                {
                    UserName = "doctor",
                    AccountType = AccountType.Doctor,
                    Birthday = new DateTime(1989, 12, 22),
                    FirstName = "Kiko-Doctor",
                };
                entity1.SetPassword("doctor");
                context.Accounts.Add(entity1);
            }

            context.SaveChanges();
        }
    }
}
