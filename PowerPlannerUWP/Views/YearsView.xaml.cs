﻿using PowerPlannerAppDataLibrary.ViewModels.MainWindow.MainScreen.Years;
using PowerPlannerUWP.Views;
using PowerPlannerUWP.Views.YearViews;
using PowerPlannerUWPLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PowerPlannerUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class YearsView : MainScreenContentViewHostGeneric
    {
        public YearsView()
        {
            this.InitializeComponent();

            base.HideCommandBar();
            Visibility = Visibility.Collapsed;
        }

        public new YearsViewModel ViewModel
        {
            get { return base.ViewModel as YearsViewModel; }
            set { base.ViewModel = value; }
        }

        public override void OnViewModelLoadedOverride()
        {
            base.OnViewModelLoadedOverride();

            // Only show once the view model has loaded (otherwise we get "[GPA]" displayed)
            Visibility = Visibility.Visible;
        }

        private void ButtonAddYear_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddYear();
        }

        private void thisPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 500)
                VisualStateManager.GoToState(this, "CompactTitleState", true);
            else
                VisualStateManager.GoToState(this, "NormalTitleState", true);
        }

        private void YearView_OnRequestAddSemester(object sender, PowerPlannerAppDataLibrary.ViewItems.ViewItemYear e)
        {
            ViewModel.AddSemester(e.Identifier);
        }

        private void YearView_OnRequestEditSemester(object sender, PowerPlannerAppDataLibrary.ViewItems.ViewItemSemester e)
        {
            ViewModel.EditSemester(e);
        }

        private void YearView_OnRequestEditYear(object sender, PowerPlannerAppDataLibrary.ViewItems.ViewItemYear e)
        {
            ViewModel.EditYear(e);
        }

        private void YearView_OnRequestOpenSemester(object sender, PowerPlannerAppDataLibrary.ViewItems.ViewItemSemester e)
        {
            ViewModel.OpenSemester(e.Identifier);
        }
    }
}
