using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Data;
using AutoMapper;
using Caliburn.Micro;
using OutPatientApp.Models;
using OutPatientApp.Persistence;

namespace OutPatientApp.ViewModels
{
    public sealed class AccountsManagerViewModel : Screen
    {
        private readonly BindableCollection<Account> _accounts = new BindableCollection<Account>();
        private readonly IMapper _mapper;
        private Account _selectedItem;

        public Account SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);
                _mapper.Map(value, this);
            }
        }


        private AccountType _accountType;
        private DateTime _birthday = DateTime.Now;

        private string _firstName;

        private Guid _id = Guid.NewGuid();

        private string _lastName;

        private string _sex = "Male";

        private string _specialization;

        private string _userName;

        public AccountsManagerViewModel(IMapper mapper)
        {
            _mapper = mapper;

            Items = CollectionViewSource.GetDefaultView(_accounts);
            DisplayName = "Accounts";

            Execute.OnUIThreadAsync(() =>
            {
                _accounts.Clear();
                using (var db = new OPContext())
                {
                    _accounts.AddRange(db.Accounts.ToList());
                }
            });
        }

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
            set
            {
                Set(ref _accountType, value);
                NotifyOfPropertyChange(nameof(IsDoctor));
            }
        }

        public string Specialization
        {
            get => _specialization;
            set => Set(ref _specialization, value);
        }

        public DateTime Birthday
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

        public bool IsDoctor => AccountType == AccountType.Doctor;
        public string[] Sexes { get; set; } = { "Male", "Female" };
        public AccountType[] AccountTypes { get; set; } = (AccountType[])Enum.GetValues(typeof(AccountType));
        public Guid Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        public void Save()
        {
            using (var db = new OPContext())
            {
                var account = _mapper.Map<Account>(this);
                var existingAccount = db.Accounts.Find(account.Id);

                if (existingAccount == null)
                {
                    account.SetPassword(Birthday.ToShortDateString().Replace("/", ""));

                    var added = db.Accounts.Add(account);
                    db.SaveChanges();
                    _accounts.Add(added);
                }
                else
                {
                    var id = existingAccount.Id;
                    _mapper.Map(account, existingAccount);
                    existingAccount.Id = id;
                    db.Entry(existingAccount).State = EntityState.Modified;
                    _accounts.Remove(existingAccount);
                    _accounts.Add(existingAccount);
                    db.SaveChanges();
                }
            }
        }

        public void NewItem()
        {
            Id = Guid.NewGuid();
            FirstName = "";
            LastName = "";
            UserName = "";
        }

        public void Delete()
        {
            if (SelectedItem != null)
            {
                using (var db = new OPContext())
                {
                    var existing = db.Accounts.Find(SelectedItem.Id);
                    if (existing != null)
                    {
                        db.Accounts.Remove(existing);
                        db.SaveChanges();
                    }

                    _accounts.Remove(SelectedItem);
                }
            }
        }
    }
}