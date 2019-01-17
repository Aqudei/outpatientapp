using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using OutPatientApp.Models;
using OutPatientApp.Persistence;

namespace OutPatientApp.ViewModels
{
    class PatientRegistrationViewModel : Screen
    {
        private readonly IMapper _mapper;
        private readonly IDialogCoordinator _dialogCoordinator;
        private string _lastName;
        private string _firstName;
        private string _middleName;
        private DateTime _birthday;
        private string _birthPlace;
        private string _civilStatus;
        private string _sex;
        private string _address;
        private string _nextKin;
        private string _kinRelationship;
        public override string DisplayName { get; set; } = "Out-Patient Registration";

        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value);
        }

        public string MiddleName
        {
            get => _middleName;
            set => Set(ref _middleName, value);
        }

        public DateTime Birthday
        {
            get => _birthday;
            set => Set(ref _birthday, value);
        }

        public string BirthPlace
        {
            get => _birthPlace;
            set => Set(ref _birthPlace, value);
        }

        public string CivilStatus
        {
            get => _civilStatus;
            set => Set(ref _civilStatus, value);
        }

        public List<string> CivilStatuses { get; set; } = new List<string>();

        public string Sex
        {
            get => _sex;
            set => Set(ref _sex, value);
        }

        public string Address
        {
            get => _address;
            set => Set(ref _address, value);
        }

        public List<string> Sexes { get; set; } = new List<string>();

        public string NextKin
        {
            get => _nextKin;
            set => Set(ref _nextKin, value);
        }

        public string KinRelationship
        {
            get => _kinRelationship;
            set => Set(ref _kinRelationship, value);
        }

        public PatientRegistrationViewModel(IMapper mapper, IDialogCoordinator dialogCoordinator)
        {
            _mapper = mapper;
            _dialogCoordinator = dialogCoordinator;

            Sexes.Add("Male");
            Sexes.Add("Female");

            CivilStatuses.Add("Single");
            CivilStatuses.Add("Married");
            CivilStatuses.Add("Divorced");
            CivilStatuses.Add("Separated");
            CivilStatuses.Add("Widowed");

            Birthday = DateTime.Now;
        }

        public void Save()
        {
            var patient = _mapper.Map<Patient>(this);
            using (var db = new OPContext())
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                _dialogCoordinator.ShowMessageAsync(this, "Success", "Patient's record was added");
            }
        }
    }
}
