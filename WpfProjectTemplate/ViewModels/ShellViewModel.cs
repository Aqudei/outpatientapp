using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using OutPatientApp.Models;

namespace OutPatientApp.ViewModels
{
    sealed class ShellViewModel : Conductor<object>.Collection.OneActive
    {
        private readonly IWindowManager _windowManager;
        private string _userName;
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        public ShellViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            //Items.Add(IoC.Get<PatientRegistrationViewModel>());
            //Items.Add(IoC.Get<PatientListViewModel>());

            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();
        }

        private DateTime _dateTime;

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { Set(ref _dateTime, value); }
        }


        private void _dispatcherTimer_Tick(object sender, System.EventArgs e)
        {
            DateTime = DateTime.Now;
        }

        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }

        public void Logout()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        protected override void OnViewReady(object view)
        {
            //            var settings = new Dictionary<string, object>();
            //            settings.Add("WindowState", WindowState.Maximized);
            //            Execute.OnUIThreadAsync(() => _windowManager.ShowWindow(IoC.Get<AccountsManagerViewModel>(), settings: settings));

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

                    UserName = vm.Account.UserName;
                }
                else
                {
                    Application.Current.Shutdown();
                }
            });
        }
    }
}
