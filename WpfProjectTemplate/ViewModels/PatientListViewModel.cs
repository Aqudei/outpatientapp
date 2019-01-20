using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using OutPatientApp.Persistence;

namespace OutPatientApp.ViewModels
{
    class PatientListViewModel : Screen
    {
        private readonly IMapper _mapper;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IWindowManager _windowManager;
        private readonly string _imageDirectory;

        public override string DisplayName { get; set; } = "Out-Patient List";

        public BindableCollection<PatientDetailViewModel> Patients { get; set; }
            = new BindableCollection<PatientDetailViewModel>();

        public PatientListViewModel(IMapper mapper, IDialogCoordinator dialogCoordinator,
            IWindowManager windowManager)
        {
            _mapper = mapper;
            _dialogCoordinator = dialogCoordinator;
            _windowManager = windowManager;


            _imageDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SPHOutPatient");
            Directory.CreateDirectory(_imageDirectory);
        }

        protected override void OnViewReady(object view)
        {
            ReloadData();
        }

        private void ReloadData()
        {
            Patients.Clear();
            using (var db = new OPContext())
            {
                Patients.AddRange(db.Patients.ToList().Select(p => _mapper.Map<PatientDetailViewModel>(p)));
                foreach (var patient in Patients)
                {
                    patient.PictureImage = Path.Combine(_imageDirectory, patient.Id + ".png");
                }
            }
        }

        public void ViewCheckups(PatientDetailViewModel patientVm)
        {
            var dlg = IoC.Get<CheckupListViewModel>();
            dlg.PatientId = patientVm.Id;
            _windowManager.ShowDialog(dlg);
        }


        public void AddCheckup(PatientDetailViewModel patientVm)
        {
            var dlg = IoC.Get<AddCheckupViewModel>();
            dlg.PatientId = patientVm.Id;
            _windowManager.ShowDialog(dlg);
        }

        public async void Delete(PatientDetailViewModel patient)
        {
            using (var db = new OPContext())
            {
                var p = db.Patients.Find(patient.Id);
                if (p == null)
                {
                    return;
                }

                db.Patients.Remove(p);
                db.SaveChanges();
                await _dialogCoordinator.ShowMessageAsync(this, "Success", "Patient record was deleted");
                ReloadData();
            }
        }
    }
}
