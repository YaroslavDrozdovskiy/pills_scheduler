using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using pills_scheduler.Views;
using Plugin.LocalNotification;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace pills_scheduler
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new PillsListPage());

            //NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;
            //NotificationCenter.Current.NotificationReceived += OnLocalNotificationReceived;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
