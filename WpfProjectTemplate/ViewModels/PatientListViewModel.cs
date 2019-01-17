using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Caliburn.Micro;
using OutPatientApp.Persistence;

namespace OutPatientApp.ViewModels
{
    class PatientListViewModel : Screen
    {
        private readonly IMapper _mapper;

        public override string DisplayName { get; set; } = "Out-Patient List";

        public BindableCollection<PatientDetailViewModel> Patients { get; set; }
            = new BindableCollection<PatientDetailViewModel>();

        public PatientListViewModel(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected override void OnViewReady(object view)
        {
            using (var db = new OPContext())
            {
                Patients.AddRange(db.Patients.ToList().Select(p => _mapper.Map<PatientDetailViewModel>(p)));
            }
        }
    }
}
