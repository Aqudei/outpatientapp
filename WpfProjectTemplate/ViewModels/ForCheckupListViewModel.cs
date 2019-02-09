using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AutoMapper;
using Caliburn.Micro;
using OutPatientApp.Models;
using OutPatientApp.Persistence;

namespace OutPatientApp.ViewModels
{
    sealed class ForCheckupListViewModel : Screen
    {
        private readonly IMapper _mapper;
        private readonly BindableCollection<ForCheckupItemViewModel> _checkups = new BindableCollection<ForCheckupItemViewModel>();
        private string _filterText;
        public ICollectionView Items { get; set; }

        public string FilterText
        {
            get => _filterText;
            set
            {
                Set(ref _filterText, value);
                NotifyOfPropertyChange(nameof(CanDoFilter));
            }
        }

        public bool CanDoFilter => !string.IsNullOrWhiteSpace(_filterText);

        public void DoFilter()
        {
            Items.Filter = o =>
            {
                var checkup = o as Checkup;

                return checkup != null && (checkup.CaseNumber.ToLower().Contains(FilterText.ToLower()) ||
                                           checkup.Patient.FirstName.ToLower().Contains(FilterText.ToLower()) ||
                                           checkup.Patient.LastName.ToLower().Contains(FilterText.ToLower()));

            };
        }

        public void ResetFilter()
        {
            Items.Filter = o => true;
        }

        public ForCheckupListViewModel(LoginViewModel loginViewModel, IMapper mapper)
        {
            _mapper = mapper;

            Items = CollectionViewSource.GetDefaultView(_checkups);

            DisplayName = "My Patients";

            using (var db = new OPContext())
            {
                _checkups.AddRange(db.Checkups.Include("Patient")
                    .Where(c => c.DoctorId == loginViewModel.Account.Id && !c.IsDone)
                    .ToList()
                    .Select(c => _mapper.Map<ForCheckupItemViewModel>(c)));
            }
        }


    }
}
