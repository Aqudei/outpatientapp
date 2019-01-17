using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutPatientApp.Models
{
    class Visit : EntityBase
    {
        public DateTime VisitDate { get; set; }

        public string Complaint { get; set; }
        public string Diagnosis { get; set; }

        public string PatientType { get; set; }

        public Visit()
        {
            VisitDate = DateTime.Now;
        }
    }
}
