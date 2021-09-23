using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using pills_scheduler.Data;
using pills_scheduler.Models;
using pills_scheduler.Views;

namespace pills_scheduler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PillsListPage : ContentPage
    {

        public DateTime currentDate = DateTime.Now.Date;
        public List<Pills> pills;

        public PillsListPage()
        {
            InitializeComponent();

            
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            PillsDatabase database = await PillsDatabase.Instance;

            pills = await database.GetItemsAsync();

            


            listView.ItemsSource = pills;

        }

        private List<Pills> SelectingPills(List<Pills> pills)
        {
            var currentDatePills = 
        }

        //private List<Pills> FilteredPills(List<Pills> pills)
        //{
        //    var currentDatePills = new List<Pills>;
        //    foreach(var pill in pills)
        //    {
        //        if (currentDate == DateTime.Now.Date)
        //        {

        //        }
        //    }
        //}





        //bool shouldUsePillsToday(Pills pill)
        //{
        //    var todayOrNot = pill.InitialDate.Date <= currentDate;

        //    // timeframe count
        //    var insideDuration = false;
        //    if (pill.Duration == "Постоянно")
        //    {
        //        insideDuration = true;
        //    }
        //    else if (pill.Duration == "1 неделя")
        //    {
        //        insideDuration = (currentDate - pill.InitialDate.Date).TotalDays <= 6;
        //    }
        //    else if (pill.Duration == "2 недели")
        //    {
        //        insideDuration = (currentDate - pill.InitialDate.Date).TotalDays <= 13;
        //    }
        //    else if (pill.Duration == "3 недели")
        //    {
        //        insideDuration = (currentDate - pill.InitialDate.Date).TotalDays <= 20;
        //    }
        //    else if (pill.Duration == "4 недели")
        //    {
        //        insideDuration = (currentDate - pill.InitialDate.Date).TotalDays <= 27;
        //    }

        //    // days count
        //    var insideDayLimit = false;
        //    if (pill.DayFrequency == "Каждый день")
        //    {
        //        insideDayLimit = true;
        //    }
        //    else if (pill.DayFrequency == "Каждые 2 дня")
        //    {
        //        insideDayLimit = (currentDate - pill.InitialDate.Date).TotalDays % 2 == 0;
        //    }
        //    else if (pill.DayFrequency == "Каждые 3 дня")
        //    {
        //        insideDayLimit = (currentDate - pill.InitialDate.Date).TotalDays % 3 == 0;
        //    }
        //    else if (pill.DayFrequency == "Каждые 4 дня")
        //    {
        //        insideDayLimit = (currentDate - pill.InitialDate.Date).TotalDays % 4 == 0;
        //    }
        //    else if (pill.DayFrequency == "Каждую неделю")
        //    {
        //        insideDayLimit = (currentDate - pill.InitialDate.Date).TotalDays % 7 == 0;
        //    }
        //    else if (pill.DayFrequency == "Каждые 2 недели")
        //    {
        //        insideDayLimit = (currentDate - pill.InitialDate.Date).TotalDays % 14 == 0;
        //    }
        //    else if (pill.DayFrequency == "Каждые 3 недели")
        //    {
        //        insideDayLimit = (currentDate - pill.InitialDate.Date).TotalDays % 21 == 0;
        //    }
        //    else if (pill.DayFrequency == "Каждые 4 недели")
        //    {
        //        insideDayLimit = (currentDate - pill.InitialDate.Date).TotalDays % 28 == 0;
        //    }

        //    return todayOrNot && insideDuration && insideDayLimit;
        //}



        private async void OnPillAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PillPage
            {
                BindingContext = new Pills()
            });
        }

        private async void OnListPillSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new PillPage
                {
                    BindingContext = e.SelectedItem as Pills
                });
            }
        }


    }
}