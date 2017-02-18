using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VKForm
{
    public partial class MainPage : ContentPage
    {
        private readonly string FormDataTemplate =
            "Имя: {0}\n" +
            "Фамилия: {1}\n" +
            "Страна: {2}\n" +
            "Город: {3}\n" +
            "ВУЗ: {4}";

        private VkApi api;

        private Dictionary<int, string> countries;
        private int country_id;

        public MainPage(VkApi api)
        {
            this.api = api;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            UpdateStates();
            PopulateCountries();
        }

        private async void PopulateCountries()
        {
            try
            {
                countries = await api.getCountriesAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Could not load countries", ex.Message, "Cancel");
            }

            foreach (string item in countries.Values.OrderBy(x => x))
            {
                Country.Items.Add(item);
            }
        }

        private void Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Country.SelectedIndex >= 0)
            {
                string value = Country.Items[Country.SelectedIndex];
                country_id = countries.FirstOrDefault(x => x.Value == value).Key;
            }
            else
                country_id = 0;
            UpdateStates();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateStates();
        }

        private async void City_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateStates();

            // todo:
            await api.getCitiesAsync(country_id, City.Text);
        }

        private async void SubmitClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataPage() {
                FormDataText = String.Format(FormDataTemplate, FName.Text, LName.Text,
                  Country.SelectedIndex >= 0 ? Country.Items[Country.SelectedIndex] : "",
                  City.Text, University.Text)
            });
        }

        void UpdateStates()
        {
            LName.IsEnabled = !string.IsNullOrEmpty(FName.Text);
            Country.IsEnabled = LName.IsEnabled && !string.IsNullOrEmpty(LName.Text);
            City.IsEnabled = Country.IsEnabled && (Country.SelectedIndex >= 0);
            University.IsEnabled = City.IsEnabled && !string.IsNullOrEmpty(City.Text);
            //Submit.IsEnabled = University.IsEnabled && !string.IsNullOrEmpty(University.Text);
        }

    }
}
