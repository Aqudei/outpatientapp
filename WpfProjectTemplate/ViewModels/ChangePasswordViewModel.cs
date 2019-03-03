using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using OutPatientApp.Persistence;
using TaskExtensions = System.Threading.Tasks.TaskExtensions;

namespace OutPatientApp.ViewModels
{
    class ChangePasswordViewModel : Screen
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly LoginViewModel _loginViewModel;
        private string _password;
        private string _passwordCopy;
        private string _message;

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public string PasswordCopy
        {
            get => _passwordCopy;
            set => Set(ref _passwordCopy, value);
        }

        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public void Cancel()
        {
            TryClose();
        }

        public void Save()
        {
            if (Password != PasswordCopy || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Passwords are not the same or empty";
                return;
            }

            using (var db = new OPContext())
            {
                var account = _loginViewModel.Account;
                account.SetPassword(Password);

                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                Message = "Password Changed";
            }
        }

        public ChangePasswordViewModel(IDialogCoordinator dialogCoordinator, LoginViewModel loginViewModel)
        {
            _dialogCoordinator = dialogCoordinator;
            _loginViewModel = loginViewModel;
        }
    }
}
