using System;
using System.ComponentModel;
using System.Windows.Data;
using Caliburn.Micro;
using OutPatientApp.Models;

namespace OutPatientApp.ViewModels
{
    internal class AccountsManagerViewModel : Screen
    {
        private readonly BindableCollection<Account> _accounts = new BindableCollection<Account>();


        private AccountType _accountType;
        private DateTime? _birthday;

        private string _firstName;


        private string _lastName;

        private string _sex;

        private string _specialization;

        private string _userName;

        public AccountsManagerViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(_accounts);
        }

        public override string DisplayName { get; set; } = "Accounts";
        public ICollectionView Items { get; set; }

        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }

        public AccountType AccountType
        {
            get => _accountType;
            set => Set(ref _accountType, value);
        }

        public string Specialization
        {
            get => _specialization;
            set => Set(ref _specialization, value);
        }

        public DateTime? Birthday
        {
            get => _birthday;
            set => Set(ref _birthday, value);
        }

        public string Sex
        {
            get => _sex;
            set => Set(ref _sex, value);
        }

        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }
    }
}