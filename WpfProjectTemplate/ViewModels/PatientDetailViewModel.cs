using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OutPatientApp.Models;

namespace OutPatientApp.ViewModels
{
    class PatientDetailViewModel : Screen
    {
        private string _lastName;
        private PatientStatus _status;
        public Guid Id { get; set; }
        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }

        public bool CanPrint => IoC.Get<LoginViewModel>().Account.AccountType == AccountType.ChiefNurse;

        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string BirthPlace { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }

        public string PictureImage { get; set; }

        public PatientStatus Status
        {
            get => _status;
            set => Set(ref _status, value);
        }
    }
}
