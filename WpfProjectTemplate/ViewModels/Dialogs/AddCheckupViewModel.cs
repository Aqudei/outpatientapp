using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Caliburn.Micro;
using OutPatientApp.Models;
using OutPatientApp.Persistence;

namespace OutPatientApp.ViewModels.Dialogs
{
    internal sealed class AddCheckupViewModel : Screen
    {
        private readonly BindableCollection<Account> _doctors = new BindableCollection<Account>();
        private string _complaint;
        private string _message;

        public AddCheckupViewModel()
        {
            DisplayName = "Add Checkup";
            Doctors = CollectionViewSource.GetDefaultView(_doctors);
        }

        public ICollectionView Doctors { get; set; }

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


        public Account Doctor { get; set; }

        public void Save()
        {
            using (var db = new OPContext())
            {
                db.Checkups.Add(new Checkup
                {
                    Complaint = Complaint,
                    PatientId = PatientId,
                    DoctorId = Doctor != null ? Doctor.Id : Guid.Empty
                });

                db.SaveChanges();
                Message = "Checkup successfully added";
            }
        }

        protected override void OnViewReady(object view)
        {
            using (var db = new OPContext())
            {
                _doctors.Clear();
                var doctors = db.Accounts.Where(a => a.AccountType == AccountType.Doctor).ToList();
                _doctors.AddRange(doctors);
            }
        }
    }
}