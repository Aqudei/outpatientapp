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
    class Builder
    {
        private readonly Guid _patientId;

        public Builder(Guid patientId)
        {
            _patientId = patientId;
        }

        public void Build()
        {
            using (var db = new OPContext())
            {
                var patient = db.Patients.Find(_patientId);
                if (patient == null)
                    return;
                var checkups = db.Checkups.Where(checkup => checkup.PatientId == _patientId).ToList();
                var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MedicalRecordsDocTemplate.docx");

                using (var docx = DocX.Load(templatePath))
                {
                    docx.ReplaceText("{FullName}", $"{patient.FullName}");
                    docx.ReplaceText("{DateOfReport}", $"{DateTime.Now:D}");
                    docx.ReplaceText("{Gender}", $"{patient.Sex}");
                    docx.ReplaceText("{Address}", $"{patient.Address}");
                    docx.ReplaceText("{ContactNumber}", $"{patient.ContactNumber}");

                    var table = docx.Tables.Find(t => t.TableDescription.Contains("MedicalHistory"));
                    var templateRow = table.Rows[1];
                    foreach (var checkup in checkups)
                    {
                        var doctor = FindDoctor(db, checkup.DoctorId);
                        var row = table.InsertRow(templateRow, 1);
                        row.ReplaceText("{DateOfCheckup}", $"{checkup.DateOfCheckup:g}");
                        row.ReplaceText("{Doctor}", doctor != null ? doctor.FullName : " ");
                        row.ReplaceText("{Complaint}", checkup.Complaint);
                        row.ReplaceText("{Diagnosis}", checkup.Diagnosis);
                    }
                    templateRow.Remove();
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
