using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OutPatientApp.Models;
using OutPatientApp.Persistence;

namespace OutPatientApp.ViewModels
{
    class AddCheckupViewModel : Screen
    {
        private string _complaint;
        private string _message;

        public string Complaint
        {
            get => _complaint;
            set => Set(ref _complaint, value);
        }

        public Guid PatientId { get; set; }

        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public void Save()
        {
            using (var db = new OPContext())
            {
                db.Checkups.Add(new Checkup
                {
                    Complaint = Complaint,
                    PatientId = PatientId
                });
                db.SaveChanges();

                Message = "Checkup successfully added";
            }
        }
    }
}
