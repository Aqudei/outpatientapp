using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OutPatientApp.ViewModels;

namespace OutPatientApp.Views
{
    /// <summary>
    /// Interaction logic for ChangePasswordView.xaml
    /// </summary>
    public partial class ChangePasswordView : UserControl
    {
        public ChangePasswordView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ChangePasswordViewModel;
            vm.Password = Password.Password;
        }

        private void PasswordCopy_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ChangePasswordViewModel;
            vm.PasswordCopy = PasswordCopy.Password;
        }
    }
}
