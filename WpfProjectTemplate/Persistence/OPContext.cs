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
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Patient> Patients { get; set; }
    }
}
