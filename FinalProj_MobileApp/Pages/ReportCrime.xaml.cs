using FinalProj_MobileApp.DB;

namespace FinalProj_MobileApp.Pages;

public partial class ReportCrime : ContentPage
{
	private readonly LocalDBService _localDBService;

	public ReportCrime(LocalDBService dBService)
	{
		InitializeComponent();
		_localDBService = dBService;
	}

    private void ClearFields()
    {
        entId.Text = string.Empty;
        entTitle.Text = string.Empty;
        entDescription.Text = string.Empty;
        entLocation.Text = string.Empty;
        dpDate.Date = DateTime.Now;
        entSeverity.Text = string.Empty;
        entStatus.Text = string.Empty;
        entLatitude.Text = string.Empty;
        entLongitude.Text = string.Empty;
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        Crime crime = new Crime
        {
            Title = entTitle.Text,
            Description = entDescription.Text,
            LocationName = entLocation.Text,
            Date = dpDate.Date,
            Severity = entSeverity.Text,
            Status = entStatus.Text,
            Latitude = double.TryParse(entLatitude.Text, out var lat) ? lat : 0,
            Longitude = double.TryParse(entLongitude.Text, out var lng) ? lng : 0
        };

        if (string.IsNullOrWhiteSpace(entId.Text))
        {
            // New entry — let the database auto-generate the ID
            await _localDBService.Create(crime);
        }
        else if (int.TryParse(entId.Text, out int existingId))
        {
            crime.Id = existingId;
            await _localDBService.Update(crime);
        }

        // Update the CrimeNotification object creation in btnSave_Clicked
        await CrimeNotificationService.SendNotificationAsync(new CrimeNotification
        {
            Title = crime.Title,
            Description = $"A new crime has been reported: {crime.Description}", 
            LocationName = crime.LocationName,
            Date = crime.Date,
            Severity = crime.Severity,
            Status = crime.Status,
            Latitude = crime.Latitude,
            Longitude = crime.Longitude
        });

        await DisplayAlert("Success", "Crime record saved and notification sent.", "OK");

        ClearFields();
    }


    private async void btnPull_Clicked(object sender, EventArgs e)
    {
        string input = await DisplayPromptAsync("Fetch Crime", "Enter Crime ID:", "OK", "Cancel", "e.g. 1", keyboard: Keyboard.Numeric);

        if (!int.TryParse(input, out int crimeId))
        {
            await DisplayAlert("Error", "Invalid ID entered.", "OK");
            return;
        }

        var crime = await _localDBService.GetById(crimeId);

        if (crime != null)
        {
            // Populate fields with crime data
            entId.Text = crime.Id.ToString();
            entTitle.Text = crime.Title;
            entDescription.Text = crime.Description;
            entLocation.Text = crime.LocationName;
            dpDate.Date = crime.Date;
            entSeverity.Text = crime.Severity;
            entStatus.Text = crime.Status;
            entLatitude.Text = crime.Latitude.ToString();
            entLongitude.Text = crime.Longitude.ToString();
        }
        else
        {
            await DisplayAlert("Not Found", $"No record found with ID {crimeId}.", "OK");
        }
    }
    //private async void btnSeedMockData_Clicked(object sender, EventArgs e)
    //{
    //    var mockData = CrimeNotificationService.GetMockNotifications();

    //    foreach (var item in mockData)
    //    {
    //        var crime = new Crime
    //        {
    //            Title = item.Title,
    //            Description = item.Description,
    //            LocationName = item.LocationName,
    //            Date = item.Date,
    //            Severity = item.Severity,
    //            Status = item.Status,
    //            Latitude = item.Latitude,
    //            Longitude = item.Longitude
    //        };

    //        await _localDBService.Create(crime);
    //    }

    //    await DisplayAlert("Success", "Mock data has been inserted into the database.", "OK");
    //}

    private async void btnClearDatabase_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirm", "Are you sure you want to delete all crime records?", "Yes", "No");
        if (!confirm) return;

        var allCrimes = await _localDBService.GetCrimes();
        foreach (var crime in allCrimes)
        {
            await _localDBService.Delete(crime);
        }

        await DisplayAlert("Success", "All records have been deleted.", "OK");
        ClearFields();
    }
}