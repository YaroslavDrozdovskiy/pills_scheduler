using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using pills_scheduler.Views;
using pills_scheduler.Data;
using pills_scheduler.Models;

namespace pills_scheduler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PillPage : ContentPage
    {
       
        public PillPage()
        {
            InitializeComponent();
            
        }

        private void addTimePickers(int freq)
        {
            var pill = (Pills)BindingContext;

            pillFreqInDayLayout.Children.Clear();

            var pillTimeList = new List<string>() { "TimeFirst", "TimeSecond", "TimeThird" };

            List <TimeSpan> times = new List<TimeSpan>();
            times.Add(new TimeSpan(12, 0, 0));
            times.Add(new TimeSpan(18, 0, 0));
            times.Add(new TimeSpan(22, 0, 0));

            for (int i = 0; i < freq; i++)
            {
                var timePicker = new TimePicker() { Format = "HH:mm", Time = times[i], FontAttributes = FontAttributes.Bold};
                
                Binding timeBinding = new Binding { Source = pill, Path = pillTimeList[i] };
                timePicker.SetBinding(TimePicker.TimeProperty, timeBinding);

                pillFreqInDayLayout.Children.Add(timePicker);
            }
        }

        private void pillFreqInDayChanged(object sender, EventArgs e)
        {
            String freqInDay = pillFreqInDayPicker.Items[pillFreqInDayPicker.SelectedIndex];
            int freq = int.Parse(freqInDay.Substring(0, 1));
            addTimePickers(freq);
        }


        //Сохранение, удаления данных 
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var pillItem = (Pills)BindingContext;
            PillsDatabase database = await PillsDatabase.Instance;
            await database.SaveItemAsync(pillItem);
            await Navigation.PopAsync();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var pillItem = (Pills)BindingContext;
            PillsDatabase database = await PillsDatabase.Instance;
            await database.DeletePillAsync(pillItem);
            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void pillFreqInDayPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}