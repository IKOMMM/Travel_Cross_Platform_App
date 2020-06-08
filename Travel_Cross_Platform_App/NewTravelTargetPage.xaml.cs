using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Cross_Platform_App.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Plugin.Geolocator;
using Travel_Cross_Platform_App.Logic;

namespace Travel_Cross_Platform_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelTargetPage : ContentPage
    {
        public NewTravelTargetPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

            try
            {

            
            var selectedVenue = venueListView.SelectedItem as Venue;
            var firstCategory = selectedVenue.categories.FirstOrDefault();

            Post post = new Post()
            {
                Experience = experienceEntry.Text,
                CategoryID = firstCategory.id,
                CategoryName = firstCategory.name,
                Address = selectedVenue.location.address,
                Distance = selectedVenue.location.distance,
                Latitude = selectedVenue.location.lat,
                Longitude = selectedVenue.location.lng,
                VenueName = selectedVenue.name

            };

            using (SQLiteConnection connect = new SQLiteConnection(App.DatabaseLocation)) {
                connect.CreateTable<Post>();
                int rows = connect.Insert(post);

                if (rows > 0)
                    DisplayAlert("Succes", "Experience succesfully insertded", "Ok");
                else
                    DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
            };
            }
            catch (NullReferenceException nre)
            {

            }
            catch(Exception exept)
            {

            }
           
        }
    }
}