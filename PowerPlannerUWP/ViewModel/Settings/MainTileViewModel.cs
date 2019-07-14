﻿using PowerPlannerAppDataLibrary.ViewModels.MainWindow.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BareMvvm.Core.ViewModels;

namespace PowerPlannerUWP.ViewModel.Settings
{
    public class MainTileViewModel : BaseSettingsViewModelWithAccount
    {
        public MainTileViewModel(BaseViewModel parent) : base(parent)
        {
        }
    }
}
