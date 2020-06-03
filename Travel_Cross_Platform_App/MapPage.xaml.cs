using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Travel_Cross_Platform_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private bool hasLocationPermission = false;
        public MapPage()
        {
            InitializeComponent();
            GetPermissions();
        }

        private async void GetPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationWhenInUsePermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                    {
                        await DisplayAlert("Potrzebna Lokalizacja", "Potrzebujemy dostępu do lokalizacji!", "OK");
                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<LocationWhenInUsePermission>();
                }
                if (status == PermissionStatus.Granted)
                {
                    hasLocationPermission = true;
                    location.IsShowingUser = true;

                    GetLocation();
                }
                else
                {
                    await DisplayAlert("Lokacja odmówiona", "Nie podałeś nam lokalizacji, więc nie masz dostępu do swojej lokalizacji na mapie", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;

                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(TimeSpan.Zero, 100);
            }
                GetLocation();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged;
        }


        void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        private async void GetLocation()
        {
            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();
                MoveMap(position);
            }
        }

        private void MoveMap(Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            location.MoveToRegion(span);
        }
    }
}