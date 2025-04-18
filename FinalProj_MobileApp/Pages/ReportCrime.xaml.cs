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

    private void btnSave_Clicked(object sender, EventArgs e)
    {
		if ()
    }
}