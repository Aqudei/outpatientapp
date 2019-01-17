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

        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }
    }
}
