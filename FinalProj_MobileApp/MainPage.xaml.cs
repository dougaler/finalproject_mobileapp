using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace FinalProj_MobileApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            //Places a pin on your current location, but honestly this isn't needed
            base.OnAppearing();
            var location = await Geolocation.GetLocationAsync();
            map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMiles(10)));
            var pin = new Pin() {
                Address = "Your Location",
                Location = location,
                Type = PinType.Place,
                Label = "Hello"
            };
            map.Pins.Add(pin);

            /*
            //Getting crime list from DB (unimplemented)
            Task.Run(async () => Lv.ItemsSource = await _dbService.GetCrimes());
            foreach (var pin in Lv.ItemsSource) {
                map.Pins.Add(pin.GetMapPin());
            }
            */
        }
        private async void OnPinClicked(object sender, PinClickedEventArgs e) {
            Pin p = (Pin)sender;
            //await Navigation.PushAsync(new WhateverTheCrimeDetailPageIsCalled(p));
        }
    }

}
