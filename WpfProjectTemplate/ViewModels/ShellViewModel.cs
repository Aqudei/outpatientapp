using System.Collections.Generic;
using System.Windows;
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
            var settings = new Dictionary<string, object>();
            settings.Add("WindowState", WindowState.Maximized);
            Execute.OnUIThreadAsync(() => _windowManager.ShowWindow(IoC.Get<AccountsManagerViewModel>(), settings: settings));

            Execute.OnUIThread(() =>
            {
                var vm = IoC.Get<LoginViewModel>();
                var dlgResult = _windowManager.ShowDialog(vm);

                if (dlgResult.HasValue && dlgResult.Value)
                {
                    if (vm.Account.AccountType == AccountType.ChiefNurse)
                    {
                        ActivateItem(IoC.Get<AccountsManagerViewModel>());
                        Items.Add(IoC.Get<PatientListViewModel>());
                    }
                    if (vm.Account.AccountType == AccountType.Clerk)
                    {
                        ActivateItem(IoC.Get<PatientRegistrationViewModel>());
                        Items.Add(IoC.Get<PatientListViewModel>());
                    }

                    if (vm.Account.AccountType == AccountType.Doctor)
                    {
                        ActivateItem(IoC.Get<ForCheckupListViewModel>());
                    }
                }
                else
                {
                    Application.Current.Shutdown();
                }
            });
        }
    }
}
