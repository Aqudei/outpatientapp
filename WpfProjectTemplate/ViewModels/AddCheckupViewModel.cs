using System;
using System.Linq;
using Caliburn.Micro;
using OutPatientApp.Models;
using OutPatientApp.Persistence;
using OutPatientApp.Services;

namespace OutPatientApp.ViewModels
{
    internal sealed class AddCheckupViewModel : Screen
    {
        private readonly CaseNumberGen _caseNumberGen;

        private CaseNumber _caseNumber;
        private string _complaint;
        private string _message;
        private Account _selectedDoctor;

        public AddCheckupViewModel(CaseNumberGen caseNumberGen)
        {
            _caseNumberGen = caseNumberGen;

            DisplayName = "Schedule New Checkup";
        }

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

        public CaseNumber CaseNumber
        {
            get => _caseNumber;
            set => Set(ref _caseNumber, value);
        }

        public void Save()
        {
            using (var db = new OPContext())
            {
                db.Checkups.Add(new Checkup
                {
                    CaseNumber = CaseNumber.ToString(),
                    Complaint = Complaint,
                    PatientId = PatientId,
                    DoctorId = SelectedDoctor != null ? SelectedDoctor.Id : Guid.Empty
                });

                db.SaveChanges();

                Message = "Checkup successfully added";
            }
        }

        protected override void OnViewReady(object view)
        {
            CaseNumber = _caseNumberGen.Get();
            Doctors.Clear();
            using (var db = new OPContext())
            {
                Doctors.AddRange(db.Accounts.Where(a => a.AccountType == AccountType.Doctor).ToList());
            }
        }
    }
}