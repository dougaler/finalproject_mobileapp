using FinalProj_MobileApp.Models.ViewModels;
using FinalProj_MobileApp.Models;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace FinalProj_MobileApp.Pages;

public partial class MapPage : ContentPage
{
	public List<CrimeGroups> CrimeNotifs = new List<CrimeGroups>();
	public MapPage()
	{
		InitializeComponent();
		CrimeNotifs.Add(new CrimeGroups("Crimes", new List<CrimeNotification>
		{
			new CrimeNotification
			{
				Id = 1,
				Title = "Gunshot Wound Reported",
				Description = "Police responding for report of a person with a gunshot wound. If safe, stay at your location. Be observant/take action as needed.",
				LocationName = "Sigma Sigma Commons",
				Date = new DateTime(2025, 4, 3, 20, 10, 0),
				Severity = "Critical",
				Status = "Active Investigation",
				Latitude = 39.1320,
				Longitude = -84.5165,
			},
			new CrimeNotification
			{
				Id = 2,
				Title = "Emergency at CVS - Taft & Vine",
				Description = "Police responding to emergency reported at CVS - Taft and Vine. If safe, stay at your location. Beobservanttake action as needed.",
				LocationName = "CVS - Taft and Vine",
				Date = new DateTime(2025, 4, 2, 18, 45, 0),
				Severity = "High",
				Status = "Responding",
				Latitude = 39.1298,
				Longitude = -84.5099
			},
			new CrimeNotification
			{
				Id = 3,
				Title = "Shots Fired - Stratford Ave",
				Description = "Police on scene of emergency after reports of shots fired near 2725 Stratford. Avoid the area.",
				LocationName = "2725 Stratford Ave",
				Date = new DateTime(2025, 3, 30, 23, 15, 0),
				Severity = "Critical",
				Status = "Scene Cleared",
				Latitude = 39.1350,
				Longitude = -84.5042
			},
			new CrimeNotification
			{
				Id = 4,
				Title = "Emergency Reported - Bishop St.",
				Description = "Police responding to emergency reported on 3239 Bishop. If safe, stay at your location. Be observanttakeaction as needed.",
				LocationName = "3239 Bishop St.",
				Date = new DateTime(2025, 3, 29, 21, 50, 0),
				Severity = "High",
				Status = "Investigation Ongoing",
				Latitude = 39.1329,
				Longitude = -84.5180
			},
			new CrimeNotification
			{
				Id = 5,
				Title = "Suspicious Activity - Langsam Library",
				Description = "Suspicious individual seen loitering outside the Langsam Library. Officers dispatched, area searched.",
				LocationName = "Langsam Library",
				Date = new DateTime(2025, 3, 28, 17, 25, 0),
				Severity = "Medium",
				Status = "Cleared",
				Latitude = 39.1315,
				Longitude = -84.5151
			}}));
		LVCrimes.ItemsSource = CrimeNotifs;
	}


	private void LVCrimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedItems = e.CurrentSelection.FirstOrDefault() as CrimeNotification;
		if (selectedItems == null) return;
		Navigation.PushAsync(new AlertDetails(selectedItems));
		((CollectionView)sender).SelectedItems = null;
	}

    protected override async void OnAppearing() {
        //Places a pin on your current location, but honestly this isn't needed
        base.OnAppearing();
        var location = await Geolocation.GetLocationAsync();
        map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMiles(10)));


		//Getting crime list from DB (unimplemented)
		//Task.Run(async () => Lv.ItemsSource = await _dbService.GetCrimes());
		foreach (var i in CrimeNotifs) {
			foreach (var pin in i) {
				map.Pins.Add(pin.GetMapPin());
			}
		}

	}
    //Broken, currently no way (or I can't think of one) to find the CrimeNotification object based on the pin
    private async void OnPinClicked(object sender, PinClickedEventArgs e) {
        Pin p = (Pin)sender;
		CrimeNotification activeWatch;
		//this doesn't actually send you there some reason, even if you could find the right crime notif
        //await Navigation.PushAsync(new AlertDetails(activeWatch));
    }
}