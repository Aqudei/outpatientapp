using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using AutoMapper;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using OutPatientApp.Models;
using OutPatientApp.Persistence;

namespace OutPatientApp.ViewModels
{
    sealed class ForCheckupListViewModel : Screen
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IMapper _mapper;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly BindableCollection<ForCheckupItemViewModel> _checkups = new BindableCollection<ForCheckupItemViewModel>();
        private string _filterText;
        public ICollectionView Items { get; set; }

        public ForCheckupItemViewModel SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        private BackgroundWorker _backgroundWorker = new BackgroundWorker();
        private ForCheckupItemViewModel _selectedItem;

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

        public async void SubmitDiagnosis(ForCheckupItemViewModel checkupItemViewModel)
        {
            var rslt = await _dialogCoordinator.ShowInputAsync(this, "Diagnosis", "Please enter your diagnosis");
            using (var db = new OPContext())
            {
                var cu = db.Checkups.Find(checkupItemViewModel.Id);
                if (cu == null)
                    return;

                cu.Diagnosis = rslt;
                db.Entry(cu).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void MarkAsDone(ForCheckupItemViewModel checkupItemViewModel)
        {
            using (var db = new OPContext())
            {
                var cu = db.Checkups.Find(checkupItemViewModel.Id);
                if (cu == null)
                    return;

                cu.IsDone = true;
                db.Entry(cu).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public ForCheckupListViewModel(LoginViewModel loginViewModel, IMapper mapper,
            IDialogCoordinator dialogCoordinator)
        {
            _loginViewModel = loginViewModel;
            _mapper = mapper;
            _dialogCoordinator = dialogCoordinator;

            Items = CollectionViewSource.GetDefaultView(_checkups);

            DisplayName = "My Patients";
            ReloadData();

            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerAsync();
        }

        private void ReloadData()
        {
            //_checkups.Clear();

            using (var db = new OPContext())
            {
                var fromDbCheckups = db.Checkups.Include("Patient")
                    .Where(c => c.DoctorId == _loginViewModel.Account.Id && !c.IsDone)
                    .ToList();

                if (!fromDbCheckups.Any())
                {
                    _checkups.Clear();
                    return;
                }


                var selectedId = SelectedItem?.Id;

                var toDelete = _checkups.Where(c => !fromDbCheckups.Select(cu => cu.Id).Contains(c.Id)).ToList();
                if (toDelete.Any())
                    _checkups.RemoveRange(toDelete);

                foreach (var fromDbCheckup in fromDbCheckups)
                {
                    var viewCheckup = _checkups.FirstOrDefault(c => c.Id == fromDbCheckup.Id);
                    if (viewCheckup == null)
                    {
                        _checkups.Add(_mapper.Map<ForCheckupItemViewModel>(fromDbCheckup));
                    }
                    else
                    {
                        var viewCheckupViewModel = _mapper.Map<ForCheckupItemViewModel>(fromDbCheckup);
                        if (viewCheckupViewModel.Equals(viewCheckup))
                            return;

                        _checkups.Remove(viewCheckup);
                        _checkups.Add(viewCheckupViewModel);
                    }
                }

                //_checkups.AddRange(checkups
                //    .Select(c => _mapper.Map<ForCheckupItemViewModel>(c)));

                if (selectedId.HasValue)
                {
                    SelectedItem = _checkups.FirstOrDefault(c => c.Id == selectedId);
                }
            }
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(2000);
                    ReloadData();
                }
                catch (Exception exception)
                {
                    Debug.WriteLine("Error _backgroundWorker_DoWork(): " + exception.Message);
                }
            }
        }
    }
}
