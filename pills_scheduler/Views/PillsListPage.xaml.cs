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
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.iOSOption;

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
            NotificationCenter.Current.RegisterCategoryList(new HashSet<NotificationCategory>(new List<NotificationCategory>()
            {
                new NotificationCategory(NotificationCategoryType.Status)
                {
                    ActionList = new HashSet<NotificationAction>( new List<NotificationAction>()
                    {
                        new NotificationAction(100)
                        {
                            Title = "Hello",
                            AndroidIconName =
                            {
                                ResourceName = "i2",
                            },
                            iOSAction = iOSActionType.None
                        },
                        new NotificationAction(101)
                        {
                            Title = "Close",
                            AndroidIconName =
                            {
                                ResourceName = "i3",
                            },
                            iOSAction = iOSActionType.None
                        }
                    })
                },
            }));

            NotificationCenter.Current.NotificationReceived += OnLocalNotificationReceived;
            NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;
        }

        


        //private void notifyButton(object sender, EventArgs e)
        //{
        //    var notification = new NotificationRequest
        //    {
        //        Description = $"Не то получишь по жопе",
        //        Title = $"Бать, пора пить таблетки",
        //        ReturningData = $"Dummy data",
        //        NotificationId = 1337,
        //    };
        //    notification.Schedule.NotifyTime = DateTime.Now.AddSeconds(5);
        //    notification.Schedule.RepeatType = NotificationRepeat.TimeInterval;
        //    notification.Schedule.NotifyRepeatInterval = TimeSpan.FromSeconds(2);
        //    //notification.Schedule.AndroidAllowedDelay = TimeSpan.FromSeconds(2);

        //    NotificationCenter.Current.Show(notification);
        //}

     



        protected override async void OnAppearing()
        {
            base.OnAppearing();

            PillsDatabase database = await PillsDatabase.Instance;

            pills = await database.GetItemsAsync();

           

            listView.ItemsSource = SelectingTodaysPills(pills);

        }



        List<Pills> SelectingTodaysPills(List<Pills> pills)
        {
            var currentDatePills = new List<Pills>();

            foreach (var item in pills)
            {
                // result check
                if (shouldUsePillsToday(item))
                {
                    if (currentDate == DateTime.Now.Date)
                    {
                        createNotifications(item);
                    }
                    currentDatePills.Add(item);
                }
            }

            return currentDatePills;
        }

        private bool shouldUsePillsToday(Pills pill)
        {

            var dateDelta = (currentDate - pill.InitialDate).TotalDays;

            var inDuration = false;
            if (pill.CourseDuration == "Постоянно")
            {
                inDuration = true;
            }
            else if (pill.CourseDuration == "1 неделя")
            {
                inDuration = (dateDelta <= 6) && (dateDelta >= 0);
            }
            else if (pill.CourseDuration == "2 недели")
            {
                inDuration = (dateDelta <= 13) && (dateDelta >= 0);
            }
            else if (pill.CourseDuration == "3 недели")
            {
                inDuration = (dateDelta <= 20) && (dateDelta >= 0);
            }
            else if (pill.CourseDuration == "4 недели")
            {
                inDuration = (dateDelta <= 27) && (dateDelta >= 0);
            }


            var inDayLimit = false;
            if (pill.FreqByDays == "Каждый день")
            {
                inDayLimit = true;
            }
            else if (pill.FreqByDays == "Каждые 2 дня")
            {
                inDayLimit = dateDelta % 2 == 0;
            }
            else if (pill.FreqByDays == "Каждые 3 дня")
            {
                inDayLimit = dateDelta % 3 == 0;
            }

            else if (pill.FreqByDays == "Каждую неделю")
            {
                inDayLimit = dateDelta % 7 == 0;
            }
            else if (pill.FreqByDays == "Каждые 2 недели")
            {
                inDayLimit = dateDelta % 14 == 0;
            }
            else if (pill.FreqByDays == "Каждые 3 недели")
            {
                inDayLimit = dateDelta % 21 == 0;
            }

            return inDuration && inDayLimit;
        }


        private void createNotifications(Pills pill)
        {

            var pillTimeList = new List<TimeSpan?>() { };
            if (pill.TimeFirst != null)
            {
                pillTimeList.Add(pill.TimeFirst);
            }

            if(pill.TimeSecond != null)
            {
                pillTimeList.Add(pill.TimeSecond);
            }

            if (pill.TimeThird != null)
            {
                pillTimeList.Add(pill.TimeThird);
            }

            int i = 0;
            foreach (var time in pillTimeList)
            {
                var notification = new NotificationRequest
                {
                    Description = $"{pill.Name}",
                    Title = $"Пора съесть таблетку",
                    ReturningData = $"{pill.ID + i}",
                    NotificationId = pill.ID + i,

                };
                notification.Schedule.NotifyTime = DateTime.Today.Date.Add((TimeSpan)time);
                notification.Schedule.RepeatType = NotificationRepeat.TimeInterval;
                notification.Schedule.NotifyRepeatInterval = TimeSpan.FromMinutes(10);
                notification.Schedule.AndroidAllowedDelay = TimeSpan.FromSeconds(10);
                notification.Schedule.NotifyAutoCancelTime = notification.Schedule.NotifyTime + TimeSpan.FromMinutes(21);

                NotificationCenter.Current.Show(notification);
                i++;
            }
   
        }

        private async void OnLocalNotificationTapped(NotificationEventArgs e)
        {
            string answer = await DisplayActionSheet("Пора съесть таблетку", "Я съел", "");
            if (answer == "Я съел")
            {
                NotificationCenter.Current.Cancel(e.Request.NotificationId);
            }
        }

        private async void OnLocalNotificationReceived(NotificationEventArgs e)
        {

            bool answer = await DisplayAlert("Уведомление", "Пора есть таблетку", "Я съел", "Напомнить позже");

            if (answer)
            {
                NotificationCenter.Current.Cancel(e.Request.NotificationId);
            }
           
        }




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

        private void pillInitialDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            currentDate = e.NewDate.Date;
            OnAppearing();

        }


    }
}