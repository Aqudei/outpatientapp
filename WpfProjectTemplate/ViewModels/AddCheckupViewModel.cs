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
        private Account _selectedDoctor;

        public string Complaint
        {
            get => _complaint;
            set
            {
                Set(ref _complaint, value);
                NotifyOfPropertyChange(nameof(CanSave));
            }
        }

        public Guid PatientId { get; set; }

        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public bool CanSave => SelectedDoctor != null && !string.IsNullOrWhiteSpace(Complaint);

        public void Save()
        {
            using (var db = new OPContext())
            {
                db.Checkups.Add(new Checkup
                {
                    Complaint = Complaint,
                    PatientId = PatientId,
                    DoctorId = SelectedDoctor != null ? SelectedDoctor.Id : Guid.Empty
                });

                db.SaveChanges();

                Message = "Checkup successfully added";
            }
        }

        public BindableCollection<Account> Doctors { get; set; } = new BindableCollection<Account>();

        public Account SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                Set(ref _selectedDoctor, value);
                NotifyOfPropertyChange(nameof(CanSave));
            }
        }

        protected override void OnViewReady(object view)
        {
            Doctors.Clear();
            using (var db = new OPContext())
            {
                Doctors.AddRange(db.Accounts.Where(a => a.AccountType == AccountType.Doctor).ToList());
            }
        }
    }
}
