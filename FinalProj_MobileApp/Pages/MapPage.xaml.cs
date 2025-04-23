using FinalProj_MobileApp.Models.ViewModels;
using FinalProj_MobileApp.Models;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using FinalProj_MobileApp.DB;


namespace FinalProj_MobileApp.Pages;

public partial class MapPage : ContentPage
{
	public List<CrimeGroups> CrimeNotifs = new List<CrimeGroups>();
    private readonly LocalDBService _dbService = new LocalDBService();

    public MapPage()
	{
		InitializeComponent();

		////Currently adds crimes manually, but in the future the CrimeNotifs item will be populated by DB items
		//CrimeNotifs.Add(new CrimeGroups("Crimes", CrimeNotificationService.GetMockNotifications()));

		////Fills the ListView on the bottom of the page to all the crime objects
		//LVCrimes.ItemsSource = CrimeNotifs;
	}

    //Populates the map with pins for all of the crimes.
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _dbService.SeedMockDataIfEmpty();

        var location = await Geolocation.GetLocationAsync();
        map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMiles(.5)));

        var crimeList = await _dbService.GetCrimes();

        var notifications = crimeList.Select(c => new CrimeNotification
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description,
            LocationName = c.LocationName,
            Date = c.Date,
            Severity = c.Severity,
            Status = c.Status,
            Latitude = c.Latitude,
            Longitude = c.Longitude
        }).ToList();

        CrimeNotifs.Clear();
        CrimeNotifs.Add(new CrimeGroups("Crimes", notifications));
        LVCrimes.ItemsSource = null;
        LVCrimes.ItemsSource = CrimeNotifs;

        map.Pins.Clear();
        foreach (var group in CrimeNotifs)
        {
            foreach (var pin in group)
            {
                map.Pins.Add(pin.GetMapPin());
            }
        }
    }

    //Sends you to AlertDetails for more details on the crime
    private void LVCrimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedItems = e.CurrentSelection.FirstOrDefault() as CrimeNotification;
		if (selectedItems == null) return;
		Navigation.PushAsync(new AlertDetails(selectedItems));
		((CollectionView)sender).SelectedItem = null;
	}

    //Sends you to AlertDetails when you click on a pin
	//Currently doesn't work since the pin being clicked is based on the Pin object and not the CrimeNotification object
	//which is what is sent to the details page, not the Pin
	// - Anthony
    private async void OnPinClicked(object sender, PinClickedEventArgs e) {
        Pin p = (Pin)sender;
		CrimeNotification activeWatch = new CrimeNotification(); //this is here to prevent an error, and is not intended functionally
		//this doesn't send you to a new screen, OnPinClicked() may not be the right method for this feature
        await Navigation.PushAsync(new AlertDetails(activeWatch));
    }

    private async void btnAddCrime_Clicked(object sender, EventArgs e)
    {
        var dbService = new LocalDBService();
        await Navigation.PushAsync(new ReportCrime(dbService));
    }
}