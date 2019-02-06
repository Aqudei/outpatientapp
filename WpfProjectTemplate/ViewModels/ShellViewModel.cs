using Caliburn.Micro;
using OutPatientApp.Models;

namespace OutPatientApp.ViewModels
{
    sealed class ShellViewModel : Conductor<object>.Collection.OneActive
    {
        private readonly IWindowManager _windowManager;

        public ShellViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            //Items.Add(IoC.Get<PatientRegistrationViewModel>());
            //Items.Add(IoC.Get<PatientListViewModel>());
        }

        protected override void OnViewReady(object view)
        {

#if DEBUG
            Execute.OnUIThreadAsync(() => { _windowManager.ShowDialog(IoC.Get<AccountsManagerViewModel>()); });
#endif

            Execute.OnUIThreadAsync(() =>
            {
                var vm = IoC.Get<LoginViewModel>();
                var dlgResult = _windowManager.ShowDialog(vm);

                if (dlgResult.HasValue && dlgResult.Value)
                {
                    if (vm.Account.AccountType == AccountType.ChiefNurse)
                    {
                        Items.Add(IoC.Get<AccountsManagerViewModel>());
                    }
                }
            });
        }
    }
}
