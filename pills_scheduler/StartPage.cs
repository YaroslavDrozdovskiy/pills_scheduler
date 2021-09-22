using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace pills_scheduler
{
    class StartPage: ContentPage
    {
        public StartPage()
        {
            Label header = new Label() { Text = "Здарова", FontSize = 32};
            this.Content = header;
        }
    } 
}
