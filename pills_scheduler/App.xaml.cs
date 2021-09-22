using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using pills_scheduler.Views;

namespace pills_scheduler
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new PillsListPage());
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
