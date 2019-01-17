﻿using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
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
            _container.RegisterSingleton<IEventAggregator, EventAggregator>();
            _container.RegisterInstance(DialogCoordinator.Instance);
            _container.RegisterSingleton<IWindowManager, WindowManager>();
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
