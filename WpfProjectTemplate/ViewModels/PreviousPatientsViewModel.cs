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
    }
}
