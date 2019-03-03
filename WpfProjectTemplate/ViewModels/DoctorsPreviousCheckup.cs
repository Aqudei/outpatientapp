using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutPatientApp.Models;

namespace OutPatientApp.ViewModels
{
    class DoctorsPreviousCheckup
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }

        public string Complaint { get; set; }
        public string Diagnosis { get; set; }
        public string CaseNumber { get; set; }

        public Patient Patient { get; set; }
        public DateTime DateOfCheckup;
        public bool IsDone { get; set; }
    }
}
