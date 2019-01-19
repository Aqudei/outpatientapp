using System;

namespace OutPatientApp.Models
{
    class Checkup : EntityBase
    {
        public Guid PatientId { get; set; }

        public string Complaint { get; set; }
        public string Diagnosis { get; set; }
        public string CaseNumber { get; set; }

        public DateTime DateOfCheckup => DateCreated;
    }
}
