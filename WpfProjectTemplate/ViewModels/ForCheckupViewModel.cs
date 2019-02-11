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
    sealed class ForCheckupViewModel : Screen
    {
        private readonly SessionData _sessionData;
        private readonly BindableCollection<Checkup> _checkups = new BindableCollection<Checkup>();
        public ICollectionView Items { get; set; }
        public Checkup SelectedCheckup { get; set; }

        public ForCheckupViewModel(SessionData sessionData)
        {
            _sessionData = sessionData;
            Items = CollectionViewSource.GetDefaultView(_checkups);
            DisplayName = "For Checkup";
        }

        protected override void OnViewReady(object view)
        {
            using (var db = new OPContext())
            {
                _checkups.Clear();
                var checkups = db.Checkups.Where(c => c.DoctorId == _sessionData.Account.Id)
                    .ToList();
                _checkups.AddRange(checkups);
            }
        }
    }
}
