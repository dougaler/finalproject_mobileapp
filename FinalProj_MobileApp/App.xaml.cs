using FinalProj_MobileApp.Pages;

namespace FinalProj_MobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
			MainPage = new NavigationPage(new MapPage());
		}
    }
}
