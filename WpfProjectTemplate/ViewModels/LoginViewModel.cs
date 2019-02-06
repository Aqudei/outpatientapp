using System;
using System.Diagnostics;
using System.Linq;
using Caliburn.Micro;
using OutPatientApp.Models;
using OutPatientApp.Persistence;

namespace OutPatientApp.ViewModels
{
    internal class LoginViewModel : Screen
    {
        private Account _account;

        private string _password;
        private string _userName;

        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public Account Account
        {
            get => _account;
            set => Set(ref _account, value);
        }


        public void Login()
        {
            try
            {
                using (var db = new OPContext())
                {
                    var account = db.Accounts.Single(u => u.UserName == UserName);
                    if (!account.VerifyPassword(Password))
                        return;
                    Account = account;
                    TryClose(true);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void ExitApp()
        {
            TryClose(false);
        }
    }
}