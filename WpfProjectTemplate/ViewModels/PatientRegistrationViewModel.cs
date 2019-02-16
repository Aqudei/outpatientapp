using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using AForge.Video.DirectShow;
using AutoMapper;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using OutPatientApp.Models;
using OutPatientApp.Persistence;

namespace OutPatientApp.ViewModels
{
    enum CameraState
    {
        Standby, Captured, Capturing
    }

    sealed class PatientRegistrationViewModel : Screen
    {
        private readonly IMapper _mapper;
        private readonly IDialogCoordinator _dialogCoordinator;
        private string _lastName;
        private string _firstName;
        private string _middleName;
        private DateTime _birthday;
        private string _birthPlace;
        private string _civilStatus;
        private string _sex;
        private string _address;
        private string _nextKin;
        private string _kinRelationship;
        private readonly VideoCaptureDevice _localWebcam;
        private BitmapImage _bitmapImage;
        public override string DisplayName { get; set; } = "Out-Patient Registration";

        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value);
        }

        public string MiddleName
        {
            get => _middleName;
            set => Set(ref _middleName, value);
        }

        public DateTime Birthday
        {
            get => _birthday;
            set => Set(ref _birthday, value);
        }

        public string BirthPlace
        {
            get => _birthPlace;
            set => Set(ref _birthPlace, value);
        }

        public string CivilStatus
        {
            get => _civilStatus;
            set => Set(ref _civilStatus, value);
        }

        public List<string> CivilStatuses { get; set; } = new List<string>();

        public string Sex
        {
            get => _sex;
            set => Set(ref _sex, value);
        }

        public string Address
        {
            get => _address;
            set => Set(ref _address, value);
        }

        public List<string> Sexes { get; set; } = new List<string>();

        public string NextKin
        {
            get => _nextKin;
            set => Set(ref _nextKin, value);
        }

        public string KinRelationship
        {
            get => _kinRelationship;
            set => Set(ref _kinRelationship, value);
        }

        public PatientRegistrationViewModel(IMapper mapper, IDialogCoordinator dialogCoordinator)
        {
            _mapper = mapper;
            _dialogCoordinator = dialogCoordinator;

            Sexes.Add("Male");
            Sexes.Add("Female");

            CivilStatuses.Add("Single");
            CivilStatuses.Add("Married");
            CivilStatuses.Add("Divorced");
            CivilStatuses.Add("Separated");
            CivilStatuses.Add("Widowed");

            Birthday = DateTime.Now;

            var webCams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (webCams.Count <= 0)
                return;

            _localWebcam = new VideoCaptureDevice(webCams[0].MonikerString);
            _localWebcam.NewFrame += _localWebcam_NewFrame;
            _localWebcam.Start();

            _imageDirectory = Properties.Settings.Default.PhotoDirectory;
            Directory.CreateDirectory(_imageDirectory);

            Application.Current.Exit += Current_Exit;
        }


        private void Current_Exit(object sender, ExitEventArgs e)
        {
            _localWebcam.NewFrame -= _localWebcam_NewFrame;
            _localWebcam.SignalToStop();
        }

        public BitmapImage PictureImage
        {
            get => _bitmapImage;
            set => Set(ref _bitmapImage, value);
        }

        private void _localWebcam_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Execute.OnUIThread(() => PictureImage = eventArgs.Frame.ToBitmapImage());

            if (CameraState != CameraState.Capturing)
                return;

            _savableImage = eventArgs.Frame;
            CameraState = CameraState.Captured;
            _localWebcam.NewFrame -= _localWebcam_NewFrame;
        }


        public CameraState CameraState
        {
            get => _cameraState;
            private set
            {
                Set(ref _cameraState, value);
                NotifyOfPropertyChange(nameof(CanCaptureImage));
                NotifyOfPropertyChange(nameof(CanResetCapture));
            }
        }

        private Bitmap _savableImage = null;
        private string _imageDirectory;
        private CameraState _cameraState = CameraState.Standby;
        private Guid _id;

        public void ResetCapture()
        {
            _savableImage = null;
            _localWebcam.NewFrame += _localWebcam_NewFrame;
            CameraState = CameraState.Standby;
        }

        public bool CanCaptureImage => CameraState == CameraState.Standby;
        public bool CanResetCapture => CameraState == CameraState.Captured;

        public Guid Id
        {
            get => _id;
            set
            {
                var changed = value != _id;
                Set(ref _id, value);

                //NotifyOfPropertyChange(nameof(CanSave));

                if (!changed)
                    return;

                if (Id == Guid.Empty)
                {
                    FirstName = "";
                    LastName = "";
                    NextKin = "";
                    MiddleName = "";
                    BirthPlace = "";
                    KinRelationship = "";
                    Address = "";
                }
                else
                {
                    using (var db = new OPContext())
                    {
                        var patient = db.Patients.FirstOrDefault(p => p.Id == Id);
                        if (patient != null)
                        {
                            _mapper.Map(patient, this);
                        }
                    }
                }
            }
        }

        public void CaptureImage()
        {
            CameraState = CameraState.Capturing;
        }

        public void NewItem()
        {
            Id = Guid.Empty;
        }

        //public bool CanSave => Id != Guid.Empty;

        public void Save()
        {
            var patient = _mapper.Map<Patient>(this);
            if (patient.Id == Guid.Empty)
                patient.Id = Guid.NewGuid();

            if (_localWebcam != null && !string.IsNullOrWhiteSpace(_imageDirectory))
            {
                var imagePath = Path.Combine(_imageDirectory, patient.Id + ".png");
                PictureImage?.SaveImage(imagePath);
                //PictureImage.ToBytes();
            }


            using (var db = new OPContext())
            {
                var p = db.Patients.Find(Id);

                if (p == null)
                {
                    db.Patients.Add(patient);
                    db.SaveChanges();
                    _dialogCoordinator.ShowMessageAsync(this, "Success", "Patient's record was added!");
                }
                else
                {
                    var id = p.Id;

                    db.Entry(p).State = EntityState.Modified;
                    _mapper.Map(this, db.Entry(p).Entity);
                    db.Entry(p).Entity.Id = id;
                    db.SaveChanges();
                    _dialogCoordinator.ShowMessageAsync(this, "Success", "Patient's record was updated!");
                }
            }
        }
    }
}
