using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;

namespace VKForm.Droid
{
    class VkHttp : VKForm.IVKHttp
    {
        private readonly HttpClient client = new HttpClient();

        async Task<string> IVKHttp.getAsync(string uri)
        {            
            var response = await client.GetAsync(uri);
            return await response.Content.ReadAsStringAsync();            
        }

        string IVKHttp.urlEncode(string value)
        {
            return HttpUtility.UrlEncode(value);
        }
    }

    [Activity(Label = "VKForm", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);            
            LoadApplication(new App(new VkHttp()));
        }
    }
}

