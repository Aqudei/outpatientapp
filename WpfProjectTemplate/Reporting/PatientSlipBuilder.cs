using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutPatientApp.Models;
using OutPatientApp.Persistence;
using Xceed.Words.NET;

namespace OutPatientApp.Reporting
{
    class PatientSlipBuilder
    {
        private readonly Guid _patientId;
        private readonly Guid _checkupId;
        private string _imageDirectory;

        public PatientSlipBuilder(Guid patientId, Guid checkupId)
        {
            _patientId = patientId;
            _checkupId = checkupId;
            _imageDirectory = Properties.Settings.Default.PhotoDirectory;
        }

        public void Build()
        {
            using (var db = new OPContext())
            {
                var patient = db.Patients.Find(_patientId);
                if (patient == null)
                    return;

                var checkup = db.Checkups.Find(_checkupId);
                if (checkup == null)
                    return;

                var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PatientSlip.docx");

                var doctor = FindDoctor(db, checkup.DoctorId);
                if (doctor == null)
                {
                    Debug.WriteLine("No doctor found. Cannot generate Patient Slip");
                    return;
                }

                using (var docx = DocX.Load(templatePath))
                {
                    docx.ReplaceText("{Date}", $"{DateTime.Now:D}");
                    docx.ReplaceText("{DoctorsName}", $"{doctor.FullName ?? ""}");
                    docx.ReplaceText("{CaseNumber}", $"{checkup.CaseNumber ?? ""}");
                    docx.ReplaceText("{PatientName}", $"{patient.FullName ?? ""}");
                    docx.ReplaceText("{Complaint}", $"{checkup.Complaint ?? ""}");

                    var fileName = Path.ChangeExtension(Path.GetTempFileName(), ".docx");
                    docx.SaveAs(fileName);
                    Process.Start(fileName);
                }
            }
        }

        private Account FindDoctor(OPContext context, Guid doctorId)
        {
            return context.Accounts.Find(doctorId);
        }
    }
}
