using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PowerPlannerAppDataLibrary.ViewItems;
using InterfacesDroid.Themes;
using Android.Graphics.Drawables;
using InterfacesDroid.Helpers;
using ToolsPortable;
using Android.Graphics;
using PowerPlannerAppDataLibrary;
using InterfacesDroid.Bindings.Programmatic;

namespace PowerPlannerAndroid.Views.ListItems
{
    public class MyScheduleItemView : LinearLayout
    {
        public ViewItemSchedule Schedule { get; private set; }

        private BindingInstance m_classColorBindingInstance;
        private BindingInstance m_classNameBindingInstance;

        public MyScheduleItemView(Context context, ViewItemSchedule s) : base(context)
        {
            Schedule = s;

            base.Orientation = Orientation.Vertical;
            base.SetPaddingRelative(ThemeHelper.AsPx(context, 5), ThemeHelper.AsPx(context, 5), 0, 0);

            m_classColorBindingInstance = Binding.SetBinding(s.Class, nameof(s.Class.Color), (c) =>
            {
                base.Background = new ColorDrawable(ColorTools.GetColor(c.Color));
            });

            // Can't figure out how to let both class name and room wrap while giving more importance
            // to room like I did on UWP, so just limiting name to 2 lines for now till someone complains.
            var textViewName = CreateTextView("");

            m_classNameBindingInstance = Binding.SetBinding(s.Class, nameof(s.Class.Name), (c) =>
            {
                textViewName.Text = c.Name;
            });

            textViewName.SetMaxLines(2);
            base.AddView(textViewName);

            base.AddView(CreateTextView(GetStringTimeToTime(s)));

            if (!string.IsNullOrWhiteSpace(s.Room))
            {
                base.AddView(CreateTextView(s.Room));
            }
        }

        public static string GetStringTimeToTime(ViewItemSchedule s)
        {
            return PowerPlannerResources.GetStringTimeToTime(DateHelper.ToShortTimeString(s.StartTime), DateHelper.ToShortTimeString(s.EndTime));
        }

        private TextView CreateTextView(string text)
        {
            var view = new TextView(Context)
            {
                Text = text
            };

            view.SetTextColor(new Color(255, 255, 255));

            return view;
        }
    }
}