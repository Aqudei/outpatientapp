using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace OutPatientApp.ViewModels
{
    class LoginViewModel : Screen
    {
        public void Login()
        {
            TryClose(true);
        }

        public void ExitApp()
        {
            TryClose(false);
        }
    }
}
