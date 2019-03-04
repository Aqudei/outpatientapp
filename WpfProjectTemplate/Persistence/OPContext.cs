using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutPatientApp.Models;

namespace OutPatientApp.Persistence
{
    class OPContext : DbContext
    {

        public OPContext() : base("OPContext")
        { }

        public DbSet<Checkup> Checkups { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CaseNumber> CaseNumbers { get; set; }
    }
}
