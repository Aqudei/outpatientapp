using System;
using System.Collections.Generic;
using System.Windows;
using AutoMapper;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using OutPatientApp.Models;
using OutPatientApp.Services;
using OutPatientApp.ViewModels;
using Unity;

namespace OutPatientApp
{
    class Bootstrapper : BootstrapperBase
    {
        private readonly IUnityContainer _container = new UnityContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void Configure()
        {
            _container.RegisterSingleton<ShellViewModel>();
            _container.RegisterInstance(DialogCoordinator.Instance);
            _container.RegisterSingleton<IEventAggregator, EventAggregator>();
            _container.RegisterInstance(DialogCoordinator.Instance);
            _container.RegisterSingleton<IWindowManager, WindowManager>();
            _container.RegisterSingleton<LoginViewModel>();
            _container.RegisterSingleton<PatientRegistrationViewModel>();
            _container.RegisterSingleton<CaseNumberGen>();

            Mapper.Initialize(config =>
            {
                config.CreateMap<AccountsManagerViewModel, Account>().ReverseMap();
                config.CreateMap<Account, Account>().ForMember(m => m.Password, opts => opts.Ignore());

                config.CreateMap<PatientRegistrationViewModel, Patient>()
                    .ForMember(m => m.LastUpdated, opts => opts.Ignore())
                    .ReverseMap();


                config.CreateMap<Patient, PatientDetailViewModel>()
                    .ForMember(m => m.FullName, opts => opts.MapFrom(p => $"{p.LastName}, {p.FirstName} {p.MiddleName}"));

                config.CreateMap<Checkup, ForCheckupItemViewModel>()
                    .ForMember(m => m.Address, opt => opt.MapFrom(src => src.Patient.Address))
                    .ForMember(m => m.Age, opt => opt.MapFrom(src => src.Patient.Age))
                    .ForMember(m => m.FullName, opt => opt.MapFrom(src => src.Patient.FullName))
                    .ForMember(m => m.Complaint, opt => opt.MapFrom(src => src.Complaint))
                    .ReverseMap();
            });

            _container.RegisterInstance(Mapper.Instance);

            base.Configure();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.ResolveAll(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.Resolve(service, key);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
