using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OutPatientApp.Models;
using OutPatientApp.Persistence;

namespace OutPatientApp.ViewModels
{
    class CheckupListViewModel : Screen
    {
        public Guid PatientId { get; set; }
        public BindableCollection<Checkup> Checkups { get; set; }
            = new BindableCollection<Checkup>();

        public override string DisplayName { get; set; } = "List of checkups";

        public CheckupListViewModel()
        {

        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            Checkups.Clear();

            using (var db = new OPContext())
            {
                Checkups.AddRange(db.Checkups.Where(c => c.PatientId == PatientId).ToList());
            }
        }
    }
}
