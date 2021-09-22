using System;
using System.Collections.Generic;
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
        public PillsListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            PillsDatabase database = await PillsDatabase.Instance;
            listView.ItemsSource = await database.GetItemsAsync();

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

    }
}