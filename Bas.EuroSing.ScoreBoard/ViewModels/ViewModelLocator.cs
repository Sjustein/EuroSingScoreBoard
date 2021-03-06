﻿using Bas.EuroSing.ScoreBoard.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.EuroSing.ScoreBoard.ViewModels
{
    internal class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
                SimpleIoc.Default.Register<IDataService, DesignDataService>();
            else
                SimpleIoc.Default.Register<IDataService, DataService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ResultsControlPanelViewModel>();
            SimpleIoc.Default.Register<ResultsViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<VotersViewModel>();
            SimpleIoc.Default.Register<VoteViewModel>();            
        }

        public MainViewModel Main { get { return ServiceLocator.Current.GetInstance<MainViewModel>(); } }
        public ResultsControlPanelViewModel ResultsControlPanel { get { return ServiceLocator.Current.GetInstance<ResultsControlPanelViewModel>(); } }
        public ResultsViewModel Results { get { return ServiceLocator.Current.GetInstance<ResultsViewModel>(); } }
        public SettingsViewModel Settings { get { return ServiceLocator.Current.GetInstance<SettingsViewModel>(); } }
        public VotersViewModel Voters { get { return ServiceLocator.Current.GetInstance<VotersViewModel>(); } }
        public VoteViewModel Vote { get { return ServiceLocator.Current.GetInstance<VoteViewModel>(); } }
    }
}
