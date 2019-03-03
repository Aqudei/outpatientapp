using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Caliburn.Micro;
using OutPatientApp.Models;
using OutPatientApp.Persistence;

namespace OutPatientApp.ViewModels
{
    sealed class PreviousPatientsViewModel : Screen
    {
        private readonly LoginViewModel _loginViewModel;
        public ICollectionView Items { get; set; }

        private BindableCollection<Checkup> _checkups = new BindableCollection<Checkup>();
        private string _searchText;

        public PreviousPatientsViewModel(LoginViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel;
            DisplayName = "Previous Patients";

            Items = CollectionViewSource.GetDefaultView(_checkups);
        }

        protected override void OnViewReady(object view)
        {
            using (var db = new OPContext())
            {
                _checkups.AddRange(db.Checkups.Include("Patient")
                    .Where(c => c.DoctorId == _loginViewModel.Account.Id).ToList());
            }
        }

        public string SearchText
        {
            get => _searchText;
            set => Set(ref _searchText, value);
        }

        public void DoFilter()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Items.Filter = o => true;
                return;
            }

            var searchText = SearchText.ToLower();
            Items.Filter = o =>
            {
                var checkup = o as Checkup;
                return checkup.CaseNumber.Contains(searchText) ||
                       checkup.Patient.FullName.ToLower().Contains(searchText) ||
                       checkup.Complaint.ToLower().Contains(searchText) ||
                       checkup.Diagnosis.ToLower().Contains(searchText);
            };
        }
    }
}
