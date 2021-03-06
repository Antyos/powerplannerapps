﻿using PowerPlannerAppDataLibrary.ViewModels.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ToolsPortable;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace PowerPlannerUWP.Controls
{
    public sealed partial class RecurrenceControl : UserControl
    {
        public RecurrenceControl()
        {
            this.InitializeComponent();

            DataContextChanged += RecurrenceControl_DataContextChanged;
        }

        private void RecurrenceControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext is RecurrenceControlViewModel)
            {
                (DataContext as RecurrenceControlViewModel).PropertyChanged += new WeakEventHandler<PropertyChangedEventArgs>(RecurrenceControl_PropertyChanged).Handler;
            }

            AssignDateFromViewModel();
        }

        private void RecurrenceControl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            AssignDateFromViewModel();
        }

        private void AssignDateFromViewModel()
        {
            // Data binding with calendar picker doesn't work, so assigning it directly
            CalendarPickerEndDate.Date = (DataContext as RecurrenceControlViewModel)?.EndDate;
        }

        private void CalendarPickerEndDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            // Data binding with calendar picker doesn't work, so assigning it directly
            if (DataContext is RecurrenceControlViewModel)
            {
                (DataContext as RecurrenceControlViewModel).EndDate = sender.Date.GetValueOrDefault().Date;
            }
        }

        // Host must assign DataContext to a RecurrenceControlViewModel instance
    }
}
