using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace VKForm
{
    public partial class App : Application
    {
        public readonly VkApi api;

        public App(IVKHttp vkHttp)
        {
            InitializeComponent();

            api = new VkApi(vkHttp);
            MainPage = new NavigationPage(new VKForm.MainPage(api));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
