using Caliburn.Micro;

namespace OutPatientApp.ViewModels
{
    sealed class ShellViewModel : Conductor<object>.Collection.OneActive
    {
        public ShellViewModel()
        {
            Items.Add(IoC.Get<PatientRegistrationViewModel>());
            Items.Add(IoC.Get<PatientListViewModel>());
        }
    }
}
