using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;

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
            base.OnAppearing();
            await LocalNotificationCenter.Current.RequestNotificationPermission();
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
        private void OnNotificationTestClicked(object sender, EventArgs e)
        {
            var notification = new NotificationRequest
            {
                NotificationId = new Random().Next(1000),
                Title = "Test Notification",
                Description = "This is a randomly triggered notification.",
                Android = new AndroidOptions
                {
                    ChannelId = "crime_alerts_gray",  
                    Priority = AndroidPriority.High,  
                    VisibilityType = AndroidVisibilityType.Public, 
                    IconSmallName = new AndroidIcon
                    {
                        ResourceName = "ic_launcher",
                        Type = AndroidIcon.DefaultType
                    },
                    Color = new AndroidColor { ResourceName = "notification_gray" },
                    VibrationPattern = new long[] { 0, 500, 1000, 500 } 
                }
            };

            LocalNotificationCenter.Current.Show(notification);
        }


        private async void OnPinClicked(object sender, PinClickedEventArgs e) {
            Pin p = (Pin)sender;
            //await Navigation.PushAsync(new WhateverTheCrimeDetailPageIsCalled(p));
        }
    }

}
