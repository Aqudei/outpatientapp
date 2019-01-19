using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace OutPatientApp.ViewModels
{
    class PatientDetailViewModel : Screen
    {
        private string _lastName;
        private bool _isInPatient;
        public Guid Id { get; set; }
        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }

        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string BirthPlace { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }

        public bool IsInPatient
        {
            get => _isInPatient;
            set => Set(ref _isInPatient, value);
        }
    }
}
