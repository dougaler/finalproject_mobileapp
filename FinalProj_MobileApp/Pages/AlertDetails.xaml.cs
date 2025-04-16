using FinalProj_MobileApp.Models;

namespace FinalProj_MobileApp.Pages;

public partial class AlertDetails : ContentPage
{
	public AlertDetails(CrimeNotification item)
	{
		
		InitializeComponent();
		LblTitle.Title = item.Title;
		LblDescription.Text = item.Description;
		LblDate.Detail = item.Date.ToString("MM/dd/yyyy");
		LblSeverity.Detail = item.Severity;
		LblStatus.Detail = item.Status;


	}
}