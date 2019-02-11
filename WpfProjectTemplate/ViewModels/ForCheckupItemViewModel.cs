using AutoMapper;
using Caliburn.Micro;
using OutPatientApp.Models;

namespace OutPatientApp.ViewModels
{
    class ForCheckupItemViewModel : Screen
    {
        private  Checkup Checkup { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Complaints { get; set; }
        public string CaseNumber { get; set; }
    }
}
