using FinalProj_MobileApp.Models;

namespace FinalProj_MobileApp.Pages;

public partial class AlertDetails : ContentPage
{
	public AlertDetails(CrimeNotification item)
	{
		
		InitializeComponent();
		LblTitle.Text = item.Title;
		LblDescription.Text = item.Description;
		LblDate.Text = item.Date.ToString("MM/dd/yyyy");
		LblSeverity.Text = item.Severity;
		LblStatus.Text = item.Status;


	}
}