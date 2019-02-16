using System;
using System.IO;
using AutoMapper;
using Caliburn.Micro;
using OutPatientApp.Models;

namespace OutPatientApp.ViewModels
{
    class ForCheckupItemViewModel : Screen
    {
        private Checkup Checkup { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Complaint { get; set; }
        public string Diagnosis { get; set; }
        public string CaseNumber { get; set; }

        public string PictureImage { get; set; }

        public Guid Id { get; set; }
    }
}
